<html><body><h1>403 Forbidden</h1>
Request forbidden by administrative rules.
</body></html>
aviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;

	void Start ()
	{
		InvokeRepeating ("Fire", delay, fireRate);
	}

	void Fire ()
	{
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		audio.Play();
	}
}
