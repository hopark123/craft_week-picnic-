using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour
{
    [SerializeField]
    private GameView gameView;
    [SerializeField]
    private GameObject obstacles = null;
    [SerializeField]
    private GameObject items = null;

    private bool IsPause;

    // Start is called before the first frame update
    void Start()
    {
        GameModel.ResetStageItem();
        gameView.InitSlot();
        gameView.Clear_Init();
        gameView.Pause_Init();

        IsPause = false;
    }

    //Game Item
    public void EatItem(GameObject obj)
    {
        int i = Convert.ToInt32(obj.name);
        GameModel.EatItem(i);
        gameView.ActivateSlot(i);
    }

    public void LoseItem()
    {
        for (int i = 0; i < GameModel.ITEM_SIZE; i++)
        {
            if (!GameModel.GetItemStatus(i))
                continue;
            else
            {
                GameModel.LostItem(i);
                gameView.DisableSlot(i);
                break;
            }
        }
    }
    //GameUI
    private void pauseGame()
    {
        IsPause = true;
        Time.timeScale = 0f;
        SoundController.instance.BgSoundStop();
    }

    private void playGame()
    {
        IsPause = false;
        Time.timeScale = 1f;
        SoundController.instance.BgSoundRestart();
    }
    
    public void Pause()
    {
        gameView.Pause_Open();
        pauseGame();
    }

    public void Play()
    {
        gameView.Pause_Close();
        playGame();
    }
    //SceneControl
    public void ReStart()
    {
        gameView.Pause_Close();
        playGame();
        GameModel.Load(SceneManager.GetActiveScene().name);
    }


    public void GoHome_P()
    {
        gameView.Pause_Close();
        GameModel.ResetStageItem();
        playGame();
        GameModel.Load("TitleScene");
    }

    public void Goal()
    {
        gameView.Clear_Open();
        pauseGame();
    }

    public void GoNextStage()
    {
        playGame();
        if (GameModel.StageNumber < GameModel.STAGE_SIZE - 1)
        {
            GameModel.StageNumber++;
            GameModel.Load("GameScene" + (GameModel.StageNumber + 1).ToString());
        }
        else
        {
            GameModel.Load("EndingScene");
        }
    }

    public void GoHome_C()
    {
        playGame();
        if (GameModel.StageNumber < GameModel.STAGE_SIZE)
            GameModel.StageNumber++;
        GameModel.Load("TitleScene");
    }

    //GamePlay

    public void Dead()
    {
        GameModel.Dead();
    }
    
    public void RespawnItem()
    {
        if (items == null)
            return;
        for (int i = 0; i < items.transform.childCount; ++i)
            items.transform.GetChild(i).gameObject.SetActive(true);
    }

    public void RespawnMap()
    {
        if (obstacles == null)
            return;
        for (int i = 0; i < obstacles.transform.childCount; ++i)
            obstacles.transform.GetChild(i).gameObject.SetActive(true);
        GameModel.ResetStageItem();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause && !IsPause)
        {
            Pause();
        }
    }
}
