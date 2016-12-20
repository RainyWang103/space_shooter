using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	public float speed;
	public float attackWait;
	public Transform enemyShotSpawn;
	public GameObject bullet;
	private float finalSpeed;
	private float finalAttackWait;

	void Start()
	{
		StartCoroutine (Attack ());
		finalSpeed = Random.Range (speed / 4, speed * 2);
		finalAttackWait = Random.Range (attackWait / 4, attackWait * 4);
	}

	void FixedUpdate()
	{
		rigidbody.velocity = new Vector3 (0.0f, 0.0f, finalSpeed);
	}

	IEnumerator Attack()//generate bullets automatically
	{
		while (true) 
		{
			Vector3 shotPosition = enemyShotSpawn.position;
			Quaternion shotRotation = Quaternion.identity;
			Instantiate (bullet, shotPosition, shotRotation);
			yield return new WaitForSeconds(finalAttackWait);
		}
	}
}
