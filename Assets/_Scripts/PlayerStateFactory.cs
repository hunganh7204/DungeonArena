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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
