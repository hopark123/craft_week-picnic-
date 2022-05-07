using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Cat : Obstacle
{
    BoxCollider2D col = null;

    protected override void Awake()
    {
        base.Awake();
        col = GetComponent<BoxCollider2D>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (col != null)
            col.enabled = true;
    }

    public override void Hit()
    {
        base.Hit();
        if (animator != null)
            animator.SetBool("hit", true);
        if (col != null)
            col.enabled = false;
        StartCoroutine(Kill());
    }

    IEnumerator Kill()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        gameObject.SetActive(false);
    }
}
