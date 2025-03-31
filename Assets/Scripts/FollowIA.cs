using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowIA : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private int vida = 30; // Vida del enemigo

    private Transform player;
    private Animator enemyAnimator;
    private Rigidbody2D enemyRb;
    private Vector2 moveDirection;
    private float lastAttackTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnimator = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            moveDirection = (player.position - transform.position).normalized;
            enemyAnimator.SetFloat("Horizontal", moveDirection.x);
            enemyAnimator.SetFloat("Vertical", moveDirection.y);
            enemyAnimator.SetFloat("Speed", moveDirection.sqrMagnitude);
        }
        else
        {
            moveDirection = Vector2.zero;

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                enemyAnimator.SetTrigger("Atacar");
                lastAttackTime = Time.time;
            }
        }
    }

    private void FixedUpdate()
    {
        enemyRb.velocity = moveDirection * speed;
    }

    public void GolpearJugador()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            ControlerPlayer playerScript = player.GetComponent<ControlerPlayer>();
            if (playerScript != null)
            {
                playerScript.RecibirDano(attackDamage);
            }
        }
    }

    public void RecibirDano(int cantidad)
    {
        vida -= cantidad;

        if (vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("El enemigo ha muerto");
        enemyAnimator.SetTrigger("Muerte"); // Aseg�rate de tener una animaci�n de muerte
        Destroy(gameObject, 0.5f); // Elimina el enemigo despu�s de la animaci�n
    }
}
