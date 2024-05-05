using UnityEngine;

public abstract class PlayerBaseState
{
    private bool _isRootedState = false;
    private PlayerStateMachine _ctx;
    private PlayerStateFactory _factory;
    private PlayerBaseState _currentSuperState;
    private PlayerBaseState _currentSubState;

    protected bool IsRootedState { set { _isRootedState = value; } }
    protected PlayerStateMachine Ctx { get { return _ctx; } }
    protected PlayerStateFactory Factory { get { return _factory; } }

    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
    public abstract void InitializeSubState();
    public abstract void OnCollisionEnter(Collision collision);

    public void UpdateStates() 
    {
        UpdateState();
        if(_currentSubState != null)
        {
            _currentSubState.UpdateState();
        }
    }
    protected void SwitchState(PlayerBaseState newState)
    {
        ExitState();
        newState.EnterState();

        if(_isRootedState) _ctx.CurrentState = newState;
        else if(_currentSuperState != null) _currentSuperState.SetSubState(newState);
    }
    protected void SetSuperState(PlayerBaseState newSuperState) 
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState) 
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
