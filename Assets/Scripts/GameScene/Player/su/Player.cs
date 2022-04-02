using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerCollision))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    StageManager stageManager;
    Animator animator;
    SpriteRenderer spriteRenderer;

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
                return (_moveSpeed + gameManager.stageNumber) / 2;
            return _moveSpeed;
        }
    }

    const int _maxJumpCnt = 2;

    public int MaxJumpCnt { get => _maxJumpCnt; }

    public bool IsGround { get; set; }
    public bool IsAlive { get; set; }
    
    public uint Jumpcnt { get; set; } = 0;
    public uint Itemcnt { get; set; } = 0;

    void OnEnable()
    {
        IsAlive = true;
        IsGround = false;
    }
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        Debug.Log(this.GetComponent<Transform>().position.ToString() + "Hit");
        Kill();
    }

    public void GetItem(GameObject itemObject)
    {
        Itemcnt++;
        string index = itemObject.GetComponent<Item>().Id;
        Debug.Log(index);
        Debug.Log((int)index[index.Length - 1]);
        gameManager.GetItems((int)index[index.Length - 1] - '0');
        //ShowMemberCnt();
        itemObject.SetActive(false);
        //showItem.GetComponent<ShowItem>().AddItem(membercnt, itemObject);
    }

    public void Goal()
    {
        gameManager.stageNumber++;
        if (gameManager.stageNumber > 3)
            gameManager.ChangeSence("endScene");
        else
            gameManager.ChangeSence("stage" + gameManager.stageNumber.ToString());
        Debug.Log("end game");
    }
    
    public ref Animator Animate() { return ref animator; }
}
