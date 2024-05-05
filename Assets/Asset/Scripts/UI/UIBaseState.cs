using UnityEngine;

public abstract class UIBaseState 
{
    private UIStateMachine _ctx;
    private UIStateFactory _factory;

    protected UIStateMachine Ctx { get { return _ctx; } }
    protected UIStateFactory Factory { get { return _factory; } }

    public UIBaseState(UIStateMachine currentContext, UIStateFactory UIStateFactory)
    {
        _ctx = currentContext;
        _factory = UIStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
    public void UpdateStates()
    {
        UpdateState();
    }
    protected void SwitchState(UIBaseState newState)
    {
        ExitState();
        newState.EnterState();
    }
}
