using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class ControlJugador : MonoBehaviour
{

    public int velocidad = 4;
    public float fuerza = 2f;
    public int puntuacion;
    private SpriteRenderer sprite;
    private Animator animacion;
    public float fuerzaSalto;
    private Rigidbody2D fisica;
    public int numVidas;
    private bool vulnerable;
    private ControlHud hud;
    public Canvas canvas;
    public int tiempoNivel;
    public int tiempoEmpleado;
    private float tiempoInicio;
    private ControlDatosJuego controlDatos;
    public AudioClip saltosFx;
    public AudioClip vidasFX;
    private AudioSource audioSource;
    public bool agachado;
    public GameObject bala;
    public Transform controlDisparoDerecha;
    public Transform controlDisparoIzquierda;
    private Vector2 movimiento; 


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }




    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animacion = GetComponent<Animator>();
        vulnerable = true;
        hud = canvas.GetComponent<ControlHud>();
        tiempoInicio = Time.deltaTime;
        hud.SetVidasTXT(numVidas);
        tiempoInicio = Time.time;
        controlDatos=GameObject.Find("DatosJuego").GetComponent<ControlDatosJuego>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && TocarSuelo())

        {
            fisica.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            audioSource.PlayOneShot(saltosFx);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            agachado = !agachado;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Disparar();
        }

        if (fisica.velocity.x > 0) sprite.flipX = false;

        else if (fisica.velocity.x < 0) sprite.flipX = true;

        AnimarJugador();

        hud.SetObjetosTXT(GameObject.FindGameObjectsWithTag("PowerUp").Length);
        if (GameObject.FindGameObjectsWithTag("PowerUp").Length == 0)
        {
            GanarJuego();
        }
        tiempoEmpleado = (int)(Time.time - tiempoInicio);
        hud.SetTiempoTXT(tiempoNivel - tiempoEmpleado);
        if (tiempoNivel - tiempoEmpleado < 0) FinJuego();

       
    }

    private void GanarJuego()
    {
        puntuacion = (numVidas*100)+(tiempoNivel-tiempoEmpleado);
        controlDatos.Puntuacion = puntuacion;
        controlDatos.Ganador = true;
        SceneManager.LoadScene("FinDeJuego");
    }

    private void AnimarJugador()

    {

        //Jugador Saltando 

        if (!TocarSuelo())
        {
            animacion.Play("JugadorSaltando");

        }
       
        //Jugador Corriendo 
        else if ((fisica.velocity.x > 1) || (fisica.velocity.x < -1) && (fisica.velocity.y == 0))
        {
            animacion.Play("JugadorCorriendo");
        }

        //Jugador Agachado
        else if (agachado)
        {

            animacion.Play("JugadorAgacharse");

        }

        //Jugador Idle 
        else if ((fisica.velocity.x < 1 || (fisica.velocity.x > -1) && fisica.velocity.y == 0))
        {
            animacion.Play("JugadorParado");
        }

      

    }

    public void FixedUpdate()
    {
        float entradaX = Input.GetAxis("Horizontal");
        fisica.velocity = new Vector2(entradaX * velocidad, fisica.velocity.y);
    }


    //public void Correr(InputAction.CallbackContext context)
    //{
    //    Vector2 input = context.ReadValue<Vector2>();
    //}

    //public void Saltar(InputAction.CallbackContext context)
    //{
    //    fisica.AddForce(Vector3.up * fuerza, ForceMode2D.Impulse);
    //}

    private bool TocarSuelo()

    {
        RaycastHit2D tocar = Physics2D.Raycast(transform.position + new Vector3(0, -2f, 0), Vector2.down, 0.2f);
        return tocar.collider != null;

    }

    public void IncrementarPuntos(int cantidad)
    {
        puntuacion += cantidad; 
    }

    public void QuitarVida()
    {
        if (vulnerable)
        {
            vulnerable = false;
            numVidas--;
            audioSource.PlayOneShot(vidasFX);
            hud.SetVidasTXT(numVidas);
            if (numVidas == 0) FinJuego();
            Invoke("HacerVulnerable", 1f);
            sprite.color = Color.red;
                     
        }
    }

    void HacerVulnerable()

    {

        vulnerable = true;
        sprite.color = Color.white;

    }

    public void FinJuego()
    {
        controlDatos.Ganador = false;
        SceneManager.LoadScene("FinDeJuego");
    }

    public void Disparar()

    {
        GameObject disparo = new GameObject(); 
        // Si sprite mira hacia la derecha instancia bala en la derecha 
        if (!sprite.flipX)
        {
           disparo = Instantiate(bala, controlDisparoDerecha.position, controlDisparoDerecha.rotation); 
            
        }

        // si el sprite mira izquierda instancia bala a la izquierda 
        else
        {

            disparo = Instantiate(bala, controlDisparoIzquierda.position, controlDisparoIzquierda.rotation);

        }

        disparo.GetComponent<Bala>().Izquierda = sprite.flipX;

    }

    public void Correr(Vector2 direccion)

    {
        movimiento = direccion;
    }

}
