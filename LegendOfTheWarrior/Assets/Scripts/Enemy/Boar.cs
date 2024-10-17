using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{

    protected override void Awake()
    {
        base.Awake();
        walkAroundState = new BoarWalkAroundState();
        chaseState = new BoarChaseState();
    }

    public override void Move()
    {
        base.Move();
        animator.SetBool("Walk", true);
    }
}
