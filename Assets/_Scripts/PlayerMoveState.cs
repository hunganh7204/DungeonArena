using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    
    public PlayerMoveState(PlayerController currentContext, PlayerStateFactory factory) : base(currentContext, factory) { }
    public override void EnterState()
    {
        Debug.Log("Check move");
        ctx.Animator.SetBool(ctx.IsMovingHash, true);
    }

    public override void UpdateState()
    {
        ctx.HandleMovement();
        CheckSwitchState();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        if (ctx.IsAttackPressed)
        {
            ctx.SwitchState(factory.Attack());
        }
        else if (!ctx.IsMovementPressed)
        {
            ctx.SwitchState(factory.Idle());
        }
        else if (ctx.IsDashPressed)
        {
            ctx.SwitchState(factory.Dash());
        }
    }

}
