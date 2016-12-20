using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	//destroy other collider when it leave and stop touching the boundary
	//solve endless shots existing when playing
	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Award") {
			return;}
		Destroy (other.gameObject);
	}
}
