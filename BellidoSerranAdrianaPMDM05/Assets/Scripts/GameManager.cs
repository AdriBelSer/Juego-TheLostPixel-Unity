using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controlador principal del juego que gestiona la carga de escenas, la interacción con el menú de configuración
/// y la salida del juego.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Instancia estática que permite el acceso global al GameManager.
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// Referencia al menú de configuración del juego.
    /// </summary>
    public GameObject settingsMenu;


    /// <summary>
    /// Método de inicialización del GameManager.
    /// - Se asegura de que haya una única instancia de GameManager.
    /// - Se suscribe al evento de carga de escenas.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al evento cuando se carga una escena.
        }
        else
        {
            Destroy(gameObject);
        } 
    }

    /// <summary>
    /// Maneja la carga de una nueva escena.
    /// - Si la escena cargada es "Menu", se actualiza la referencia al menú de ajustes.
    /// </summary>
    /// <param name="scene">Escena cargada.</param>
    /// <param name="mode">Modo de carga de la escena.</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Actualiza las referencias cuando se carga la escena Menu
        if (scene.name == "Menu")
        {
            settingsMenu = GameObject.FindGameObjectWithTag("SettingsMenu");
            if (settingsMenu != null) settingsMenu.SetActive(false);
        }
    }

    /// <summary>
    /// Carga la escena del nivel y reanuda el tiempo del juego.
    /// </summary>
    public void LoadNivel()
    {
        AudioCtrl.instance.playClick();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Nvl_1");
    }

    /// <summary>
    /// Muestra el menú de ajustes y pausa el juego.
    /// </summary>
    public void LoadSettings()
    {
        AudioCtrl.instance.playClick();
        settingsMenu.SetActive(true);
        Time.timeScale = 0f;
        
    }

    /// <summary>
    /// Cierra el menú de ajustes y reanuda el juego.
    /// </summary>
    public void CloseSettings()
    {
        AudioCtrl.instance.playClick();
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Cierra la aplicación y muestra un mensaje de salida en la consola.
    /// </summary>
    public void ExitGame()
    {
        AudioCtrl.instance.playClick();
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }

}
