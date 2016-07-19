using System.Collections;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explotion;
	public GameObject playerExpotion;
	public int scoreValue;
	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null)
		{
			Debug.Log("Cannot find 'GameController' script ");

		}
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.name);

		if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
		{
			return;
		}

		if (explotion != null)
		{
			Instantiate(explotion, transform.position, transform.rotation);
		}

		if (other.CompareTag("Player"))
		{
			Instantiate(playerExpotion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}
		gameController.AddScore(scoreValue);
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
