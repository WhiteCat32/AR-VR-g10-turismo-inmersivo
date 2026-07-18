using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI - Menú de Pausa")]
    [SerializeField] private GameObject pauseMenuPanel; // El panel que contiene "Continuar" y "Salir"

    [Header("Configuración de Escenas")]
    [SerializeField] private string mainMenuSceneName = "Main_Menu"; // 👈 Nombre exacto de tu menú

    private bool isPaused = false;

    void Start()
    {
        // Al iniciar, aseguramos que el menú esté oculto y el juego corriendo
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);
        
        ResumeGame(); // Reseteamos el estado por si acaso
    }

    // ------------------------------------------------------------
    // 1. PAUSAR (Botón "Pausa" de la UI)
    // ------------------------------------------------------------
    public void PauseGame()
    {
        if (isPaused) return; 

        isPaused = true;

        // 🛑 Congelar animaciones y físicas
        Time.timeScale = 0f;

        // 🔇 Pausar el audio del guía turístico
        AudioListener.pause = true;

        // 📱 Mostrar el menú de opciones
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(true);
    }

    // ------------------------------------------------------------
    // 2. CONTINUAR (Botón "Continuar / Seguir en la App")
    // ------------------------------------------------------------
    public void ResumeGame()
    {
        if (!isPaused) return; 

        isPaused = false;

        // ▶️ Descongelar el tiempo
        Time.timeScale = 1f;

        // 🔊 Reanudar el audio donde lo dejó
        AudioListener.pause = false;

        // 📱 Ocultar el menú de pausa
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);
    }

    // ------------------------------------------------------------
    // 3. SALIR AL MENÚ INICIO (Botón "Salir al Inicio")
    // ------------------------------------------------------------
    public void ExitToMainMenu()
    {
        // ¡CRUCIAL! Restauramos el tiempo y el audio ANTES de cargar
        Time.timeScale = 1f;
        AudioListener.pause = false;

        // Cargamos la escena "Main_Menu" usando la variable configurable
        SceneManager.LoadScene(mainMenuSceneName);
    }

    // ------------------------------------------------------------
    // (OPCIONAL) 4. BOTÓN PARA IR DIRECTAMENTE A CRÉDITOS
    // Si quieres un botón en el menú de pausa que diga "Créditos"
    // ------------------------------------------------------------
    public void GoToCredits()
    {
        // Restauramos el tiempo antes de irnos
        Time.timeScale = 1f;
        AudioListener.pause = false;
        
        SceneManager.LoadScene("Credits");
    }

    // ------------------------------------------------------------
    // (OPCIONAL) 5. CERRAR APLICACIÓN
    // ------------------------------------------------------------
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}