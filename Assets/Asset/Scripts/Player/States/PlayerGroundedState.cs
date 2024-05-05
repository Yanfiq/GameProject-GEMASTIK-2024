using System;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState, IRootState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    {
        IsRootedState = true;
    }

    public void HandleGravity()
    {
        Ctx.CurrentMovementY = Ctx.Gravity;
        Ctx.AppliedMovementY = Ctx.Gravity;
    }

    public override void EnterState()
    {
        InitializeSubState();
        HandleGravity();
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
        if(Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress)
        {
            SwitchState(Factory.Jump());
        }
        //else if (!Ctx.CharacterController.isGrounded)
        else if (!Physics.CheckSphere(Ctx._groundChecker.position, Ctx._groundDistance, Ctx._groundMask))
        {
            SwitchState(Factory.Fall());
        }
    }

    public override void InitializeSubState()
    {
        if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SetSubState(Factory.Walk());
        }
        else
        {
            SetSubState(Factory.Run());
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {

    }
}
