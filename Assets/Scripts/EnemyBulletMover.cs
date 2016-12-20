<html><body><h1>403 Forbidden</h1>
Request forbidden by administrative rules.
</body></html>
r {

	public float bulletSpeed;

	void Start()
	{
		rigidbody.velocity = transform.forward * bulletSpeed;
	}
}
