
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionNew : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        // Comprobamos que existan teclado y ratón.
        Keyboard teclado = Keyboard.current;
        Mouse raton = Mouse.current;

        // ==========================================
        // 1. TECLADO
        // ==========================================

        if (teclado != null)
        {
            // Pulsar A.
            if (teclado.aKey.wasPressedThisFrame)
            {
                Debug.Log("Se pulsa la tecla A");
            }

            // Mantener A.
            if (teclado.aKey.isPressed)
            {
                Debug.Log("Se mantiene pulsada la tecla A");
            }

            // Soltar A.
            if (teclado.aKey.wasReleasedThisFrame)
            {
                Debug.Log("Se suelta la tecla A");
            }


            // ======================================
            // 2. MOVIMIENTO Y CARRERA
            // ======================================

            // Avanzar con W o Flecha Arriba.
            if (teclado.wKey.isPressed ||
                teclado.upArrowKey.isPressed)
            {
                Debug.Log("Me muevo hacia delante");

                // Mantener Shift para correr.
                if (teclado.leftShiftKey.isPressed)
                {
                    Debug.Log("Ando hacia delante y voy corriendo");
                }
            }
        }


        // ==========================================
        // 3. RATÓN
        // ==========================================

        if (raton != null)
        {
            // Pulsar clic izquierdo.
            if (raton.leftButton.wasPressedThisFrame)
            {
                Debug.Log("Se pulsa clic izquierdo");
            }

            // Mantener clic izquierdo.
            if (raton.leftButton.isPressed)
            {
                Debug.Log("Se mantiene clic izquierdo");
            }

            // Soltar clic izquierdo.
            if (raton.leftButton.wasReleasedThisFrame)
            {
                Debug.Log("Se levanta clic izquierdo");
            }


            // ======================================
            // 4. APUNTAR Y DISPARAR
            // ======================================

            // Disparar sin apuntar.
            if (raton.leftButton.wasPressedThisFrame &&
                !raton.rightButton.isPressed)
            {
                Debug.Log("Disparo SIN apuntar");
            }

            // Mantener clic derecho para apuntar.
            if (raton.rightButton.isPressed)
            {
                Debug.Log("APUNTO");

                // Disparar mientras apuntamos.
                if (raton.leftButton.wasPressedThisFrame)
                {
                    Debug.Log("Disparo APUNTANDO");
                }
            }
        }
    }
}
