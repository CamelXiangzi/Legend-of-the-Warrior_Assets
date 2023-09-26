using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 导入InputSystem
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class PlayerControl : MonoBehaviour
{
    #region 组件的声明
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public PhysicsCheck pc;
    public PlayerAnimation pA;
    public CapsuleCollider2D coll;
    #endregion

    #region 类创建的对象的声明
    public PlayerInputControl inputControl;
    #endregion

    #region Vector2
    public Vector2 inputDirection;
    #endregion

    #region 变量
    [Header("基础参数")]
    public float speed;
    public float jumpForce;
    public float HurtForce;
    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    #endregion

    [Header("物理材质")]
    public PhysicsMaterial2D wall;
    public PhysicsMaterial2D floor;


    #region Awake()
    // 类的初始化,组件的获得
    private void Awake()
    {
        // 获得组件
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pc = GetComponent<PhysicsCheck>();
        pA = GetComponent<PlayerAnimation>();
        coll = GetComponent<CapsuleCollider2D>();
        // 类的初始化
        inputControl = new PlayerInputControl();
        // 事件的注册
        inputControl.Gameplay.Jump.started += Jump;
        inputControl.Gameplay.J.started += PlayerAttack;
    }
    #endregion

    #region Start()
    // 变量的初始化
    void Start()
    {

    }
    #endregion

    #region Update()
    // 游戏每帧都会执行
    void Update()
    {
        // 获得移动方向
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
        // 切换物理材质

    }
    #endregion

    #region FixedUpdate()
    // FixedUpdate() 无论在什么设备上，都以一个固定的时钟的频率来进行执行
    // 通常与物理有关的，我们都放在 FixedUpdate()
    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
        {
            // 人物移动
            Move();
        }
    }
    #endregion


    // // 测试
    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     Debug.Log(other.name);
    // }




    #region 方法
    // 启动 InputSystem 系统
    private void OnEnable()
    {
        inputControl.Enable();
    }

    // 关闭 InputSystem 系统
    private void OnDisable()
    {
        inputControl.Disable();
    }

    // 人物移动
    private void Move()
    {
        // 人物的移动速度
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        // 人物翻转
        int faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
        {
            faceDir = 1;
        }
        if (inputDirection.x < 0)
        {
            faceDir = -1;
        }
        // 三元运算符
        // faceDir = (inputDirection.x > 0) ? 1 : -1;
        // 不可用三元运算符，三元运算符只能用在 只有两种判断的情况下使用
        // 上面的情况，有 >0 <0 =0 三种情况，当=0时，三元运算符会默认赋值false的情况
        transform.localScale = new Vector3(faceDir, 1, 1);
    }


    // 跳
    private void Jump(InputAction.CallbackContext context)
    {
        if (pc.isFloor)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

        }
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        // 把速度停下来
        rb.velocity = Vector2.zero;
        // normalized 归一化，我只需要确定方向，而不是要具体的值
        Vector2 dir = new Vector2((transform.position.x - attacker.transform.position.x), 0).normalized;

        rb.AddForce(HurtForce * dir, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        // 关闭玩家的控制权
        inputControl.Gameplay.Disable();
    }


    private void PlayerAttack(InputAction.CallbackContext context)
    {
        pA.PlayAttack();
        isAttack = true;
    }

    // 切换物理材质(摩擦力)
    private void CheckState()
    {
        coll.sharedMaterial = pc.isFloor ? floor : wall;
    }

    #endregion

}
