using UnityEngine;
using System.Collections;

public class DestroyByTIme : MonoBehaviour
{
	public float lifeTime;
	// Use this for initialization
	void Start()
	{
		Destroy(gameObject, lifeTime);
	}


}
