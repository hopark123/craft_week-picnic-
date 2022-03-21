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
    Animator animator;
    
    [field:SerializeField]
    public float jumpPower { get; private set; }
    
    [field:SerializeField]
    public float moveSpeed { get; private set; }
    
    [field:SerializeField]
    public int MaxJumpCnt { get; private set; }

    public bool IsGround { get; set; }
    public bool IsAlive { get; set; }
    
    public uint Jumpcnt { get; set; } = 0;
    public uint Itemcnt { get; set; } = 0;
    
    void OnEnable()
    {
        IsAlive = true;
        IsGround = false;
    }

    void OnDisable()
    {
        IsAlive = false;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Hit() { }

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
