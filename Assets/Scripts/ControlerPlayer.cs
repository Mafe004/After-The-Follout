using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // Velocidad del jugador
    private Vector2 moveInput; // Entrada de movimiento del jugador
    private Animator playerAnimator; // Controlador de animaciones
    private Rigidbody2D playerRb; // Componente de física del jugador

    [SerializeField] private GameObject Ataque; // Objeto de ataque
    [SerializeField] private GameObject Sangre; // Objeto para mostrar sangre
    [SerializeField] private int vida = 100; // Vida del jugador
    [SerializeField] private int danoAtaque = 1; // Dano que el jugador hace al enemigo
    [SerializeField] private float attackRange = 1.5f; // Rango de ataque

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        Ataque.SetActive(false);
        Sangre.SetActive(false);

        // Buscar el punto de entrada al iniciar la escena
        GameObject puntoEntrada = GameObject.Find("DatosJuego.puntoEntrada");
        if (puntoEntrada != null)
        {
            transform.position = puntoEntrada.transform.position;
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto PuntoEntradaDesierto");
        }
    }

    void Update()
    {
        // Obtener la entrada del jugador
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        // Actualizar animaciones
        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);

         // Verificar ataque
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(RealizarAtaque());
        }
    }

    private void FixedUpdate()
    {
        // Aplicar movimiento al jugador
        playerRb.velocity = moveInput * speed;
    }

    IEnumerator RealizarAtaque()
    {
        Ataque.SetActive(true);
        GolpearEnemigo(); // Golpea al enemigo cuando ataca
        yield return new WaitForSeconds(0.3f);
        Ataque.SetActive(false);
    }

    public void RecibirDano(int cantidad)
    {
        // Reducir vida del jugador
        vida -= cantidad;

        if (vida <= 0)
        {
            Debug.Log("Jugador ha muerto");
            // Aqui puedes poner una animacion de muerte o reiniciar el nivel
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
        // Detectar enemigos cercanos para golpearlos
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D enemigo in enemigos)
        {
            if (enemigo.CompareTag("Enemigo"))
            {
                FollowIA enemigoScript = enemigo.GetComponent<FollowIA>();
                if (enemigoScript != null)
                {
                    enemigoScript.RecibirDano(danoAtaque);
                }
            }
        }
    }
}
