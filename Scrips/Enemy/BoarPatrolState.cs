using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaceState
{

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }
    public override void LogicUpdate()
    {
        // 发现Player切换到 chaseState(追击状态)
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }

        // 撞墙转向 
        // !currentEnemy.pC.isFloor 判断前方有没有悬崖
        if (!currentEnemy.pC.isFloor ||
         (currentEnemy.pC.touchLeftWall && currentEnemy.faceDir.x < 0) ||
           (currentEnemy.pC.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.anim.SetBool("isWalk", false);
        }
        else
        {
            currentEnemy.anim.SetBool("isWalk", true);
        }
    }


    public override void PhysicsUpdate()
    {

    }


    public override void OnExit()
    {
        currentEnemy.anim.SetBool("isWalk", false);
        Debug.Log("exit");
    }
}
