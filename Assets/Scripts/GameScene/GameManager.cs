using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject respawnPoint;
    public GameObject player;
    
    public void Kill()
    {
        StartCoroutine(KillPlayer());
    }

    IEnumerator KillPlayer()
    {
        player.SetActive(false);
        yield return new WaitForSecondsRealtime(3.0f);
        player.transform.position = respawnPoint.transform.position;
        player.SetActive(true);
    }
}
