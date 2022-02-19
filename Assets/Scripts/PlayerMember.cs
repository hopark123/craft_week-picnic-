using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerMember : MonoBehaviour
{
    private int membercnt = 0;
    [SerializeField] private Text text;

    void Start()
    {
        membercnt = 0;    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collision vector 비교
        if (collision.gameObject.tag == "Member")
        {
            membercnt++;
            text.GetComponent<MemberCnt>().ShowMemberCnt(membercnt);
            if (membercnt > 1)
                collision.GetComponent<Member>().targetName = "Member" + (membercnt - 1).ToString();
            collision.gameObject.tag = "Member" + membercnt.ToString();
            collision.GetComponent<Member>().memberId = membercnt;
            collision.GetComponent<Member>().TargetFind();
            collision.GetComponent<Member>().touch = true;

            //Debug.Log("touch");
        }
    }

    public int getMemberCnt()
    {
        return (membercnt);
    }
}
