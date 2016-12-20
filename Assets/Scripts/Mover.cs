using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	
	//we want the laser bolt to have its own velocity
	//forward is a vector3: (0,0,1), velocity in z-axis
	
	public float speed;
	
	void Start()
	{
		rigidbody.velocity = transform.forward * speed; 
	}
	
}