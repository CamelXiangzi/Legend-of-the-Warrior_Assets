using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public PhysicsCheck pC;

    [Header("基本参数")]
    public float normalSpeed;
    // 追击速度
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public Transform attacker;
    public float hurtForce;


    [Header("计时器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;

    public float lostTime;
    public float lostTimeCounter;


    [Header("状态")]
    public bool isHurt;
    public bool isDead;

    [Header("状态机")]
    protected BaceState currentState;
    // 巡逻
    protected BaceState patrolState;
    // 追击
    protected BaceState chaseState;

    [Header("检测")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pC = GetComponent<PhysicsCheck>();
    }


    // 启动状态机
    private void OnEnable()
    {
        // 当前状态，设置成巡逻状态
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void Start()
    {
        currentSpeed = normalSpeed;
        waitTimeCounter = waitTime;
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);

        // 执行状态机里的LogicUpdate()
        currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isDead && !wait)
        {

            Move();
        }

        // 同理，执行状态机里的PhysicsUpdate()
        currentState.PhysicsUpdate();
        TimeCounter();
    }

    private void OnDisable()
    {
        // 同理，执行状态机里的OnExit()
        currentState.OnExit();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }

    // 计时器
    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);

                // 改变地面检测点
                if (transform.localScale.x > 0)
                {
                    pC.buttomOffset.x = pC.buttomOffsetX;
                }
                if (transform.localScale.x < 0)
                {
                    pC.buttomOffset.x = -pC.buttomOffsetX;
                }
                pC.Check();
                // Debug.Log(pC.isFloor);

                wait = false;
            }
        }

        // 仇恨值递减
        if (!FoundPlayer() && lostTimeCounter > 0)
        {
            lostTimeCounter -= Time.deltaTime;
        }
    }

    public bool FoundPlayer()
    {
        // 发射一个方形的判断器
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, checkDistance, attackLayer);
    }

    public void OnTakeDamage(Transform attackerTrans)
    {
        attacker = attackerTrans;
        // 转身
        if (attackerTrans.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (attackerTrans.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // 受伤击退
        isHurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;

        // 受伤后将惯性停下来、
        rb.velocity = new Vector2(0, rb.velocity.y);

        StartCoroutine(OnHurt(dir));
    }

    // 协程
    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.45f);
        isHurt = false;
    }

    public void OnDie()
    {
        gameObject.layer = 2;
        anim.SetBool("isDead", true);
        isDead = true;
        DestroyAfterAnimation();
    }

    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }

    // 切换状态
    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _ => null,
        };

        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(checkDistance * -transform.localScale.x, 0), 0.2f);
    }
}
