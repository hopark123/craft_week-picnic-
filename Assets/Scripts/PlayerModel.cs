using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    //player info field
    public const float JUMP_POWER = 30;
    public const float MOVE_SPEED = 8;
    public const int MAX_JUMPCNT = 2;

    //player status field
    public bool IsAlive
    {
        get => _isAlive;
        set
        {
            _isAlive = value;
            if (value) //init
            {
                IsGround = false;
                IsSlow = false;
                IsSlide = false;
                _jumpCnt = 0;
            }
        }
    }
    
    public bool IsSlow { get; set; } = false;
    
    public bool IsGround
    {
        get => _isGround;
        set
        {
            this._isGround = value;
            if (value == true)
                _jumpCnt = 0;
        }
    }

    public bool IsSlide { get; set; } = false;
    
    public (Vector2, Vector2) PlayerBox
    {
        get
        {
            if (IsSlide)
                return (_slideSize, _slideOffset);
            return (_standSize, _standOffset);
        }
    }

    public float JumpPower
    {
        get
        {
            if (this.IsSlow)
                return JUMP_POWER * 2 / 3;
            return JUMP_POWER;
        }
    }

    public float MoveSpeed
    {
        get
        {
            if (this.IsSlow)
                return MOVE_SPEED / 2;
            return MOVE_SPEED;
        }
    }

    public void InitPlayerBox(Vector2 size, Vector2 offset)
    {
        _standSize = size;
        _standOffset = new Vector2(0, size.y / 2);
        _slideSize = new Vector2(_standSize.x, _standSize.y / 4);
        _slideOffset = new Vector2(_standOffset.x, _standOffset.y / 4);
    }

    //private field
    private int _jumpCnt = 0;
    private bool _isAlive = true,
                 _isGround = false;
    
    private Vector2 _standSize,
	                _standOffset;
    private Vector2 _slideSize,
	                _slideOffset;

    //method
    public bool Jump()
    {
        if (!IsAlive || _jumpCnt >= MAX_JUMPCNT)
            return false;
        _jumpCnt++;
        return true;
    }

    public bool Slide()
    {
        if (IsAlive)
            return IsSlide = true;
        return false;
    }
}
