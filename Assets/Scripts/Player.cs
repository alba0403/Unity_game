using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //variables
    public float speed=5;
    private Rigidbody2D rb2D;
    private float move;
    public float jumpForce = 4;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public LayerMask groundLayer;

    private Animator animator;

    private int coins;
    public TMP_Text textCoins;

    // al iniciar el juego
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();     //para acceder al componente de Unity
        animator = GetComponent<Animator>();
    }

    // se llama por cada frame
    void Update()
    {
        move= Input.GetAxisRaw("Horizontal");       //para que se mueva con A+D y flechas < + > (izquierda/derecha)
        rb2D.linearVelocity = new Vector2(move*speed, rb2D.linearVelocity.y);

        //para orientar el personaje al girar
        if(move != 0)
            transform.localScale = new Vector3(Mathf.Sign(move),1,1);
        
        // para saltar
        if(Input.GetButtonDown("Jump") && isGrounded){
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
        }

        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetFloat("VerticalVelocity", rb2D.linearVelocity.y); //para que al parametro que queremos en el animator controller se le pase la velocidad que tenemos en el eje Y
        // tambien permite saber si el player cae o salta, para poner la animación correcta
        animator.SetBool("IsGrounded", isGrounded);

    }

    // crea una esfera debajo del personaque para comprobar si colisiona con la layer de floor
    private void FixedUpdate(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.transform.CompareTag("Coin")){     // si algo colisiona con el elemento con el tag Coin, se destruye el coin
            Destroy(collision.gameObject);
            coins++;
            textCoins.text = coins.ToString();
        }

        if(collision.transform.CompareTag("Spikes")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


}
