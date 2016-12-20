using UnityEngine;
using System.Collections;

[System.Serializable]//to show variables in Boundary in inspector
public class Boundary //doesn't inheritate from any class
{
	public float xMax, xMin, zMin, zMax; 
}

//Note:* Unity will read all the codes to check conditions, then execute accordingly
//will read Update first to determine what to change in the frame
public class PlayerController : MonoBehaviour {

	public float speed; //to manipulate speed of moving
	public Boundary boundary; //create instance of boundary
	public float tilt; //to factor rotating at x-boundary

	public GameObject shot;
	public Transform shotSpawn; //unity will still need us to drag a gameobject
	public Transform addedShotSpawn1, addedShotSpawn2;
	public float fireRate;
	private float nextFire; 
	private bool withAward;
	private int awardBulletCounter;
	public int awardBulletLimit;
	//private int numOfBigMove;


	void Start()
	{
		withAward = false;
		awardBulletCounter = 0;
	}

	//To fire a bolt, don't need physics, don't want to wait to FixedUpdate
	void Update ()  //firing, instantiate like duplicate: object, position, rotation
	{
		if (Input.GetButton ("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
		//	GameObject clone =
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation); //as GameObject
			if(withAward){
			Instantiate (shot, addedShotSpawn1.position, addedShotSpawn1.rotation);
			Instantiate (shot, addedShotSpawn2.position, addedShotSpawn2.rotation);
			awardBulletCounter++;
			}
		}
		if (awardBulletCounter >= awardBulletLimit) {
			withAward = false;
			awardBulletCounter = 0;
		}
	}
	void FixedUpdate() //moving
	{
		//GetAxis only returns values between -1 and 1, thus moving slowly
		float moveHorizontal = Input.GetAxis ("Horizontal"); //x
		float moveVertical = Input.GetAxis ("Vertical");     //z

		//remember to use "new" keyword
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		//Step 1: enable moving the player by setting rigidbody velocity
		rigidbody.velocity = movement * speed;
		//Step 2: set boundary for moving the player, for all 4 directions
		rigidbody.position = new Vector3 
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
				0.0f, //no movement in y-axis
				Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
			);
		//Step 3: enable player rotating when moving in x-direction
		//rotate about z-axis
		//amount of rotating to be proportional to how fast we move in x-direction
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Award") {
			withAward = true;
			awardBulletCounter = 0;
		}
	}

	

}
