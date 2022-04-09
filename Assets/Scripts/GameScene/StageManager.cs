using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public GameObject goal;
    public GameObject obs;
    public GameObject player;
    public GameObject respawnPoint;

    public void Kill()
    {
        StartCoroutine(KillPlayer());
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
}
