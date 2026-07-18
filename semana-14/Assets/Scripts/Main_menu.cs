using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Main_menu : MonoBehaviour
{
    [Header("Escenas")]
    [SerializeField] private string escenaQR = "QRScanner";
    [SerializeField] private string escenaCreditos = "Credits";

    /// <summary>
    /// Carga la escena del escáner QR.
    /// </summary>
    public void Play()
    {
        CargarEscena(escenaQR);
    }

    /// <summary>
    /// Carga la escena de créditos.
    /// </summary>
    public void Creditos()
    {
        CargarEscena(escenaCreditos);
    }

    /// <summary>
    /// Método general para cargar escenas.
    /// </summary>
    /// <param name="nombreEscena">Nombre de la escena.</param>
    private void CargarEscena(string nombreEscena)
    {
        if (Application.CanStreamedLevelBeLoaded(nombreEscena))
        {
            Debug.Log($"Cargando escena: {nombreEscena}");
            SceneManager.LoadScene(nombreEscena);
        }
        else
        {
            Debug.LogError($"La escena '{nombreEscena}' no está agregada en Build Settings.");
        }
    }

    /// <summary>
    /// Cierra la aplicación.
    /// </summary>
    public void Salir()
    {
        Debug.Log("Cerrando aplicación...");

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}