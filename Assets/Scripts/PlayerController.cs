using UnityEngine;
using System.Collections;
using UnityEditor;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;

}
public class PlayerController : MonoBehaviour
{

	public float speed;
	public float tilt;
	public Boundary boundary;
	public GameObject shot;
	public Transform[] shotSpawns;
	public SimpleTouchPad touchPad;
	public SimpleTouchAreaButton areaButton;



	public float fireRate;
	private float nextFire;
	private Quaternion calibrationQuaternion;

	void Start()
	{
		CalibrateAccellerometer();
	}
	void Update()
	{
		//keyboard control
		//if ((Input.GetButton("Fire1") || Input.GetButton("Jump")) && Time.time > nextFire)

		//touch control
		if (areaButton.CanFire() && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			foreach (var shotSpawn in shotSpawns)
			{
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);//as GameObject;	
			}

			GetComponent<AudioSource>().Play();
		}
	}
	void FixedUpdate()
	{
		//keybord motion 
		//float moveHorizontal = Input.GetAxis("Horizontal");
		//float moveVertical = Input.GetAxis("Vertical");

		//Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		//Mobile motion
		//Vector3 accelerationRaw = Input.acceleration;
		//Vector3 acceleration = FixedAcceleration(accelerationRaw);
		//Vector3 movement = new Vector3(acceleration.x, 0.0f, acceleration.y);
		Vector2 direction = touchPad.GetDirection();
		var movement = new Vector3(direction.x, 0.0f, direction.y);

		GetComponent<Rigidbody>().velocity = movement * speed;

		GetComponent<Rigidbody>().position = new Vector3
			(
				Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
			);
		GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
	//used to callibrate  the Input.acceleration input
	void CalibrateAccellerometer()
	{
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
	}
	//get the calibrated value from input
	Vector3 FixedAcceleration(Vector3 acceleration)
	{
		Vector3 fixedAccelleration = calibrationQuaternion * acceleration;
		return fixedAccelleration;

	}
}