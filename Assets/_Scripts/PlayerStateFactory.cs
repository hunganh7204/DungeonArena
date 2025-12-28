using UnityEngine;

public class PlayerStateFactory : MonoBehaviour
{
    PlayerController _context;

    public PlayerStateFactory(PlayerController context)
    {
        _context = context;
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context,this);
    }

    public PlayerBaseState Move()
    {
        return new PlayerMoveState(_context,this);
    }

    public PlayerBaseState Attack()
    {
        return new PlayerAttackState(_context,this);
    }

    public PlayerBaseState Dash()
    {
        return new PlayerDashState(_context,this);
    }

}
