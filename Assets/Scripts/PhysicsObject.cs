using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PhysicsObject : MonoBehaviour
{
    [SerializeField]
    private float minGroundNormalY = .65f;
    [SerializeField]
    private float gravityModifier = 1f;

    protected Vector2 targetVelocity;
    protected Vector2 velocity;
    //ground
    protected bool grounded;
    protected Vector2 groundNormal;
    //body
    protected Rigidbody2D rigidBody2D;
    // Physics layer filter
    protected ContactFilter2D contactFilter;
    //hitBuffer
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float shellRadius = 0.01f;
    protected const float minMoveDistance = 0.001f;

    void OnEnable()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //init contactFilter and set layerMask
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {
        /*
        if (!grounded)
        {
            if (velocity.y > 0)
            {
                velocity.y *= 0.5f;
            }
        }
        */
    }

    void FixedUpdate()
    {
        //calculate gravity acceleration
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        //move
        velocity.x = targetVelocity.x;
        //init grounded
        grounded = false;
        //calculate delta position.
        Vector2 deltaPosition = velocity * Time.deltaTime;
        //calculate with ground.
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        //move x
        Vector2 move = moveAlongGround * deltaPosition.x;
        Movement(move, Direction.x);
        //move y
        move = Vector2.up * deltaPosition.y;
        Movement(move, Direction.y);
    }

    void Movement(Vector2 move, Direction dir)
    {
        float distance = move.magnitude;
        if (distance > minMoveDistance)
        {
            int count = rigidBody2D.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            //init hitbufferlist    
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }
            //get ground normal vector and distance
            for (int i = 0; i < hitBufferList.Count; i++)
            {
                //get ground normalVector from hitObj(ground)
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (dir == Direction.y)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                // calculate velocity when hitObj is opposite to velocity vector
                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }
                //distance between hitObj and obj
                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
            //move obj
            rigidBody2D.position = rigidBody2D.position + move.normalized * distance;
        }
    }

    [Flags]
    private enum Direction
    {
        x,
        y
    }
}
