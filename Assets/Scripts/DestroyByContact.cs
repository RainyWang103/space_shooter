using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion; //associate with special effect
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start() //need to find gamecontroller for score, because the Asteroid is a prefab
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null)//ensurance
		{Debug.Log ("Cannot find 'GameController' script!");}
	}

	//OnTriggerEnter: when first touch the collider, different from OnTriggerExit
	//Destroy script only mark the component to be destroyed, 
	//all component will be destroyed together at the end of the frame
	void OnTriggerEnter(Collider other)
	{

			if (other.tag == "Enemy" ||other.tag == "Asteroid" ||other.tag == "Boss") {return;} 
			//boundary is a solid cube actually
			//But it will be destroyed when leaving the boundary
			if (other.tag == "Boundary"||other.tag == "EnemyBullet") {return;}

			if (other.tag == "Award") {return;}

			//Types of destroy effects
			if (other.tag == "Player") { //player is destroyed
				Instantiate (playerExplosion, transform.position, transform.rotation);
				gameController.GameOver ();
			}
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (other.gameObject);
			Destroy (gameObject);

			gameController.AddScore (scoreValue);
		
	}

}
