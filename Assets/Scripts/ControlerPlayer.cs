using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Vector2 moveInput;
    private Animator playerAnimator;
    private Rigidbody2D playerRb;

    [SerializeField] private GameObject Ataque;
    [SerializeField] private GameObject Sangre;
    [SerializeField] private int vida = 100;
    [SerializeField] private int dañoAtaque = 1; // Daño que el jugador hace al enemigo
    [SerializeField] private float attackRange = 1.5f; // Rango de ataque

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        Ataque.SetActive(false);
        Sangre.SetActive(false);
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(RealizarAtaque());
        }
    }

    private void FixedUpdate()
    {
        playerRb.velocity = moveInput * speed;
    }

    IEnumerator RealizarAtaque()
    {
        Ataque.SetActive(true);
        GolpearEnemigo(); // Golpea al enemigo cuando ataca
        yield return new WaitForSeconds(0.3f);
        Ataque.SetActive(false);
    }

    public void RecibirDaño(int cantidad)
    {
        vida -= cantidad;

        if (vida <= 0)
        {
            Debug.Log("Jugador ha muerto");
            // Aquí puedes poner una animación de muerte o reiniciar el nivel
        }

        StartCoroutine(MostrarSangre());
    }

    IEnumerator MostrarSangre()
    {
        Sangre.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Sangre.SetActive(false);
    }

    void GolpearEnemigo()
    {
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D enemigo in enemigos)
        {
            if (enemigo.CompareTag("Enemigo"))
            {
                FollowIA enemigoScript = enemigo.GetComponent<FollowIA>();
                if (enemigoScript != null)
                {
                    enemigoScript.RecibirDaño(dañoAtaque);
                }
            }
        }
    }
}
