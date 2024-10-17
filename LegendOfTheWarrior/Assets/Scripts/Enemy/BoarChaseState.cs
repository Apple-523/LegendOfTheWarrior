using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaseState
{

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        Debug.Log("切换为chase状态");
    }
    public override void LogicUpdate()
    {

    }


    public override void PhysicsUpdate()
    {

    }

    public override void OnExit()
    {

    }

}
