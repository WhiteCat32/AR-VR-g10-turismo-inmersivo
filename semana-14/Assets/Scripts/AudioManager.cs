using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        // Patrón Singleton: Solo debe existir uno en toda la escena
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas si quieres
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Crear el AudioSource global si no existe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        audioSource.playOnAwake = false;
        audioSource.loop = false; // Los audios del tour son cortos, sin bucle
    }

    // Método principal para reproducir el audio del QR escaneado
    public void PlayTourAudio(AudioClip clip)
    {
        if (audioSource == null) return;

        // 🛑 ¡Aquí está la clave! Detenemos CUALQUIER sonido que esté sonando
        audioSource.Stop();
        
        // Si el clip existe, lo asignamos y reproducimos (el nuevo reemplaza al viejo)
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    // Método opcional para silenciar si se pierde el rastreo
    public void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}