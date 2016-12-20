using UnityEngine;
using UnityEngine.UI; //for score 
using System.Collections;

public class GameController : MonoBehaviour {
	
	//Application 1. spawn asteroid hazards by loop
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	//Application 1.2. spawn enemy hazards by loop
	public GameObject enemy;
	public Vector3 enemySpawn;
	public int enemyCount;
	public float enemySpawnWait;
	public float enemyWaveWait;

	//Application 1.3.spawn Boss
	public GameObject boss;
	public float scoreLevel;
	public Vector3 bossSpawn;
	private int bossCounter = 0;

	//Application 1.4. spawn award
	public GameObject award;
	public float awardLevel;
	public Vector3 awardSpawn;
	private int awardCounter = 0;

	//Application 2: show score
	private int score;
	public Text scoreText;
	//Application 3: gameover and restart
	public Text restartText;
	public Text gameOverText;
	private bool gameOver;
	private bool restart;


	void Start()
	{
		gameOver = false;
		restart = false;
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
		StartCoroutine (SpawnWavesEnemy ());
		scoreText.text = "";
		restartText.text = "";
		gameOverText.text = "";
	}

	void Update() //restart
	{
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		if (score > scoreLevel*(bossCounter+1)) {

			Vector3 spawnPosition = new Vector3 (Random.Range (-bossSpawn.x, bossSpawn.x), bossSpawn.y, bossSpawn.z);
			//no rotation
			Quaternion spawnRotation = Quaternion.identity;
			//What to spawn
			Instantiate (boss, spawnPosition, spawnRotation);
			bossCounter++;
			scoreLevel *= 2;
		}

		if (score > awardLevel * (awardCounter + 1)) {
			Vector3 spawnPositionA = new Vector3 (Random.Range (-awardSpawn.x, awardSpawn.x), awardSpawn.y, awardSpawn.z);
			//Vector3 spawnPosition2 = new Vector3 (Random.Range (-awardSpawn.x, awardSpawn.x), awardSpawn.y, awardSpawn.z);
			//no rotation
			Quaternion spawnRotation = Quaternion.identity;
			//What to spawn
			Instantiate (award, spawnPositionA, spawnRotation);
			//Instantiate (award, spawnPosition2, spawnRotation);
			awardCounter+=3;
		
		}

	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds(startWait);
		while(true)
		{
			for (int i=0;i<hazardCount;i++) 
			{
				//same hights, different start position
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				//no rotation
				Quaternion spawnRotation = Quaternion.identity;
				//What to spawn
				Instantiate (hazard, spawnPosition, spawnRotation);
				//Coroutine
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
			if(gameOver)
			{
				restartText.text = "Press 'R' to restart";
				restart = true;
				break;
			}
		}
	}

	IEnumerator SpawnWavesEnemy ()
	{
		while(true)
		{
			for (int i=0;i<enemyCount;i++) 
			{
				//same hights, different start position
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				//no rotation
				Quaternion spawnRotation = Quaternion.identity;
				//What to spawn
				Instantiate (enemy, spawnPosition, spawnRotation);
				//Coroutine
				yield return new WaitForSeconds(enemySpawnWait);
			}
			yield return new WaitForSeconds(enemyWaveWait);
			if(gameOver)
			{
				restartText.text = "Press 'R' to restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore(int newScoreValue)//expose to other objects
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score.ToString(); 
	}

	public void GameOver()//expose to other objects
	{
		gameOverText.text = "Game Over !";
		gameOver = true;
	}


}
