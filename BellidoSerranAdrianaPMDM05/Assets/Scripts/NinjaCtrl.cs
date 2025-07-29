using System.Collections;
using UnityEngine;

/// <summary>
/// Controlador de enemigos tipo "Ninja" que patrullan entre puntos establecidos.
/// Reaccionan al ser golpeados por el jugador con animaci�n, sonido y efecto visual.
/// </summary>
public class NinjaCtrl : MonoBehaviour
{
    // Array de puntos de patrulla
    [SerializeField] private Transform[] waypoint;

    // Velocidad de movimiento entre puntos
    [SerializeField] private float speed = 0.01f;

    // Efecto visual que se muestra al morir
    [SerializeField] private GameObject effect;

    // �ndice del punto actual hacia el que se dirige el enemigo
    private int currentWayPointIndex = 0;

    // Referencias a componentes
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AudioSource audioSource;

    // Direcci�n de movimiento (usado para voltear el sprite)
    private bool isMoveLeft = true;


    /// <summary>
    /// Inicializa componentes en el primer fotograma.
    /// </summary>
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Mueve al enemigo entre puntos de patrulla.
    /// </summary>
    void Update()
    {
        if (waypoint.Length > 0)
        {
            Transform currentWayPointTransform = waypoint[currentWayPointIndex];
            transform.position = Vector2.MoveTowards(transform.position, currentWayPointTransform.position, speed);
        }
    }

    /// <summary>
    /// Cambia de direcci�n al llegar a un punto de patrulla.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("waypoint"))
        {
            currentWayPointIndex = (currentWayPointIndex + 1) % waypoint.Length;
            isMoveLeft = !isMoveLeft;
            spriteRenderer.flipX = isMoveLeft;
        }

    }

    /// <summary>
    /// Llamado cuando el enemigo es golpeado por el jugador.
    /// Inicia la secuencia de eliminaci�n del ninja.
    /// </summary>
    public void Hit()
    {
        StartCoroutine(KillNinja());
    }

    /// <summary>
    /// Corrutina que reproduce animaci�n y efecto, desactiva colisi�n y destruye al ninja.
    /// </summary>
    private IEnumerator KillNinja()
    {
        GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("Hit");
        AudioCtrl.instance.playHitEnemy();

        if (effect != null)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
        }
        // Espera medio segundo antes de eliminar al objeto
        yield return new WaitForSeconds(0.5f);

        // Destruye al ninja
        Destroy(gameObject);
    }


}
