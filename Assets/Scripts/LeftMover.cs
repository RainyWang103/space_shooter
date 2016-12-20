using UnityEngine;
using System.Collections;

public class LeftMover : MonoBehaviour {

	public float speed;
	
	void FixedUpdate()
	{
		rigidbody.velocity = new Vector3 (-1.0f, 0.0f, 0.0f) * speed; 
	}

}
