using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    
    public PlayerMoveState(PlayerController currentContext, PlayerStateFactory factory) : base(currentContext, factory) { }
    public override void EnterState()
    {
        Debug.Log("Check move");
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
        if (!ctx.IsMovementPressed)
        {
            ctx.SwitchState(factory.Idle());
        }
    }

}
