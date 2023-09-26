using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{

    private CapsuleCollider2D cC;

    [Header("检测参数")]
    public bool manual;
    // 脚底位移差值
    public Vector2 buttomOffset;
    // 检测的误差范围, 0.2即可
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public float checkRaduis;
    // 需要检测的图层
    public LayerMask FloorLayer;

    [Header("状态")]
    public bool isFloor;
    public bool touchLeftWall;
    public bool touchRightWall;

    private void Awake()
    {
        cC = GetComponent<CapsuleCollider2D>();

        if (!manual)
        {
            rightOffset = new Vector2((cC.bounds.size.x + cC.offset.x) / 2, cC.bounds.size.y / 2);
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
        }
    }

    private void Update()
    {
        Check();
    }

    public void Check()
    {
        // 检测地面
        // transform.position 自身的坐标
        isFloor = Physics2D.OverlapCircle((Vector2)transform.position + buttomOffset, checkRaduis, FloorLayer);

        // 墙体判断
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, FloorLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, FloorLayer);
    }

    // 当物体被选中时，在地图上进行绘制
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + buttomOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}
