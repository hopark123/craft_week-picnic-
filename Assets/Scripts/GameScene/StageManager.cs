using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private int stage;
    [SerializeField]
    private GameObject goal;
    [SerializeField]
    private GameObject obs;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject respawnPoint;
    [SerializeField]
    private ItemSlot slot;
    [SerializeField]
    private GameObject playCanvus;
    [SerializeField]
    private GoalWindow goalWindow;

    public bool Pause { get; private set; } = false;
    
    public void Kill()
    {
        StartCoroutine(KillPlayer());
        slot.LostItem();
        GameManager.deathCnt++;
    }

    public void GetItem(int index)
    {
        slot.GetItem(index);
        GameManager.GetItem(index);
    }

    IEnumerator KillPlayer()
    {
        yield return new WaitForSecondsRealtime(1.0f);

        player.SetActive(false);
        yield return new WaitForSecondsRealtime(2.0f);
        Respawn();
    }

    void Respawn()
    {
        for (int i = 0; i < obs.transform.childCount; ++i)
        {
            obs.transform.GetChild(i).gameObject.SetActive(true);
        }
        player.transform.position = respawnPoint.transform.position;
        player.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.GetComponent<Player>().Jumpcnt = 2;
        player.transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        player.SetActive(true);
    }

    public void Restart()
    {
        GameManager.ResetItem();
        Time.timeScale = 1f;
        GameManager.Load(SceneManager.GetActiveScene().name);
    }

    public void Goal()
    {
        goalWindow.Goal();
        PauseGame();
    }

    public void PauseGame()
    {
        Pause = !Pause;
        if (Pause)
        {
            Time.timeScale = 0f;
            playCanvus.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            playCanvus.SetActive(true);
        }
    }
}
