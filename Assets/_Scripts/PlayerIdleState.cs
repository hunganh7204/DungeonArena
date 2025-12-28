using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerController currentcontext, PlayerStateFactory factory) : base(currentcontext, factory) { }

    public override void EnterState()
    {
        Debug.Log("Check idle");
        ctx.Animator.SetBool(ctx.IsMovingHash, false);
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
        if(ctx.IsAttackPressed)
        {
            ctx.SwitchState(factory.Attack());
        }
        else if (ctx.IsMovementPressed)
        {
            ctx.SwitchState(factory.Move());
        }
        else if (ctx.IsDashPressed)
        {
            ctx.SwitchState(factory.Dash());
        }
    }

}
