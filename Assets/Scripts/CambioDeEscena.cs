using UnityEngine; // Acceso a los componentes y funciones de Unity
using UnityEngine.SceneManagement; // Permite cambiar de escenas

// Esta clase se debe adjuntar a un objeto con Collider 2D
public class CambioDeEscena : MonoBehaviour
{
    // Nombre de la escena a la que se cambiará cuando el jugador entre al trigger
    public string nombreEscenaDestino;
    public string nombrePuntoEntradaDestino;

    // Se ejecuta automáticamente cuando otro Collider2D entra en este objeto (y este tiene IsTrigger activado)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Compara si el objeto que entró tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
             // Guardar el nombre del punto de entrada antes de cambiar de escena
            DatosJuego.puntoEntrada = nombrePuntoEntradaDestino;

            // Cambia la escena usando el nombre que se puso en el campo desde el Inspector
            SceneManager.LoadScene(nombreEscenaDestino);
        }
    }
}