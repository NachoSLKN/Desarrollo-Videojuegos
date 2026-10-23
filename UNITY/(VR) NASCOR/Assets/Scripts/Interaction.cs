
using UnityEngine;

public class Interaction : MonoBehaviour
{
    // Start se ejecuta una vez al comenzar.
    void Start()
    {

    }

    // Update se ejecuta una vez por frame.
    void Update()
    {
        // ==========================================
        // 1. TECLADO
        // ==========================================

        // Al pulsar la tecla A (un solo frame).
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Se pulsa la tecla A");
        }

        // Mientras se mantiene pulsada la tecla A.
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Se mantiene pulsada la tecla A");
        }

        // Al soltar la tecla A (un solo frame).
        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("Se suelta la tecla A");
        }


        // ==========================================
        // 2. RATÓN
        // ==========================================

        // Al pulsar el clic izquierdo (un solo frame).
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Se pulsa clic izquierdo");
        }

        // Mientras se mantiene el clic izquierdo.
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Se mantiene clic izquierdo");
        }

        // Al soltar el clic izquierdo (un solo frame).
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Se levanta clic izquierdo");
        }


        // ==========================================
        // 3. MOVIMIENTO Y CARRERA
        // ==========================================

        // Avanzar pulsando W o Flecha Arriba.
        // || significa OR (O lógico).
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("Me muevo hacia delante");

            // Condición anidada:
            // Si además mantiene Shift, corre.
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("Ando hacia delante y voy corriendo");
            }
        }


        // ==========================================
        // 4. APUNTAR Y DISPARAR
        // ==========================================

        // Disparar SIN apuntar.
        // && significa AND (Y lógico).
        // ! significa NOT (negación).
        if (Input.GetMouseButtonDown(0) &&
            !Input.GetMouseButton(1))
        {
            Debug.Log("Disparo SIN apuntar");
        }

        // Mantener clic derecho para apuntar.
        if (Input.GetMouseButton(1))
        {
            Debug.Log("APUNTO");

            // Disparar mientras estamos apuntando.
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Disparo APUNTANDO");
            }
        }
    }
}
