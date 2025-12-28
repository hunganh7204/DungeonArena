using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    private float _startTime;
    private int _playerLayer;
    private int _enemyLayer;
    public PlayerDashState(PlayerController currentContext, PlayerStateFactory factory) : base(currentContext, factory) { }

    public override void EnterState()
    {
        Debug.Log("Dash");
        _startTime = Time.time;
        ctx.Animator.SetBool(ctx.IsMovingHash, true);

        _playerLayer = LayerMask.NameToLayer("Player");
        _enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics.IgnoreLayerCollision(_playerLayer, _enemyLayer, true);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        ctx.Controller.Move(ctx.transform.forward * ctx.DashSpeed*Time.deltaTime);
    }

    public override void ExitState()
    {
        Physics.IgnoreLayerCollision(_playerLayer,_enemyLayer,false);
    }

    public override void CheckSwitchState()
    {
        if(Time.time >= _startTime+ctx.DashDuration)
        {
            if (ctx.IsMovementPressed) ctx.SwitchState(factory.Move());
            else
            {
                 ctx.SwitchState(factory.Idle());
            }
        }
    }

}
