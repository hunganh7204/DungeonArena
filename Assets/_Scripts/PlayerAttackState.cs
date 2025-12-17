using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerController currentContext, PlayerStateFactory factory) : base(currentContext, factory) { }

    public override void EnterState()
    {
        Debug.Log("Check attack");
        if(Time.time - ctx.LastAttackTime > ctx.ComboResetTime || ctx.ComboCounter > 2)
        {
            ctx.ComboCounter = 0; //reset combo
        }
        ctx.Animator.SetInteger(ctx.ComboStepHash, ctx.ComboCounter); //gui thong tin don danh so may
        ctx.Animator.SetTrigger(ctx.AttackHash);
        //ctx.Controller.Move(Vector3.zero); // nhan vat khong bi troi khi tan cong
        ctx.ResetAttackTrigger();
        ctx.LastAttackTime = Time.time; //luu thoi gian vua danh
        ctx.ComboCounter++;//tang so dem combo
        ctx.SetNextAttackTime();
        Debug.Log(ctx.ComboCounter);
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
        AnimatorStateInfo stateInfo = ctx.Animator.GetCurrentAnimatorStateInfo(0); // lay thong tin animation o Layer0
        if (stateInfo.normalizedTime >= 0.9f ) //Kiem tra da xong animation chua
        {
            ctx.SwitchState(factory.Idle());
        }
    }

}
