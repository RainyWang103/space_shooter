using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	public float tumble;

	void Start()
	{
		//how fast a rigidbody component is rotating 
		//Random.insideUnitSphere: random vector3 in a sphere with unit 1
		rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
	}

}
