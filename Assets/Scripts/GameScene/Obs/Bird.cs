using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bird : Obstacle
{
    BoxCollider2D col;

    protected override void Awake()
    {
        base.Awake();
        col = GetComponent<BoxCollider2D>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        col.enabled = true;
    }

    public override void Hit()
    {
        base.Hit();
        animator.SetBool("hit", true);
        col.enabled = false;
        StartCoroutine(Kill());
    }

    IEnumerator Kill()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        gameObject.SetActive(false);
    }
}
