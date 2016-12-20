using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossController : MonoBehaviour {

		public int lifeCycle;
		public int hits; //for inspector now
		public float speed;
		public float attackWait;
		public float attackWaveWait;
		public GameObject[] bossShotSpawn;
		public GameObject[] bullet;
		private float finalSpeed;
		private GameObject targetBullet;

		public float effectWait;
		public Vector3 effectRange;
		public int effectTimes;
		private bool done = false;

		public GameObject hitExplosion; //associate with special effect
		public GameObject dieExplosion;
		public GameObject playerExplosion;
		public int scoreValue;
		public int bulletCount;
		private GameController gameController;

		public GameObject bigMoveToLeft;
		public GameObject bigMoveToRight;
		public GameObject bigMoveToFront;
		public GameObject[] bigMoveSpawnToLeft;
		public GameObject[] bigMoveSpawnToRight;
		public GameObject[] bigMoveSpawnToFront;
		public GameObject bigMoveSignal;
		private int bigMoveCount = 8;
		private int bigMoveDelay;
		private bool[] coroutineFinish = {false,false,false};
		private IEnumerator coroutine1;
		private IEnumerator coroutine2;
		private IEnumerator coroutine3;
		
		
		void Start()
		{
			GameObject gameControllerObject = GameObject.FindWithTag("GameController");
			if (gameControllerObject != null) 
			{
				gameController = gameControllerObject.GetComponent<GameController>();
			}
			if (gameController == null)//ensurance
			{Debug.Log ("Cannot find 'GameController' script!");}

			StartCoroutine (Attack ());
			transform.rotation = Quaternion.Euler (0.0f, 180.0f, 1.0f);
			hits = 0;

			coroutine1 = SpawnBigMove (bigMoveSpawnToLeft, bigMoveToLeft,0);
			coroutine2 = SpawnBigMove (bigMoveSpawnToRight, bigMoveToRight,1);
			coroutine3 = SpawnBigMove (bigMoveSpawnToFront, bigMoveToFront,2);
		}
		
		void Update()
		{
			rigidbody.velocity = new Vector3 (0.0f, 0.0f, speed);
			if (hits >= lifeCycle && !done) {
			StartCoroutine (DieEffects ());
			//Destroy (gameObject);
			}
			
			if (bigMoveDelay / 10 > 0) 
			{
				float grade = Random.Range (0.0f, 1.0f);
				if(grade >= 0.6f)
				{
					Debug.Log (grade);
					Instantiate (bigMoveSignal, transform.position, transform.rotation);
					StartCoroutine(coroutine1);
					StartCoroutine(coroutine2);
					StartCoroutine(coroutine3);
				}
				bigMoveDelay = 0;

			}
			//Debug.Log (bigMoveDelay);
			if (coroutineFinish[0]&&coroutineFinish[1]&&coroutineFinish[2]) {
				StopCoroutine (coroutine1);coroutineFinish[0]=false;
				StopCoroutine (coroutine2);coroutineFinish[1]=false;
				StopCoroutine (coroutine3);coroutineFinish[2]=false;
			}
		}
	

		IEnumerator DieEffects()
		{
			for(int i=0; i<effectTimes; i++)
			{	
				Vector3 offset = new Vector3(
					Random.Range (-effectRange.x, effectRange.x),
					1.0f,
					Random.Range (-effectRange.z, effectRange.z)
				);
				Instantiate (hitExplosion, transform.position + offset, transform.rotation);
				yield return new WaitForSeconds (effectWait);
			}
			Instantiate (dieExplosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
		
		IEnumerator Attack()//generate bullets automatically
		{
			while (true) 
			{
				for (int j=0; j<bulletCount; j++){
					for(int i=0; i<5; i++)
					{
						Vector3 shotPosition = bossShotSpawn[i].transform.position;
						Quaternion shotRotation = Quaternion.identity;
						if(i==0){targetBullet = bullet[0];}
						else if(i==3||i==1){targetBullet = bullet[1];}
						else if(i==4||i==2){targetBullet = bullet[2];}
					//Debug.Log (i);
					Instantiate (targetBullet, shotPosition, shotRotation);
					//yield return new WaitForSeconds(attackWait);
					}
					yield return new WaitForSeconds(attackWait);
				}
				yield return new WaitForSeconds (attackWaveWait);
			}

		}

		IEnumerator SpawnBigMove(GameObject[] spawn, GameObject attack, int flagNum)
		{
			while (true) {
					for (int i=0; i<bigMoveCount; i++) {
						Vector3 spwanPosition = new Vector3();
						Quaternion spwanRotation;
						spwanRotation = (flagNum == 2)? Quaternion.Euler(0.0f, 180.0f, 0.0f): Quaternion.identity;
						float x = -9.0f+3.0f*i;
						Vector3 hitshipPOS = new Vector3 (x, 0.0f , 27.0f);
						spwanPosition = (flagNum == 2)? hitshipPOS:spawn [i].transform.position;
						Instantiate (attack, spwanPosition, spwanRotation);
					}
				coroutineFinish[flagNum]=true;
				yield return null;
			}

		}

	//OnTriggerEnter: when first touch the collider, different from OnTriggerExit
	//Destroy script only mark the component to be destroyed, 
	//all component will be destroyed together at the end of the frame
	void OnTriggerEnter(Collider other)
	{

		//Types of destroy effects
		if (other.tag == "PlayerBullet") {
			hits++;
			bigMoveDelay++;
			Instantiate (hitExplosion, transform.position, transform.rotation);
			Destroy (other.gameObject);
			gameController.AddScore (scoreValue);
		} //player is destroyed

		else if (other.tag == "Player") {
			hits++;
			bigMoveDelay++;
			Instantiate (playerExplosion, transform.position, transform.rotation);
			Destroy (other.gameObject);
			gameController.AddScore (scoreValue);
			gameController.GameOver (); 
		} 
		
	}

}
