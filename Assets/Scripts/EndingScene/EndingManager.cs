using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject itemObjs;
    [SerializeField]
    private GameObject itemCntObj;
    private Text itemInfo;
    [SerializeField]
    private GameObject deadTextObj;
    private Text deadCnt;

    private int itemCnt;
    private int itemTotal;

    void Awake()
    {
        deadCnt = deadTextObj.GetComponent<Text>();
        itemInfo = itemCntObj.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        deadCnt.text = GameManager.deathCnt.ToString();
        itemTotal = GameManager.stageSize * GameManager.itemNum;
        itemInfo.text = Scan().ToString() + "/" + itemTotal.ToString();
    }

    int Scan()
    {
        for (int i = 0; i < GameManager.stageSize; ++i)
        {
            for (int j = 0; j < GameManager.itemNum; ++j)
            {
                if (GameManager.itemlst[i, j])
                {
                    itemCnt++;
                }
                itemObjs.transform.GetChild(i * GameManager.itemNum + j).gameObject.SetActive(GameManager.itemlst[i, j]);
            }
        }
        return itemCnt;
    }
}
