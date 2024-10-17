using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarWalkAroundState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;

    }

    public override void LogicUpdate()
    {
        if (currentEnemy.FoundPlayer())
        {
            // 发现敌人，切换到chase
            currentEnemy.SwitchState(NPCState.Chase);
            return;
        }
        // 日常巡逻来回走的逻辑
        if (!currentEnemy.phyciseCheck.isOnGround)
        {
            Debug.Log("没在地面上");
        }
        if (!currentEnemy.phyciseCheck.isOnGround || currentEnemy.phyciseCheck.isTouchLeftWall && currentEnemy.faceDirector.x < 0 || // 撞左边墙时，脸要朝左侧
                currentEnemy.phyciseCheck.isTouchRightWall && currentEnemy.faceDirector.x > 0)
        {
            currentEnemy.isWaiting = true;
            currentEnemy.rigidbody2d.velocity = Vector2.zero;
            currentEnemy.animator.SetBool(currentEnemy.kAnimWalk, false);
        }
    }

    public override void PhysicsUpdate()
    {

    }
    public override void OnExit()
    {
        currentEnemy.animator.SetBool(currentEnemy.kAnimWalk, false);
        Debug.Log("退出巡逻状态");
    }

}
