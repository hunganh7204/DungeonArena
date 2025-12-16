using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerController currentcontext, PlayerStateFactory factory) : base(currentcontext, factory) { }

    public override void EnterState()
    {
        Debug.Log("Check idle");
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
        if (ctx.IsMovementPressed)
        {
            ctx.SwitchState(factory.Move());
        }
    }

}
