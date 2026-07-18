using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Credits : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private string menuPrincipal = "Main_Menu";
    [SerializeField] private float tiempoEspera = 15f;

    private bool cambiandoEscena = false;

    private void Start()
    {
        Invoke(nameof(VolverAlMenu), tiempoEspera);
    }

    private void Update()
    {
        if (cambiandoEscena)
            return;

        // Cualquier tecla
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            VolverAlMenu();
            return;
        }

        // Clic izquierdo
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            VolverAlMenu();
            return;
        }

        // Toque en pantalla (Android)
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            VolverAlMenu();
        }
    }

    private void VolverAlMenu()
    {
        if (cambiandoEscena)
            return;

        cambiandoEscena = true;
        CancelInvoke(nameof(VolverAlMenu));

        if (Application.CanStreamedLevelBeLoaded(menuPrincipal))
        {
            SceneManager.LoadScene(menuPrincipal);
        }
        else
        {
            Debug.LogError($"La escena '{menuPrincipal}' no está agregada en Build Settings.");
        }
    }
}