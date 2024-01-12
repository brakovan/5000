using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask enemyLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;

    public AnimationClip newWalkUp;
    public AnimationClip newWalkDown;
    public AnimationClip newWalkLeft;
    public AnimationClip newWalkRight;
    private AnimatorOverrideController animatorOverrideController;

    private float lastAttackTime;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float attackRadius = 5f;
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float attackCooldown = 5f;

    public bool isHavePalka = false;
    public bool isMove = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animatorOverrideController;

        if (isHavePalka)
            UpdatePlayerAnimations();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
            isMove = true;

        if (SceneManager.GetActiveScene().buildIndex == 3)
            isHavePalka = true;

        GetMovementAndAnimations();
        if (isHavePalka && Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            rb.velocity = movement * moveSpeed;
        }
    }

    public void UpdatePlayerAnimations()
    {
        animatorOverrideController["Player_Walk_Up"] = newWalkUp;
        animatorOverrideController["Player_Walk_Down"] = newWalkDown;
        animatorOverrideController["Player_Walk_Left"] = newWalkLeft;
        animatorOverrideController["Player_Walk_Right"] = newWalkRight;
    }

    private void GetMovementAndAnimations()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
        anim.SetBool("HasPalka", isHavePalka);
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            anim.SetTrigger("Attack");
            Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyLayer);
            foreach (var hit in hitEnemys)
            {
                hit.gameObject.GetComponent<HPManager>()?.TakeDamage(damage);
            }

            lastAttackTime = Time.time;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}

