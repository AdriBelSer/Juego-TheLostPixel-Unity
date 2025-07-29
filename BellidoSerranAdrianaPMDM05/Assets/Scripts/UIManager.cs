using UnityEngine;
using UnityEngine.UI;

public class UIMAnager : MonoBehaviour
/// <summary>
/// Este script se encarga de inicializar y asignar las acciones de los botones del menú principal.
/// También establece una referencia del menú de ajustes en el GameManager.
/// </summary>
{
    // Referencia al menú de ajustes, asignado desde el Inspector
    [SerializeField] private GameObject settingsMenu;

    /// <summary>
    /// Método que se llama automáticamente al iniciar la escena.
    /// Configura los botones y enlaza el menú de ajustes con el GameManager.
    /// </summary>
    void Start()
    {

        // Asignar referencias al GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.settingsMenu = settingsMenu;
        }
        // Asignación de acciones a los botones del menú, usando sus tags para encontrarlos en la escena
        SetupButton("btn_startGame", GameManager.instance.LoadNivel);
        SetupButton("btn_exitGame", GameManager.instance.ExitGame);
        SetupButton("btn_stopMusic", AudioCtrl.instance.switchMusic);
        SetupButton("btn_settings", GameManager.instance.LoadSettings);

    }
    /// <summary>
    /// Método auxiliar para buscar un botón por su tag y asignarle una acción al hacer clic.
    /// </summary>
    /// <param name="tag">El tag del objeto que contiene el botón.</param>
    /// <param name="action">La acción que se ejecutará al hacer clic en el botón.</param>
    private void SetupButton(string tag, UnityEngine.Events.UnityAction action)
    {
        GameObject buttonObj = GameObject.FindWithTag(tag);
        if (buttonObj != null)
        {
            Button button = buttonObj.GetComponent<Button>();
            // Si el componente Button existe, asignar la acción al evento onClick
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(action);
            }
        }
    }
}
