using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Controlador de audio global para gestionar música de fondo y efectos de sonido.
/// Implementa el patrón Singleton para mantener una única instancia entre escenas.
/// Permite reproducir, silenciar y ajustar volúmenes de música y efectos, así como gestionar clips específicos.
/// </summary>
public class AudioCtrl : MonoBehaviour
{
    // Instancia estática para el patrón Singleton
    public static AudioCtrl instance;

    // Mezcladores de audio para música y efectos
    public AudioMixer musicMixer;
    public AudioMixer soundMixer;

    // Fuentes de audio para reproducir música y efectos
    public AudioSource musicSource;
    public AudioSource soundSource;

    // Clips de efectos de sonido
    public AudioClip click;
    public AudioClip jump;
    public AudioClip hitEnemy;
    public AudioClip deadCharacter;
    public AudioClip takeCollectible;
    public AudioClip playerGotHurt;
    public AudioClip playerWin;

    // Clips de música para el menú y el nivel
    public AudioClip menuMusic;
    public AudioClip levelMusic;

    // Clip de música actual en reproducción
    public AudioClip currentMusicClip;

    // Indica si la música está silenciada
    public bool volumeMuted = false;

    /// <summary>
    /// Método llamado al iniciar la instancia del objeto.
    /// Implementa el patrón Singleton y persiste entre escenas.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Carga los volúmenes guardados y reproduce la música del menú al comenzar.
    /// </summary>
    private void Start()
    {
        LoadVolume();
        playMusic(menuMusic);
    }

    /// <summary>
    /// Reproduce una pista de música si no está ya sonando.
    /// </summary>
    /// <param name="clip">Clip de música a reproducir</param>
    public void playMusic(AudioClip clip)
    {
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        currentMusicClip = clip;
        musicSource.loop = true;
        musicSource.Play();
        volumeMuted = false;
    }

    /// <summary>
    /// Detiene la música actual y marca el estado como silenciado.
    /// </summary>
    public void stopMusic()
    {
        musicSource.Stop();
        volumeMuted = true;
    }

    /// <summary>
    /// Alterna entre reproducir y detener la música.
    /// </summary>
    public void switchMusic()
    {
        if (volumeMuted)
        {
            playMusic(currentMusicClip);
            Debug.Log(volumeMuted);
        }

        else
        {
            stopMusic();
            Debug.Log(volumeMuted);
        }

    }

    /// <summary>
    /// Silencia todos los efectos de sonido.
    /// </summary>
    public void stopSounds()
    {
        soundMixer.SetFloat("Volume", -80f);
    }

    /// <summary>
    /// Restaura el volumen de los efectos de sonido según el valor guardado.
    /// </summary>
    public void playSounds()
    {
        float volume = PlayerPrefs.HasKey("Sound") ? PlayerPrefs.GetFloat("Sound") : 1f;
        soundMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }

    // Métodos para reproducir clips de efectos específicos
    public void playClick() => soundSource.PlayOneShot(click);

    public void playJump() => soundSource.PlayOneShot(jump);

    public void playHitEnemy() => soundSource.PlayOneShot(hitEnemy);

    public void playDeadCharacter() => soundSource.PlayOneShot(deadCharacter);

    public void playTakeCollectible() => soundSource.PlayOneShot(takeCollectible);

    public void playerHurt() => soundSource.PlayOneShot(playerGotHurt);

    public void playWin() => soundSource.PlayOneShot(playerWin);

    /// <summary>
    /// Carga los valores de volumen de música y sonido desde PlayerPrefs
    /// y los aplica al mezclador correspondiente.
    /// </summary>
    public void LoadVolume()
    {
        float musicVolume = PlayerPrefs.HasKey("Music") ? PlayerPrefs.GetFloat("Music") : 1f;
        float soundVolume = PlayerPrefs.HasKey("Sound") ? PlayerPrefs.GetFloat("Sound") : 1f;

        musicMixer.SetFloat("Volume", Mathf.Log10(musicVolume) * 20);
        soundMixer.SetFloat("Volume", Mathf.Log10(soundVolume) * 20);
    }

    /// <summary>
    /// Establece el volumen de la música y lo guarda en PlayerPrefs.
    /// </summary>
    /// <param name="volume">Volumen entre 0.001 y 1</param>
    public void SetMusic(float volume)
    {
        volume = Mathf.Clamp(volume, 0.001f, 1f);
        musicMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Music", volume);
        PlayerPrefs.Save();

    }

    /// <summary>
    /// Establece el volumen de los efectos de sonido y lo guarda en PlayerPrefs.
    /// </summary>
    /// <param name="volume">Volumen entre 0.001 y 1</param>
    public void SetSound(float volume)
    {
        volume = Mathf.Clamp(volume, 0.001f, 1f);
        soundMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Sound", volume);
        PlayerPrefs.Save();

    }

}
