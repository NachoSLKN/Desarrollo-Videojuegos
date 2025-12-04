using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {

	public float speed; //Variable de números con decimales, al ser publica la podremos llamar desde el inspector
	private Vector3 position; //Posición en el espacio
	public CharacterController controller;
    
    
    //Animator
    Animator anim;


    //Animation (Legacy)
    //public AnimationClip run;
    //public AnimationClip idle; //Idle para cuando el obj no se mueve


    // Use this for initialization
    void Start () {
	

			position = transform.position; //Queremos que por defecto la posición del jugador sea la suya misma, no la del origen. 
            anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {


		if (Input.GetMouseButton(0))
		{

			//Locate where the player clicked on the terrain
			locatePosition();


		}


			//Move the player to the position
			moveToPosition();

	}


	void locatePosition()
	{

		//Una línea en el espacio 3D la cual nos da información de los objetos con los que interfiere
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit, 1000))
		{

            if (hit.collider.tag != "Player") { //Para cuando se pulse al Player
			position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
			Debug.Log(position);
            }
		}


	}

    void moveToPosition()
    {
        // Calculamos una sola vez si se está moviendo
        bool isMoving = Vector3.Distance(transform.position, position) > 0.2f;

        //GameObject is Moving
        if (isMoving) // usamos la variable en vez de calcular otra vez
        {
            // Calculamos la rotación hacia el punto de destino
            Quaternion newRotation = Quaternion.LookRotation(position - transform.position, Vector3.up);

            //Bloqueamos el movimiento del jugador cuando mire hacia el cursor
            newRotation.x = 0f;
            newRotation.z = 0f;

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);

            // Movimiento del jugador
            controller.SimpleMove(transform.forward * speed);

            // -------------//
            //   LEGACY     //
            // -------------//

            //GetComponent<Animation>().CrossFade("DS_bow_run", 0.2f);

            // -------------------------//
            //   NUEVA FORMA (Animator) //
            // -------------------------//

            anim.SetFloat("Speed", 1f); // activa animación de correr / caminar
        }
        // Se ejecuta cuando el obj no se mueve    
        else
        {
            // ------------------------------
            //   LEGACY (comentado)
            // ------------------------------

            //GetComponent<Animation>().CrossFade("DS_bow_idle_A", 0.2f);

            // ------------------------------
            //   NUEVA FORMA (Animator)
            // ------------------------------
            anim.SetFloat("Speed", 0f); // activa animación idle
        }
    }



}
