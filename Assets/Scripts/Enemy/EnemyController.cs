using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private LayerMask playerLayer;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator anim;


    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float stopDistance = 5f;
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float attackRadius = 5f;
    [SerializeField]
    private float attackCooldown = 5f;

    private float lastAttackTime;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (player != null && playerController.isHavePalka)
        {
            Move();
        }
    }

    private void Update()
    {
        if (player != null)
        {
            Attack();
        }
    }

    private void Move()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            anim.SetBool("Attack", false);
            anim.SetBool("Run", true);

            rb.velocity = direction * moveSpeed;
            spriteRenderer.flipX = direction.x < 0;
        }
        else
        {
            anim.SetBool("Attack", true);
            anim.SetBool("Run", false);

            rb.velocity = Vector3.zero;
        }
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, attackRadius, playerLayer);
            if (hitPlayer != null)
            {
                hitPlayer.gameObject.GetComponent<HPManager>().TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }
}

