using UnityEngine;

public class TeleportPoint : MonoBehaviour
{

/*Tendremos una función Teleport y con este Script accederemos a esta función pasándole el Vector3,
pasándole la posición del TeleportPoint.
*/

    public void Teleport()
    {
        TeleportManager.Instance.Teleport(transform.position);
    }
}