using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Controlador principal del jugador.
/// Gestiona el movimiento, los saltos, la recogida de objetos, la detección de enemigos,
/// el sistema de vidas, rebotes, y la interacción con la interfaz de usuario.
/// </summary>
public class PlayerCtrl : MonoBehaviour
{
    // Velocidad de movimiento horizontal del jugador
    [SerializeField] private float speed = 3f;

    // Fuerza del salto
    [SerializeField] private float jumpForce = 3f;

    // Elementos de UI que muestran la información del jugador
    [SerializeField] private TextMeshProUGUI applesCountText;
    [SerializeField] private TextMeshProUGUI livesCountText;
    [SerializeField] private TextMeshProUGUI winnerScreenApplesCountText;
    [SerializeField] private TextMeshProUGUI winnerScreenLivesCountText;

    // Referencias a componentes
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;    
    private Animator animator;
    private AudioSource audioSource;

    // Control de estado
    private bool isGrounded = false;
    private int applesCount = 0;
    private int livesCount = 3;
    private float lastTapTime = 0f;
    private float doubleTapDelay = 0.3f; // Tiempo m�ximo entre taps

    // UI de fin de juego
    public GameObject GameOverCanvas;
    public GameObject WinCanvas;

    // Fuerza del rebote al golpear enemigos
    [SerializeField] private float velocityRebound = 3f;

    /// <summary>
    /// Inicializa componentes y muestra información inicial en pantalla.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        applesCountText.text = applesCount.ToString();
        livesCountText.text = livesCount.ToString();
        winnerScreenApplesCountText.text = applesCountText.text;
        winnerScreenLivesCountText.text = livesCountText.text;
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Lógica de entrada del jugador.
    /// Detecta doble toque para salto y movimiento mientras se mantiene pulsado.
    /// </summary>
    void Update()
    {
        animator.SetBool("isRunning", Math.Abs(rb.linearVelocityX) > 0.2);
        if (Input.touchCount > 0)

        {
            UnityEngine.Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                float currentTime = Time.time;

                // Si el tiempo entre el �ltimo toque y el actual es menor que el l�mite, saltamos
                if (currentTime - lastTapTime < doubleTapDelay)
                {
                    Jump(); // Doble toque detectado
                }

                lastTapTime = currentTime;
            }

            if (touch.phase == TouchPhase.Stationary)
            {
                Move(touch);
            }
        }
    }

    /// <summary>
    /// Realiza el salto si el jugador está en el suelo.
    /// </summary>
    private void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            AudioCtrl.instance.playJump();
        }
    }

    /// <summary>
    /// Mueve al jugador hacia la derecha o izquierda según la posición del toque.
    /// </summary>
    private void Move(UnityEngine.Touch t)
    {

        if (t.position.x > Screen.width / 2)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocityY);
            spriteRenderer.flipX = false;
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocityY);
            spriteRenderer.flipX = true;
        }
        

    }

    /// <summary>
    /// Rebota verticalmente al saltar sobre un enemigo.
    /// </summary>
    public void Rebound()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, velocityRebound);

    }

    /// <summary>
    /// Rebote hacia atrás cuando el jugador recibe daño.
    /// </summary>
    public void ReboundBack(Vector2 enemyPosition)
    {
        float direction = transform.position.x < enemyPosition.x ? -1f : 1f;
        rb.linearVelocity = new Vector2(direction * speed, velocityRebound);

    }

    /// <summary>
    /// Lógica de colisión con enemigos, suelo y otros elementos.
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("passiveEnemy"))
        {
            if (collision.GetContact(0).normal.y > -0.5f)
            {
                livesCount--;
                AudioCtrl.instance.playerHurt();
                ReboundBack(collision.transform.position);
            }

            if (livesCount <= 0)
            {
                PlayerLose();
            }
            else
            {
                livesCountText.text = livesCount.ToString();
            }
        }
  
        if (collision.gameObject.CompareTag("activeEnemy"))
        {
            ContactPoint2D contact = collision.GetContact(0);

            if (contact.normal.y > 0.5f) // contacto desde arriba
            {
                collision.gameObject.GetComponent<NinjaCtrl>().Hit();
                Rebound();
            }
            else
            {
                livesCount--;
                AudioCtrl.instance.playerHurt();
                ReboundBack(collision.transform.position);
                if (livesCount <= 0)
                {
                    PlayerLose();
                }
                else
                {
                    livesCountText.text = livesCount.ToString();
                }
            }
        }

        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    /// <summary>
    /// Lógica al perder el juego (sin vidas).
    /// Detiene el tiempo y muestra pantalla de derrota.
    /// </summary>
    private void PlayerLose() {
        livesCount = 0;
        livesCountText.text = "0";
        AudioCtrl.instance.playDeadCharacter();
        AudioCtrl.instance.stopMusic();
        Time.timeScale = 0f;
        GameOverCanvas.SetActive(true);
    }

    /// <summary>
    /// Detecta cuando el jugador deja de tocar el suelo.
    /// </summary>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }

    /// <summary>
    /// Detecta y gestiona la recogida de manzanas.
    /// Si se recogen 7, gana el juego.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("apple"))
        {
            Destroy(collision.gameObject);
            applesCount++;
            applesCountText.text = applesCount.ToString();
            AudioCtrl.instance.playTakeCollectible();

            if (applesCount >= 7)
            {
                Debug.Log("WIN");
                WinCanvas.SetActive(true);
                AudioCtrl.instance.playWin();
                AudioCtrl.instance.stopMusic();    
                Time.timeScale = 0f;
                winnerScreenApplesCountText.text = applesCountText.text;
                winnerScreenLivesCountText.text = livesCountText.text;

            }

        }

    }
    
}