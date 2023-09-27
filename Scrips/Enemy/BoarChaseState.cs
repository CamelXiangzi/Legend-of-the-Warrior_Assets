using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaceState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        // Debug.Log("Chase");
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("isRun", true);
    }
    public override void LogicUpdate()
    {
        if (currentEnemy.lostTimeCounter <= 0)
        {
            // 仇恨值为0后，退出追击状态
            currentEnemy.SwitchState(NPCState.Patrol);
        }

        if (!currentEnemy.pC.isFloor ||
           (currentEnemy.pC.touchLeftWall && currentEnemy.faceDir.x < 0) ||
             (currentEnemy.pC.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 1, 1);
        }
    }



    public override void PhysicsUpdate()
    {

    }
    public override void OnExit()
    {
        currentEnemy.anim.SetBool("isRun", false);
        // 重置时间
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
    }
}
