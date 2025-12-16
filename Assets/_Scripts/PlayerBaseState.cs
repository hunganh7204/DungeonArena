using UnityEngine;

public abstract class PlayerBaseState : MonoBehaviour
{
    protected PlayerController ctx;
    protected PlayerStateFactory factory;
    public PlayerBaseState(PlayerController ctx, PlayerStateFactory factory)
    {
        this.ctx = ctx;
        this.factory = factory;
    }

    public abstract void EnterState(); //vao trang thai
    public abstract void UpdateState(); //chay trang thai lien tuc moi frame
    public abstract void ExitState(); //chay khi thoat trang thai
    public abstract void CheckSwitchState(); //kiem tra dieu kien chuyen trang thai


}
