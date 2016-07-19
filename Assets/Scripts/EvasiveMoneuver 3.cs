using UnityEngine;
using System.Collections;


public class EvasiveMoneuver : MonoBehaviour
{

	public float tilt;
	public float dodge;
	public float smoothing;
	public Vector2 startWait;
	public Vector2 maneuverTIme;
	public Vector2 maneuverWait;
	public Boundary boundary;

	private Transform playerTransform;

	private float currentSpeed;
	private float targetManeuver;
	private Rigidbody rigidBody;

	// Use this for initialization
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		currentSpeed = rigidBody.velocity.z;
		StartCoroutine(Evade());
	}
	IEnumerator Evade()
	{
		yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
		while (true)
		{
			//targetManeuver = Random.Range(1,dodge)*-Mathf.Sign(transform.position.x);
			targetManeuver = playerTransform.position.x;
			yield return new WaitForSeconds(Random.Range(maneuverTIme.x, maneuverTIme.y));
			targetManeuver = 0;
			yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));

		}
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		float newMoneuver = Mathf.MoveTowards(rigidBody.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		rigidBody.velocity = new Vector3(newMoneuver, 0.0f, currentSpeed);
		GetComponent<Rigidbody>().position = new Vector3
			(
				Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
			);
		rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBody.velocity.x * -tilt);
	}
}
