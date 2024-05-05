using UnityEngine;

public class MainState : UIBaseState
{
    public MainState(UIStateMachine currentContext, UIStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        
    }
}
