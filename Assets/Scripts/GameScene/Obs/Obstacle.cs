using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Obstacle : MonoBehaviour
{
    protected Animator animator;

    public bool IsAlive { get; private set; }
    public uint jumpAdd { get; protected set; } = 1;
    
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        IsAlive = true;
    }

    public virtual void Hit()
    {
        IsAlive = false;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }
}
