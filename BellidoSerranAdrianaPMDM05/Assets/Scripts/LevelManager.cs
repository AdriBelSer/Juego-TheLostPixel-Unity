using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/// <summary>
/// Controlador de nivel responsable de gestionar la lógica relacionada con la escena actual,
/// como reiniciar el nivel, abrir/cerrar el menú de ajustes o volver al menú principal.
/// </summary>
public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Instancia estática para permitir el acceso global al LevelManager.
    /// </summary>
    public static LevelManager Instance;

    /// <summary>
    /// Referencia al menú de ajustes del nivel.
    /// </summary>
    public GameObject settingsMenuNvl;

    /// <summary>
    /// Música de fondo asociada a este nivel.
    /// </summary>
    public AudioClip musicLvl;

    /// <summary>
    /// Reinicia el nivel actual:
    /// - Restaura la velocidad del tiempo.
    /// - Carga la escena del nivel.
    /// - Reproduce música y efectos de sonido.
    /// </summary>
    public void RestartLevel()
    {
        AudioCtrl.instance.playClick();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Nvl_1");
        AudioCtrl.instance.playMusic(musicLvl);
        AudioCtrl.instance.playSounds();

    }

    /// <summary>
    /// Pausa el juego y abre el menú de configuración del nivel.
    /// También detiene los sonidos del juego.
    /// </summary>
    public void LoadSettingsNvl()
    {
        AudioCtrl.instance.playClick();
        Time.timeScale = 0f;
        AudioCtrl.instance.stopSounds();
        settingsMenuNvl.SetActive(true);
    }

    /// <summary>
    /// Cierra el menú de configuración, reanuda el juego y activa de nuevo los sonidos.
    /// </summary>
    public void CloseSettingsNvl()
    {
        AudioCtrl.instance.playClick();
        Time.timeScale = 1f;
        AudioCtrl.instance.playSounds();
        settingsMenuNvl.SetActive(false);
    }

    /// <summary>
    /// Carga la escena del menú principal.
    /// </summary>
    public void LoadMenu()
    {
        AudioCtrl.instance.playClick();
        SceneManager.LoadScene("Menu");
        AudioCtrl.instance.playSounds();
    }

}
