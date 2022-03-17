using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerMember : MonoBehaviour
{
    public List<GameObject> l_member;
    private int membercnt = 0;
    [SerializeField] private Text text;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject showItem;



    void Start()
    {
        l_member.Add(player);
        membercnt = 0;
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7) //member
        {
            membercnt++;
            //Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            l_member.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
            ShowMemberCnt();
            showItem.GetComponent<ShowItem>().AddItem(membercnt, collision.gameObject);
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) //member
        {
            membercnt++;
            //Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            l_member.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
            ShowMemberCnt();
            showItem.GetComponent<ShowItem>().AddItem(membercnt, collision.gameObject);
        }
    }

    public void DeleteItem()
    {
        Destroy(l_member[membercnt]);
        membercnt--;
        ShowMemberCnt();
        if (membercnt < 0)
            Debug.Log("Game.Over");
    }

    public void ShowMemberCnt()
    {
        string temp = membercnt.ToString();
        text.text = temp;
    }



    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // collision vector 비교
    //    if (collision.gameObject.tag == "Member")
    //    {
    //        membercnt++;
    //        text.GetComponent<MemberCnt>().ShowMemberCnt(membercnt);
    //        if (membercnt > 1)
    //            collision.GetComponent<Member>().targetName = "Member" + (membercnt - 1).ToString();
    //        collision.gameObject.tag = "Member" + membercnt.ToString();
    //        collision.GetComponent<Member>().memberId = membercnt;
    //        collision.GetComponent<Member>().TargetFind();
    //        collision.GetComponent<Member>().touch = true;
    //        Enemy.GetComponent<Enemy>().EnermyFindTarget("Member" + membercnt.ToString());
    //    }
    //}

    //public int getMemberCnt()
    //{
    //    return (membercnt);
    //}
}
