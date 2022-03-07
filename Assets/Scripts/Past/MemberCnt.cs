using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MemberCnt : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Text text;
    private int memberId;
    public void ShowMemberCnt(int memcnt)
    {
        memberId = memcnt;
        string temp = memcnt.ToString();
        text.text = temp;
    }
}
