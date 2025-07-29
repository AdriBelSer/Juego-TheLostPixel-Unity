using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Controlador de audio global para gestionar m�sica de fondo y efectos de sonido.
/// Implementa el patr�n Singleton para mantener una �nica instancia entre escenas.
/// Permite reproducir, silenciar y ajustar vol�menes de m�sica y efectos, as� como gestionar clips espec�ficos.
/// </summary>
public class AudioCtrl : MonoBehaviour
{
    // Instancia est�tica para el patr�n Singleton
    public static AudioCtrl instance;

    // Mezcladores de audio para m�sica y efectos
    public AudioMixer musicMixer;
    public AudioMixer soundMixer;

    // Fuentes de audio para reproducir m�sica y efectos
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

    // Clips de m�sica para el men� y el nivel
    public AudioClip menuMusic;
    public AudioClip levelMusic;

    // Clip de m�sica actual en reproducci�n
    public AudioClip currentMusicClip;

    // Indica si la m�sica est� silenciada
    public bool volumeMuted = false;

    /// <summary>
    /// M�todo llamado al iniciar la instancia del objeto.
    /// Implementa el patr�n Singleton y persiste entre escenas.
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
    /// Carga los vol�menes guardados y reproduce la m�sica del men� al comenzar.
    /// </summary>
    private void Start()
    {
        LoadVolume();
        playMusic(menuMusic);
    }

    /// <summary>
    /// Reproduce una pista de m�sica si no est� ya sonando.
    /// </summary>
    /// <param name="clip">Clip de m�sica a reproducir</param>
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
    /// Detiene la m�sica actual y marca el estado como silenciado.
    /// </summary>
    public void stopMusic()
    {
        musicSource.Stop();
        volumeMuted = true;
    }

    /// <summary>
    /// Alterna entre reproducir y detener la m�sica.
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
    /// Restaura el volumen de los efectos de sonido seg�n el valor guardado.
    /// </summary>
    public void playSounds()
    {
        float volume = PlayerPrefs.HasKey("Sound") ? PlayerPrefs.GetFloat("Sound") : 1f;
        soundMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }

    // M�todos para reproducir clips de efectos espec�ficos
    public void playClick() => soundSource.PlayOneShot(click);

    public void playJump() => soundSource.PlayOneShot(jump);

    public void playHitEnemy() => soundSource.PlayOneShot(hitEnemy);

    public void playDeadCharacter() => soundSource.PlayOneShot(deadCharacter);

    public void playTakeCollectible() => soundSource.PlayOneShot(takeCollectible);

    public void playerHurt() => soundSource.PlayOneShot(playerGotHurt);

    public void playWin() => soundSource.PlayOneShot(playerWin);

    /// <summary>
    /// Carga los valores de volumen de m�sica y sonido desde PlayerPrefs
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
    /// Establece el volumen de la m�sica y lo guarda en PlayerPrefs.
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
