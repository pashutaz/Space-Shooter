using UnityEngine;
using System.Collections;

internal class DestroyByBoundary : MonoBehaviour
{
	void OnTriggerExit(Collider other)
	{
		// Destroy everything that leaves the trigger
		Destroy(other.gameObject);
	}
}