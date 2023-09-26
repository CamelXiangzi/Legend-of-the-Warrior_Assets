using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected PhysicsCheck pC;

    [Header("基本参数")]
    public float normalSpeed;
    // 追击速度
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;


    [Header("计时器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pC = GetComponent<PhysicsCheck>();
    }

    private void Start()
    {
        currentSpeed = normalSpeed;
        waitTimeCounter = waitTime;
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);

        if ((pC.touchLeftWall && faceDir.x < 0) || (pC.touchRightWall && faceDir.x > 0))
        {
            wait = true;
            anim.SetBool("isWalk", false);
        }
        TimeCounter();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }

    protected void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }
    }
}
