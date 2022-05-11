using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameModel
{
    //game data
    public const int STAGE_SIZE = 3;
    public const int ITEM_SIZE = 4;

    //openingScene
    public static bool FirstStart { get; set; } = true;
    //playData
    public static int StageNumber { get; set; } = 0;
    public static int DeadCnt { get; private set; } = 0;
    //itemField
    private static bool[,] Itemlst = new bool[STAGE_SIZE, ITEM_SIZE];
    //scene
    public static SceneController sceneController { get; } = new SceneController();
    //item method
    public static bool GetItemStatus(int itemIdx) => Itemlst[StageNumber, itemIdx];
    
    public static bool GetItemStatus(int stageIdx, int itemIdx) => Itemlst[stageIdx, itemIdx];
    
	public static void EatItem(int itemIdx)
    {
        Debug.Log("eat" + itemIdx);
        Itemlst[StageNumber, itemIdx] = true;
    }

    public static void DeleteItem(int itemIdx)
    {
        Itemlst[StageNumber, itemIdx] = false;
    }

    public static void ResetStageItem()
    {
        for (int i = 0; i < ITEM_SIZE; i++)
            DeleteItem(i);
    }

    public static void ResetGame()
    {
        for (int i = 0; i < STAGE_SIZE; i++)
        {
            for (int j = 0; j < ITEM_SIZE; j++)
            {
                Itemlst[i, j] = false;
            }
        }
        DeadCnt = 0;
        FirstStart = true;
        StageNumber = 0;  
    }

    //game method
    public static void Dead()
    {
        DeadCnt++;
    }

    public static void Load(string nextScene)
    {
        sceneController.dest = nextScene;
        sceneController.CallLoadScene();
    }
}
