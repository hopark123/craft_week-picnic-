using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneController))]
public class GameManager : MonoBehaviour
{
    public GameObject respawnPoint;
    public GameObject player;
    public int stageNumber { get; set; } = 1;

    private Stack<int>[] itemsStack;
    private SceneController sceneController;

    void Awake()
    {
        sceneController = new SceneController();
        itemsStack = new Stack<int>[3];
        for (int i = 0; i < itemsStack.Length; ++i)
            itemsStack[i] = new Stack<int>();
    }

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

    public void GetItems(int itemNumber)
    {
        itemsStack[stageNumber].Push(itemNumber);
    }

    public void LostItems()
    {
        itemsStack[stageNumber].Pop();
    }

    public void ChangeSence(string nextScene)
    {
        StartCoroutine(sceneController.LoadScenes(nextScene));
    }

    Stack<int>[] GetItemsStack()
    {
        return itemsStack;
    }


}
