using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpcionesMenu : MonoBehaviour
{
    
    public Slider sliderVolumen;  // Referencia al slider del volumen
    public TMP_InputField inputNombre; // Referencia al slider de cambio de nombre
    public TMP_Dropdown dropdownCalidad; // Referencia al slider de la calidad gráfica

    void Start()  // Se llama al inicio 
    {
        // Carga el volumen guardado o pone 1.0f si no hay valor guardado
        sliderVolumen.value = PlayerPrefs.GetFloat("volumen", 1f);
        // Carga el nombre guardado o usa "Jugador" si no hay ninguno
        inputNombre.text = PlayerPrefs.GetString("nombreJugador", "Jugador");
        // Carga la calidad guardada o usa la calidad actual de Unity
        dropdownCalidad.value = PlayerPrefs.GetInt("calidad", QualitySettings.GetQualityLevel());

        // Aplica los valores cargados en tiempo real
        CambiarVolumen(sliderVolumen.value);
        CambiarNombre(inputNombre.text);
        CambiarCalidad(dropdownCalidad.value);
    }

    public void CambiarVolumen(float valor)
    {
        AudioListener.volume = valor;  // Cambia el volumen global del juego
        PlayerPrefs.SetFloat("volumen", valor); // Guarda el valor para futuras sesiones
    } 

    public void CambiarNombre(string nuevoNombre)
    {
        PlayerPrefs.SetString("nombreJugador", nuevoNombre); // Guarda el nombre ingresado
    }

    public void CambiarCalidad(int calidadIndex)
    {
        QualitySettings.SetQualityLevel(calidadIndex); // Cambia la calidad en Unity (0 = baja, 1 = media, etc.)
        PlayerPrefs.SetInt("calidad", calidadIndex);  // Guarda la selección
    }
}