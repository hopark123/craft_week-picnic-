using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerCollision))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField]
    StageManager stageManager;
    Animator animator;

    public bool Slow { get; set; } = false;

    const float _jumpPower = 30;
    public float jumpPower
    {
        get
        {
            if (Slow)
                return _jumpPower * 2 / 3;
            return _jumpPower;
        }
    }

    const float _moveSpeed = 8;
    
    public float moveSpeed
    {
        get
        {
            if (Slow)
                return (_moveSpeed + GameManager.stageNumber) / 2;
            return _moveSpeed;
        }
    }

    const int _maxJumpCnt = 2;

    public int MaxJumpCnt { get => _maxJumpCnt; }

    public bool IsGround { get; set; }
    public bool IsAlive { get; set; }
    
    public uint Jumpcnt { get; set; } = 0;

    void OnEnable()
    {
        IsAlive = true;
        IsGround = false;
    }
    
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Kill()
    {
        if (!IsAlive)
            return;
        IsAlive = false;
        animator.SetBool("hit", true);
        stageManager.Kill();
    }

    public void Hit()
    {
        Kill();
    }

    public void GetItem(GameObject itemObject)
    {
        int index = Convert.ToInt32(itemObject.name);
        stageManager.GetItem(index);
        itemObject.SetActive(false);
    }

    public void Goal()
    {
        Debug.Log("game result camera");
        GameManager.Load("EndingScene");
    }
    
    public ref Animator Animate() { return ref animator; }
}
