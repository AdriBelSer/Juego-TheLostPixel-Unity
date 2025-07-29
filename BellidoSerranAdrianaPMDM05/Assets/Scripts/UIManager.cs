using UnityEngine;
using UnityEngine.UI;

public class UIMAnager : MonoBehaviour
/// <summary>
/// Este script se encarga de inicializar y asignar las acciones de los botones del men� principal.
/// Tambi�n establece una referencia del men� de ajustes en el GameManager.
/// </summary>
{
    // Referencia al men� de ajustes, asignado desde el Inspector
    [SerializeField] private GameObject settingsMenu;

    /// <summary>
    /// M�todo que se llama autom�ticamente al iniciar la escena.
    /// Configura los botones y enlaza el men� de ajustes con el GameManager.
    /// </summary>
    void Start()
    {

        // Asignar referencias al GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.settingsMenu = settingsMenu;
        }
        // Asignaci�n de acciones a los botones del men�, usando sus tags para encontrarlos en la escena
        SetupButton("btn_startGame", GameManager.instance.LoadNivel);
        SetupButton("btn_exitGame", GameManager.instance.ExitGame);
        SetupButton("btn_stopMusic", AudioCtrl.instance.switchMusic);
        SetupButton("btn_settings", GameManager.instance.LoadSettings);

    }
    /// <summary>
    /// M�todo auxiliar para buscar un bot�n por su tag y asignarle una acci�n al hacer clic.
    /// </summary>
    /// <param name="tag">El tag del objeto que contiene el bot�n.</param>
    /// <param name="action">La acci�n que se ejecutar� al hacer clic en el bot�n.</param>
    private void SetupButton(string tag, UnityEngine.Events.UnityAction action)
    {
        GameObject buttonObj = GameObject.FindWithTag(tag);
        if (buttonObj != null)
        {
            Button button = buttonObj.GetComponent<Button>();
            // Si el componente Button existe, asignar la acci�n al evento onClick
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(action);
            }
        }
    }
}
