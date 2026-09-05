using UnityEngine;

public class TeleportPointLegacy : MonoBehaviour
{
	public void Teleport ()
	{
TeleportManagerLegacy.Instance.Teleport(transform.position);	}
}
