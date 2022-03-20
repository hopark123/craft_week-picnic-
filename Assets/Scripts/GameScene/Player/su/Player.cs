using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float bounceForce;

    public bool IsGround { get; private set; }
    public bool IsAlive { get; private set; }
    
    public int Jumpcnt { get; private set; } = 0;
    private bool IsJump = false;

    private Vector2 standSize;
    private Vector2 slideSize;
    private Vector2 standOffset;
    private Vector2 slideOffset;
    
    private ContactPoint2D contact;
    
    private Rigidbody2D rd;
    private Animator animator;
    private BoxCollider2D boxCollider;

    void OnEnable()
    {
        IsAlive = true;
        IsGround = false;
    }

    void OnDisable()
    {
        rd.velocity = Vector2.zero;
        IsAlive = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        standSize = boxCollider.size;
        standOffset = boxCollider.offset;
        slideSize = new Vector2(standSize.x, standSize.y / 2);
        slideOffset = new Vector2(standOffset.x, standOffset.y - boxCollider.size.y / 4);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move
        if (IsAlive)
            this.transform.Translate(Vector3.right * moveSpeed);

        if (IsJump)
        {
            JumpAction();
            IsJump = false;
        }
    }


    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(contact.point, contact.point + contact.normal);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Hit"))
        {
            gameManager.Kill();
            return;
        }

        contact = collision.contacts[0];
#if true
        float projection = Vector2.Dot(Vector2.down, contact.normal);

        if (projection <= -0.3f && projection >= -1)// 땅의 윗부분에 충돌했는지
        {
            //init jump
            IsGround = true;
            Jumpcnt = 0;
            animator.SetBool("jump", false);
        }
        else
        {
            Debug.Log("contact" + contact.point + "player" + transform.position);
            gameManager.Kill();
        }
#else
        if (transform.position.y > contact.point.y && Mathf.Abs(transform.position.x - contact.point.x) <= standSize.x / 2 * transform.localScale.x + 0.01f)
        {
            IsGround = true;
            Jumpcnt = 0;
            animator.SetBool("jump", false);
        }
        else
        {
            Debug.Log("contact" + contact.point + "player" + transform.position);
            gameManager.Kill();
        }
#endif
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsGround) IsGround = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //item.
    }

    private void JumpAction()
    {
        if (Jumpcnt < 2)
        {
            rd.velocity = new Vector2(rd.velocity.x, 0);
            rd.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            Jumpcnt++;
            animator.SetBool("jump", true);
        }
    }

    public void Jump() => IsJump = true;

    public void Slide()
    {
        boxCollider.offset = slideOffset;
        boxCollider.size = slideSize;
        animator.SetBool("slide", true);
    }

    public void Stand()
    {
        boxCollider.offset = standOffset;
        boxCollider.size = standSize;
        animator.SetBool("slide", false);
    }
}
