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
    #endregion

    #region 类创建的对象的声明
    public PlayerInputControl inputControl;
    #endregion

    #region Vector2
    public Vector2 inputDirection;
    #endregion

    #region 变量
    public float speed;
    #endregion

    #region Awake()
    // 类的初始化,组件的获得
    private void Awake()
    {
        // 获得组件
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        // 类的初始化
        inputControl = new PlayerInputControl();
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
    }
    #endregion

    #region FixedUpdate()
    // FixedUpdate() 无论在什么设备上，都以一个固定的时钟的频率来进行执行
    // 通常与物理有关的，我们都放在 FixedUpdate()
    private void FixedUpdate()
    {
        // 人物移动
        Move();
    }
    #endregion

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

    #endregion
}
