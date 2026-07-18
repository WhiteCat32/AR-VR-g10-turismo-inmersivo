using UnityEngine;
using Vuforia;

public class TourGuide : MonoBehaviour
{
    [Header("Configuración del Guía Turístico")]
    [SerializeField] private AudioClip tourAudioClip; // Audio de ESTE QR

    private Animator animator;
    private ObserverBehaviour observer;

    void Start()
    {
        animator = GetComponent<Animator>();
        observer = GetComponentInParent<ObserverBehaviour>();

        // Verificar que el AudioManager exista en la escena
        if (AudioManager.Instance == null)
        {
            Debug.LogError("❌ Falta el AudioManager en la escena. Crea un GameObject con ese script.");
        }

        if (observer != null)
        {
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnDestroy()
    {
        // Limpieza de eventos para evitar errores
        if (observer != null)
        {
            observer.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        // Cuando el QR es detectado y rastreado
        if (status.Status == Status.TRACKED)
        {
            Presentarse();
        }
        // (OPCIONAL) Cuando se pierde el QR, detenemos el audio global
        // para que no siga sonando si apuntas a una pared
        else if (status.Status == Status.NO_POSE || status.Status == Status.LIMITED)
        {
            // Solo paramos si este QR era el que estaba sonando (opcional)
            // Para simplificar, podemos parar siempre al perder rastreo
            // o dejarlo que siga hasta que otro QR lo reemplace.
            // Recomiendo dejarlo sonando hasta que otro lo pise, 
            // pero si quieres silencio al perder el QR, descomenta esta línea:
            // AudioManager.Instance.StopAudio();
        }
    }

    private void Presentarse()
    {
        // 1. Disparar la animación "Hablando" en el modelo 3D de este QR
        if (animator != null)
        {
            animator.SetTrigger("Talk");
        }

        // 2. Reproducir el audio mediante el MANAGER GLOBAL
        // Esto detiene automáticamente el audio del QR anterior y suena este nuevo
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayTourAudio(tourAudioClip);
        }
        else
        {
            Debug.LogWarning("⚠️ AudioManager no encontrado. No se puede reproducir el audio.");
        }
    }
}