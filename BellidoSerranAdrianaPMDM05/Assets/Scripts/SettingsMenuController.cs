using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlador del men� de configuraci�n de sonido del juego.
/// Se encarga de inicializar los sliders de volumen y de manejar los cambios
/// en el volumen de la m�sica y efectos sonoros, as� como cerrar el men�.
/// </summary>
public class SettingsMenuController : MonoBehaviour
{
    // Referencias a los sliders de volumen de m�sica y efectos
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    // Bot�n para cerrar el men� de configuraci�n
    [SerializeField] private Button closeButton;

    /// <summary>
    /// Inicializa el estado de los sliders con los valores guardados en PlayerPrefs
    /// y registra los eventos de cambio de valor y cierre del men�.
    /// </summary>
    private void Awake()
    {
        // 1) Inicializa los sliders al valor guardado, o con 1f por defecto
        float savedMusic = PlayerPrefs.GetFloat("Music", 1f);
        float savedSound = PlayerPrefs.GetFloat("Sound", 1f);
        musicSlider.value = savedMusic;
        soundSlider.value = savedSound;

        // 2) Registra eventos: actualiza vol�menes y cierra el men� al pulsar el bot�n
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    /// <summary>
    /// Se llama cuando el usuario cambia el volumen de la m�sica desde el slider.
    /// Actualiza el volumen en el controlador de audio.
    /// </summary>
    /// <param name="value">Nuevo valor del slider (entre 0 y 1)</param>
    private void OnMusicSliderChanged(float value)
    {
        AudioCtrl.instance.SetMusic(value);
    }

    /// <summary>
    /// Se llama cuando el usuario cambia el volumen de los efectos de sonido desde el slider.
    /// Actualiza el volumen en el controlador de audio.
    /// </summary>
    /// <param name="value">Nuevo valor del slider (entre 0 y 1)</param>
    private void OnSoundSliderChanged(float value)
    {
        AudioCtrl.instance.SetSound(value);
    }

    /// <summary>
    /// Se llama cuando el usuario pulsa el bot�n de cerrar.
    /// Llama al m�todo correspondiente del GameManager para cerrar el men�.
    /// </summary>
    private void OnCloseButtonClicked()
    {
        GameManager.instance.CloseSettings();
    }
}