using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingView : MonoBehaviour
{
    [SerializeField]
    private GameObject itemObjs;
    [SerializeField]
    private Text itemInfo;
    [SerializeField]
    private Text deadCnt;

    private int itemCnt = 0;
    private int itemTotal = 0;


    // Start is called before the first frame update
    void Start()
    {
        deadCnt.text = GameModel.DeadCnt.ToString();
        itemTotal = GameModel.STAGE_SIZE * GameModel.ITEM_SIZE;
        itemInfo.text = Scan().ToString() + "/" + itemTotal.ToString();
    }

    int Scan()
    {
        for (int i = 0; i < GameModel.STAGE_SIZE; ++i)
        {
            for (int j = 0; j < GameModel.ITEM_SIZE; ++j)
            {
                if (GameModel.GetItemStatus(i, j))
                    itemCnt++;
                itemObjs.transform.GetChild(i * GameModel.ITEM_SIZE + j).gameObject.SetActive(GameModel.GetItemStatus(i, j));
            }
        }
        return itemCnt;
    }
}
