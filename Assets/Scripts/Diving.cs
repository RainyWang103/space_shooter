using UnityEngine;
using System.Collections;

public class Diving : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Boss")
		{transform.position = new Vector3(transform.position.x, transform.position.y - 2.0f, transform.position.z);}
	}

	void OnTriggerExit (Collider other)
	{
		if(other.tag == "Boss")
		{transform.position = new Vector3(transform.position.x, transform.position.y + 3.0f, transform.position.z);}
	}
}
