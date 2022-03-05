using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject player;

    Transform target;
    public string targetName = "Player";
    public bool enemyStart = true;
    private float speed = 0.4f;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetName).GetComponent<Transform>();
    }
    private void Update()
    {
        if (enemyStart == true)
            EnermyFollow();
    }

    public void EnermyFindTarget(string nextTarget)
    {
        targetName = nextTarget;
        target = GameObject.FindGameObjectWithTag(targetName).GetComponent<Transform>();
    }

    private void EnermyFollow()
    {
        var heading = target.position - this.transform.position;
        Debug.Log(targetName);
        if (heading.sqrMagnitude > 0.3f)
        {
            // 속도 수정 필요
            transform.position = Vector2.Lerp(transform.position, target.position, Time.deltaTime * speed);
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag(targetName).gameObject);
            player.GetComponent<PlayerMember>().catchMember();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("cathc Target" + targetName + " ?" + collision.gameObject.tag);

        //if (collision.gameObject.tag == targetName)
        //{
        //}
    }
}
