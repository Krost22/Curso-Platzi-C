using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    //Variables del movimiento del personaje
    public float jumpForce = 6f;
    public float runningSpeed = 2f;
    Rigidbody2D rigidBody;
    public float rayDistance = 0.2f; //metros de longitud del rayo(ray)
    Animator animator;
    SpriteRenderer spriteRenderer;


    //Aqui se declaran las condiciones de las animaciones
    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    const string STATE_QUIETO = "isQuieto";

    //Controla las capas
    public LayerMask groundMask;

    //Carga el rigidbody antes de que inicie el juego
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
        animator.SetBool(STATE_QUIETO, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetMouseButtonDown(0))  //poder usar la tecla espacio o clic izq del mause como salto
        {
            Jump();
        }

        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        animator.SetBool(STATE_QUIETO, IsQuieto());


        Debug.DrawRay(this.transform.position, Vector2.down * rayDistance, Color.red); //me muestra en tiempo real donde está tocando el suelo
    }


    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0.03677839f, -0.04457986f);
            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.03677839f, -0.04457986f);
            spriteRenderer.flipX = true;

        }


        rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * runningSpeed,
        rigidBody.velocity.y);

    }

    void Jump()
    {
        if (IsTouchingTheGround())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //Controla la funcion de salto
        }
    }

    //Detecta si está tocando el suelo
    public bool IsTouchingTheGround()
    {
        if(Physics2D.Raycast(this.transform.position, // Desde donde estoy
            Vector2.down, // Trazo un rayo hasta abajo
            rayDistance,          // De 20 cm
            groundMask)){   // Contra la mascara del suelo

           // GameManager.sharedInstance.currentGameState = GameState.inGame;

            return true;
        }
        else
        {
            //TODO: programar logíca de NO contacto con el suelo

            return false;
        }
    }

    //Controla la animacion al personaje no recibir ordenes
    

    public bool IsQuieto()
    {
        if (rigidBody.velocity.x == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
