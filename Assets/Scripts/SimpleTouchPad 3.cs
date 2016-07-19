using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	public float smoothing;

	private Vector2 origin;
	private Vector2 direction;
	private Vector2 smoothDirection;
	private bool touched;
	private int pointerID;


	void awake()
	{
		direction = Vector2.zero;
		touched = false;
	}
	public void OnPointerDown(PointerEventData data)
	{
		// set our start point 
		if (!touched)
		{
			touched = true;
			pointerID = data.pointerId;
			origin = data.position;
		}
	}
	public void OnDrag(PointerEventData data)
	{
		//compare the differense betwen our start point and  current poiner pos
		if (data.pointerId == pointerID)
		{
			Vector2 currentPosition = data.position;
			Vector2 dirrectionRaw = currentPosition - origin;
			direction = dirrectionRaw.normalized;
		}

	}
	public void OnPointerUp(PointerEventData data)
	{
		//reset everything 
		if (data.pointerId == pointerID)
		{
			direction = Vector2.zero;
			touched = false;
		}

	}

	public Vector2 GetDirection()
	{
		smoothDirection = Vector2.MoveTowards(smoothDirection, direction, smoothing);

		return smoothDirection;
	}
}
