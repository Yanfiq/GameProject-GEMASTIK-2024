using UnityEngine;

public class PlayerFallState : PlayerBaseState, IRootState
{
    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    {
        IsRootedState = true;
    }

    public override void CheckSwitchState()
    {
        //if (Ctx.CharacterController.isGrounded)
        if(Physics.CheckSphere(Ctx._groundChecker.position, Ctx._groundDistance, Ctx._groundMask))
        {
            SwitchState(Factory.Grounded());
        }
    }

    public void HandleGravity()
    {
        float previousYVelocity = Ctx.CurrentMovementY;
        Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.Gravity * Time.deltaTime);
        Ctx.AppliedMovementY = Mathf.Max((previousYVelocity + Ctx.CurrentMovementY) * .5f, -20f);
    }

    public override void EnterState()
    {
        InitializeSubState();
        Ctx.Animator.SetBool(Ctx.IsFallingHash, true);
    }

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchState();
    }

    public override void ExitState()
    {
        Ctx.Animator.SetBool(Ctx.IsFallingHash, false);
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
