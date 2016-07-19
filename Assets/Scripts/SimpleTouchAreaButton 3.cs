using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	private bool touched;
	private int pointerID;
	private bool canFire;


	void awake()
	{
		touched = false;
	}
	public void OnPointerDown(PointerEventData data)
	{
		// set our start point 
		if (!touched)
		{
			touched = true;
			pointerID = data.pointerId;
			canFire = true;
		}
	}

	public void OnPointerUp(PointerEventData data)
	{
		//reset everything 
		if (data.pointerId == pointerID)
		{
			canFire = false;
			touched = false;
		}

	}
	public bool CanFire()
	{
		return canFire;
	}

}
