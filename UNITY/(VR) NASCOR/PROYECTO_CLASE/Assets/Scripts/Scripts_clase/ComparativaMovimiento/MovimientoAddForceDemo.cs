// using UnityEngine;

// // 03: Rigidbody DINAMICO; la masa influye en ForceMode.Force.
// [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
// public class MovimientoAddForceDemo : MonoBehaviour
// {
//     [SerializeField, Min(0f)] private float fuerza = 25f;
//     private InputComparativaMovimiento entrada;
//     private Rigidbody cuerpo;

//     private void Awake()
//     {
//         entrada = GetComponentInParent<InputComparativaMovimiento>();
//         cuerpo = GetComponent<Rigidbody>();
//         if (entrada == null) Debug.LogError("Falta InputComparativaMovimiento en el padre.", this);
//         if (cuerpo.isKinematic) Debug.LogWarning("03_AddForce debe tener Is Kinematic DESACTIVADO.", this);
//     }

//     private void FixedUpdate()
//     {
//         if (entrada == null) return;
//         Vector2 ejes = entrada.Direccion;
//         Vector3 direccion = new Vector3(ejes.x, 0f, ejes.y);
//         cuerpo.AddForce(direccion * fuerza, ForceMode.Force);
//     }
// }
