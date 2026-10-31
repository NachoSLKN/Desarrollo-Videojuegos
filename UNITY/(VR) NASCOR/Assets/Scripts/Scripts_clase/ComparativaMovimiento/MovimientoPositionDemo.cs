// using UnityEngine;

// // 02: cambia directamente la posicion GLOBAL; no resuelve colisiones.
// [RequireComponent(typeof(CapsuleCollider))]
// public class MovimientoPositionDemo : MonoBehaviour
// {
//     [SerializeField, Min(0f)] private float velocidad = 5f;
//     private InputComparativaMovimiento entrada;

//     private void Awake()
//     {
//         entrada = GetComponentInParent<InputComparativaMovimiento>();
//         if (entrada == null) Debug.LogError("Falta InputComparativaMovimiento en el padre.", this);
//     }

//     private void Update()
//     {
//         if (entrada == null) return;
//         Vector2 ejes = entrada.Direccion;
//         Vector3 direccion = new Vector3(ejes.x, 0f, ejes.y);
//         transform.position += direccion * velocidad * Time.deltaTime;
//     }
// }
