using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class Car : Obstacle
{
    EdgeCollider2D col = null;
    GameObject particle = null;
    public AudioClip CarClip;

    protected override void Awake()
    {
        base.Awake();
        col = GetComponent<EdgeCollider2D>();
        particle = transform.GetChild(0).gameObject;

        jumpAdd = 2;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (col != null)
            col.enabled = true;
        if (particle != null)
            particle.SetActive(false);
    }

    public override void Hit()
    {
        base.Hit();
        if (animator.GetBool("hit") == false)
            SoundController.instance.SFXPlay("Car", CarClip);
        if (animator != null) {
            animator.SetBool("hit", true);
        }
        if (animator != null)
            particle.SetActive(true);
        StartCoroutine(Kill());
    }

    IEnumerator Kill()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        if (col != null)
            col.enabled = false;
        if (!IsAlive)
            gameObject.SetActive(false);
    }
}
