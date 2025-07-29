using UnityEngine;

/// <summary>
/// Clase que se encarga de seleccionar y reproducir la m�sica adecuada 
/// para la escena actual al iniciar.
/// </summary>
public class MusicSelector : MonoBehaviour
{
    /// <summary>
    /// Clip de m�sica que se debe reproducir en esta escena.
    /// Se asigna desde el Inspector de Unity.
    /// </summary>
    [SerializeField] private AudioClip musicForThisScene;


    /// <summary>
    /// En el inicio de la escena, solicita al controlador de audio que reproduzca la m�sica indicada.
    /// </summary>
    void Start()
    {
        if (AudioCtrl.instance != null)
        {
            AudioCtrl.instance.playMusic(musicForThisScene);
        }
    }
}