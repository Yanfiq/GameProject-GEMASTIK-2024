using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : UIBaseState
{
    public GameOverState(UIStateMachine currentContext, UIStateFactory playerStateFactory)
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
