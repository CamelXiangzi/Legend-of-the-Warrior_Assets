using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{

    [Header("检测参数")]
    // 脚底位移差值
    public Vector2 buttomOffset;
    // 检测的误差范围, 0.2即可
    public float checkRaduis;
    // 需要检测的图层
    public LayerMask FloorLayer;

    [Header("状态")]
    public bool isFloor;



    private void Update()
    {
        Check();
    }

    public void Check()
    {
        // 检测地面
        // transform.position 自身的坐标
        isFloor = Physics2D.OverlapCircle((Vector2)transform.position + buttomOffset, checkRaduis, FloorLayer);
    }

    // 当物体被选中时，在地图上进行绘制
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + buttomOffset, checkRaduis);
    }
}
