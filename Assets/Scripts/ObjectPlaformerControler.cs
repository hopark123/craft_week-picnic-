using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPlaformerControler : PhysicsObject
{
    [field:SerializeField]
    public float jumpForce { get; set; }
    [field:SerializeField]
    public float speed { get; set; }
    public bool isGrounded { get => grounded; }
    
    private float _moveDir = 0f;
    private bool _jump = false;
    private bool _isJumping = false;
    [Flags]
    public enum Dir
    {
        left,
        stop,
        right
    }

    protected override void ComputeVelocity()
    {
        base.ComputeVelocity();
        //init move jump vector
        Vector2 move = Vector2.zero;

        move.x = _moveDir * speed;
        if (_jump && isGrounded)
        {
            base.velocity.y = jumpForce;
            _jump = false;
            _isJumping = true;
        }
        else if (_isJumping)
        {
            base.velocity.y *= 0.5f;
            _isJumping = false;
        }
        base.targetVelocity = move;
    }

    public void Move(Dir dir)
    {
        switch (dir)
        {
            case Dir.left:
                _moveDir = -1;
                break ;
            case Dir.right:
                _moveDir = 1;
                break ;
            default:
                _moveDir = 0;
                break ;
        }
    }

    public void Jump()
    {
        _jump = true;
    }
}
