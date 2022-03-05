using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoal : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collision vector 비교
        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("end game");
        }

    }
}
