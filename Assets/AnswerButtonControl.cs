using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnswerButtonControl : MonoBehaviour, IDropHandler
{
	public GameObject item
	{
		get
		{
			if (transform.childCount > 0)
			{
				return transform.GetChild(0).gameObject;
			}
			return null;
		}
	}
	public void OnDrop(PointerEventData eventData)
	{

		if (!item)
		{
			ButtonControl.itemBeingDragged.transform.SetParent(transform);
			
			ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x,y) => x.HasChanged());

		}
	}
}
