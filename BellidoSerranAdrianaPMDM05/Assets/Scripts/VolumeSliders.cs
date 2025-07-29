using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlador para los sliders de volumen de música y efectos de sonido.
/// Permite al usuario ajustar los niveles de volumen y guarda/restaura esos valores usando PlayerPrefs.
/// </summary>
public class VolumeSliders : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;

    /// <summary>
    /// Se ejecuta al iniciar la escena. Inicializa los sliders con los valores almacenados
    /// y añade listeners para actualizar el volumen cuando se modifican los sliders.
    /// </summary>
    private void Start()
    {
        SetSliders();
        /*Añade un listener para actualizar el volumen de la música cuando el usuario
        cambie el slider*/
        musicSlider.onValueChanged.AddListener((volume) =>
        {
            ChangeMusicVolume(volume);
        });
        /*Añade un listener para actualizar el volumen de los sonidos cuando el usuario 
         cambie el slider*/
        soundSlider.onValueChanged.AddListener((volume) =>
        {
            ChangeSoundVolume(volume);
        });
    }
    /// <summary>
    /// Cambia el volumen de la música en el controlador de audio global.
    /// </summary>
    /// <param name="volume">Nuevo volumen entre 0 y 1</param>
    public void ChangeMusicVolume(float volume)
    {
        AudioCtrl.instance.SetMusic(volume);

    }
    /// <summary>
    /// Cambia el volumen de los efectos de sonido en el controlador de audio global.
    /// </summary>
    /// <param name="volume">Nuevo volumen entre 0 y 1</param>
    public void ChangeSoundVolume(float volume)
    {
        AudioCtrl.instance.SetSound(volume);

    }
    /// <summary>
    /// Establece los valores iniciales de los sliders de volumen a partir de los datos almacenados
    /// en PlayerPrefs, o usa un valor por defecto de 1f si no hay datos guardados.
    /// </summary>
    private void SetSliders()
    {

        float musicVolume = PlayerPrefs.HasKey("Music") ? PlayerPrefs.GetFloat("Music") : 1f;
        float soundVolume = PlayerPrefs.HasKey("Sound") ? PlayerPrefs.GetFloat("Sound") : 1f;

        musicSlider.value = musicVolume;
        soundSlider.value = soundVolume;
    }



}
