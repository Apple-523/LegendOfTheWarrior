using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rigidbody2d;
    protected Animator animator;


    [Header("基本参数")]
    public float normalSpeed;
    public float chaseSpeed;

    public float currentSpeed;

    public Vector3 faceDirector;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        faceDirector = new Vector3(-transform.localScale.x, 0, 0);
    }

    private void FixedUpdate() {
        Move();
    }

    public virtual void Move()
    {
        rigidbody2d.velocity = new Vector2(currentSpeed * faceDirector.x * Time.deltaTime, faceDirector.y); 
    }
}
