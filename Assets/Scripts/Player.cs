using UnityEngine;

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

    // al iniciar el juego
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();     //para acceder al componente de Unity
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
    }

    // crea una esfera debajo del personaque para comprobar si colisiona con la layer de floor
    private void FixedUpdate(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }
}
