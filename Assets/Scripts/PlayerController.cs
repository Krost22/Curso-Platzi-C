using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    //Variables del movimiento del personaje
    public float jumpForce = 6f;
    public float runningSpeed = 2f;
    public float rayDistance = 0.2f; //metros de longitud del rayo(ray)

    Vector3 startPosition; //detecta el frame donde inicia el personaje

    Rigidbody2D rigidBody;
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
        startPosition = this.transform.position;

        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
        animator.SetBool(STATE_QUIETO, false);
    }

    void ResetPosition() //En el curso lo llaman StartGame
    {
        this.transform.position = startPosition;
        this.rigidBody.velocity = Vector2.zero;
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


        Debug.DrawRay(this.transform.position, Vector2.down * rayDistance, Color.red); //me muestra en tiempo real donde está tocando el suelo el raycast
    }


    void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        { //Si está jugando dejalo rotar el personaje con flip
            rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * runningSpeed,
            rigidBody.velocity.y);

            if (Input.GetAxis("Horizontal") > 0)
            {
                GetComponent<CapsuleCollider2D>().offset = new Vector2(0.03677839f, -0.04457986f);
                spriteRenderer.flipX = false;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.03677839f, -0.04457986f);
                spriteRenderer.flipX = true;

            }else if (GameManager.sharedInstance.currentGameState == GameState.menu)
            {
                rigidBody.Sleep(); //inmoviliza al personaje
                
            }
        } 
              
    }

    void Jump()//Controla la funcion de salto
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame) { 
            if (IsTouchingTheGround())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); 
        }
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

    public void PlayerDie() //Controla la muerte pero solo del jugador
    {
        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
        rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
    }
}