using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 状态机
public abstract class BaceState 
{
    // 状态机的作用对象
    protected Enemy currentEnemy;

    public abstract void OnEnter(Enemy enemy);
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void OnExit();
}
