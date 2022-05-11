using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerControler : MonoBehaviour
{
    //player
    PlayerModel pModel;
    PlayerView pView;
    //unity
    Rigidbody2D rd = null;
    BoxCollider2D col = null;
    //controller
    [SerializeField]
    private Button jumpButton;
    [SerializeField]
    private Button slideButton;
    [SerializeField]
    private GameControler gameControler;
    [SerializeField]
    private Transform respawnPoint;
    
    //private field
    private bool jmpOrder = false;

    //Audio Clip
    public AudioClip JumpClip;
    public AudioClip ItemClip;
    public AudioClip SlideClip;
    public AudioClip GoalClip;
    public AudioClip DeadClip;


    void Awake()
    {
        //player
        pView = GetComponent<PlayerView>();
        pModel = new PlayerModel();
        //Unity
        rd = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //init PlayerBoxSize
        pModel.InitPlayerBox(col.size, col.offset);
        //register controller button
        EventTrigger jmpTrigger = jumpButton.gameObject.AddComponent<EventTrigger>();
        //add pointerDown event to jmpTrigger
        var jmpPointerDown = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };
        jmpPointerDown.callback.AddListener((eventData) => PressJmpButton());
        jmpTrigger.triggers.Add(jmpPointerDown);

        EventTrigger slideTrigger = slideButton.gameObject.AddComponent<EventTrigger>();
        //add pointerDown event to slideTrigger
        var slidePointerDown = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };
        slidePointerDown.callback.AddListener((eventData) => PressSlideButtonDown());
        slideTrigger.triggers.Add(slidePointerDown);

        var slidePointerUp = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerUp
        };
        slidePointerUp.callback.AddListener((eventData) => PressSlideButtonUp());
        slideTrigger.triggers.Add(slidePointerUp);
    }
    
    void OnEnable()
    {
        pModel.IsAlive = true;
        jmpOrder = false;
    }

    void OnDisable()
    {
        pModel.IsAlive = false;
        pView.HitEnd();
    }

    // Update is called once per frame
    void Update()
    {
        if (pModel.IsAlive)
            Move();
    }

    void FixedUpdate()
    {
        if (pModel.IsAlive && jmpOrder)
        {
            Jump();
            jmpOrder = false;
        }
    }

    void Move()
    {
        if (pModel.IsAlive)
            transform.Translate((pModel.MoveSpeed + GameModel.StageNumber) * Time.deltaTime * Vector2.right);
    }

    void Jump()
    {
        if (pModel.Jump())
        {
            pView.Jump();
            //control
            if (rd != null)
            {
                SoundController.instance.SFXPlay("Jump", JumpClip);
                rd.velocity = new Vector2(rd.velocity.x, 0);
                rd.AddForce(Vector2.up * pModel.JumpPower, ForceMode2D.Impulse);
            }
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        pView.HitEnd();
        gameControler.RespawnMap();
        transform.position = respawnPoint.position;
        pModel.IsAlive = true;
        SoundController.instance.BgSoundRestart();
    }

    private void Hit()
    {
        if (!pModel.IsAlive)
            return;
        gameControler.Dead();
        SoundController.instance.SFXPlay("Dead", DeadClip);
        pModel.IsAlive = false;
        jmpOrder = false;
        pView.Hit();
        SoundController.instance.BgSoundStop();
        StartCoroutine(Respawn());
    }

    // user control

    void PressJmpButton() => jmpOrder = true;

    void PressSlideButtonDown()
    {
        GameObject obj1 = GameObject.Find("Slidesound");
        if (pModel.Slide())
        {
            if (!obj1)
                SoundController.instance.SFXPlay("Slide", SlideClip);
            pView.Slide();
            //control
            (col.size, col.offset) = pModel.PlayerBox;
        }
    }
    
    void PressSlideButtonUp()
    {
        GameObject obj1 = GameObject.Find("Slidesound");
        if (pModel.IsSlide)
        {
            if (obj1)
                Destroy(obj1);
            pModel.IsSlide = false;
            pView.SlideEnd();
            //control
            (col.size, col.offset) = pModel.PlayerBox;
        }
    }

    //Collision Event Control

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Hit"))
        {
            Hit();
            return;
        }
        ContactPoint2D contact = collision.contacts[0];
        float projection = Vector2.Dot(Vector2.down, contact.normal);
        if (projection <= -0.3f && projection >= -1)// 땅의 윗부분에 충돌했는지
        {
            //init jump
            pModel.IsGround = true;
            pView.JumpEnd();

            if (collision.transform.CompareTag("Obs"))
            {
                Obstacle obs = collision.transform.GetComponent<Obstacle>();
                obs.Hit();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D contact;

        for (int i = 0; i < collision.contactCount; i++)
        {
            contact = collision.contacts[i];
            float projection = Vector2.Dot(Vector2.down, contact.normal);

            if (projection > -0.3f || projection < -1)// 땅의 윗부분에 충돌했는지
            {
                Hit();
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (pModel.IsGround)
            pModel.IsGround = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("item"))
        {
            SoundController.instance.SFXPlay("Item", ItemClip);
            gameControler.EatItem(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Goal"))
        {
            SoundController.instance.SFXPlay("Goal", GoalClip);
            gameControler.Goal();
        }
        if (collision.transform.CompareTag("Slow"))
        {
            pModel.IsSlow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Slow"))
        {
            pModel.IsSlow = false;
        }
    }
}
