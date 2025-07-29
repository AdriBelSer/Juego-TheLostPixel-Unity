using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlador del menú de configuración de sonido del juego.
/// Se encarga de inicializar los sliders de volumen y de manejar los cambios
/// en el volumen de la música y efectos sonoros, así como cerrar el menú.
/// </summary>
public class SettingsMenuController : MonoBehaviour
{
    // Referencias a los sliders de volumen de música y efectos
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    // Botón para cerrar el menú de configuración
    [SerializeField] private Button closeButton;

    /// <summary>
    /// Inicializa el estado de los sliders con los valores guardados en PlayerPrefs
    /// y registra los eventos de cambio de valor y cierre del menú.
    /// </summary>
    private void Awake()
    {
        // 1) Inicializa los sliders al valor guardado, o con 1f por defecto
        float savedMusic = PlayerPrefs.GetFloat("Music", 1f);
        float savedSound = PlayerPrefs.GetFloat("Sound", 1f);
        musicSlider.value = savedMusic;
        soundSlider.value = savedSound;

        // 2) Registra eventos: actualiza volúmenes y cierra el menú al pulsar el botón
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    /// <summary>
    /// Se llama cuando el usuario cambia el volumen de la música desde el slider.
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
    /// Se llama cuando el usuario pulsa el botón de cerrar.
    /// Llama al método correspondiente del GameManager para cerrar el menú.
    /// </summary>
    private void OnCloseButtonClicked()
    {
        GameManager.instance.CloseSettings();
    }
}