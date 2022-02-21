using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    public int memberId = 0;
    public GameObject target;
    public bool touch = false;
    Rigidbody2D rig;
    public LayerMask groundLayer;

    private void FixedUpdate()
    {
        if (touch == true)
        {
            Followtarget();
        }
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(7, 7);
    }

    private float   Direction()
    {
        if (transform.position.x - target.transform.position.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
            return (1);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
            return (-1);
        }

    }

    public void Followtarget()
    {
        var heading = target.transform.position - this.transform.position;
        if (heading.sqrMagnitude > 3f)
        {
            transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * -1f, 0.5f, groundLayer);
            RaycastHit2D hitdia = Physics2D.Raycast(transform.position, new Vector2(1 * Direction(), 1) , 2f, groundLayer);
            if (hit || hitdia)
            {
                rig.velocity = Vector2.up * jumpPower;
            }
        }
        if (heading.sqrMagnitude > 100f)
        {
            transform.position = target.transform.position + new Vector3(-1 * Direction(), 0);
            rig.velocity = new Vector2(0, 0);
        }

    }

}
    