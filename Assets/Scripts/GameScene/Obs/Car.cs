using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class Car : Obstacle
{
    //BoxCollider2D col;
    //PolygonCollider2D col;
    EdgeCollider2D col;
    GameObject particle;

    protected override void Awake()
    {
        base.Awake();
        //col = GetComponent<BoxCollider2D>();
        //col = GetComponent<PolygonCollider2D>();
        col = GetComponent<EdgeCollider2D>();
        particle = transform.GetChild(0).gameObject;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        col.enabled = true;
        particle.SetActive(false);
    }

    public override void Hit()
    {
        base.Hit();
        //animator.SetBool("hit", true);
        particle.SetActive(true);
        StartCoroutine(Kill());
    }

    IEnumerator Kill()
    {
        Debug.Log(IsAlive);
        yield return new WaitForSecondsRealtime(1.5f);
        col.enabled = false;
        if (!IsAlive)
            gameObject.SetActive(false);
    }
}
