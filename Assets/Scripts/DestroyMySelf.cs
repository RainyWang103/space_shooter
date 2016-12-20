using UnityEngine;
using System.Collections;

public class DestroyMySelf : MonoBehaviour {

	public GameObject getAwardEffect;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			Instantiate (getAwardEffect, other.transform.position, other.transform.rotation);
			Destroy (gameObject);
		}
	}
}
