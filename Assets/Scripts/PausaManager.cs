using UnityEngine;
using UnityEngine.SceneManagement; // Para cambiar de escena

public class PausaManager : MonoBehaviour
{
    // Referencia al panel de pausa 
    public GameObject panelPausa;

    // Variable para saber si el juego está pausado o no
    private bool juegoPausado = false;

    void Update()
    {
        // Detecta si el jugador presiona la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si ya está pausado, reanudar; si no, pausar
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    // Método que pausa el juego
    public void Pausar()
    {
        panelPausa.SetActive(true);   // Mostrar el panel
        Time.timeScale = 0f;          // Pausar el tiempo del juego
        juegoPausado = true;          // Cambiar el estado
    }

    // Método que reanuda el juego
    public void Reanudar()
    {
        panelPausa.SetActive(false);  // Ocultar el panel
        Time.timeScale = 1f;          // Reanudar el tiempo del juego
        juegoPausado = false;         // Cambiar el estado
    }

    // Método para ir al menú principal
    public void IrAlMenu()
    {
        Time.timeScale = 1f;          // Asegurarse de reanudar el tiempo
        SceneManager.LoadScene("MenuInicial"); // Cambia a la escena de menú (asegúrate que se llame así)
    }

    // Método para salir del juego
    public void SalirDelJuego()
    {
        Application.Quit();           // Cierra el juego (funciona fuera del editor)
        Debug.Log("Saliendo del juego..."); // Para pruebas en el editor
    }
}