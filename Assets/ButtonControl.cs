using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonControl : MonoBehaviour, IBeginDragHandler, IEndDragHandler,IDragHandler
{
	public static GameObject itemBeingDragged;
	Vector2 startPosition;
	private RectTransform rectTransform;
	private CanvasGroup canvasGroup;
	public static Transform startParent;
	private void Awake()
	{
			rectTransform = GetComponent<RectTransform>();
		canvasGroup= GetComponent<CanvasGroup>();
	}
	public void OnBeginDrag(PointerEventData eventData)
	{
		canvasGroup.blocksRaycasts= false;
		itemBeingDragged = gameObject;
		startPosition= transform.position;
		startParent = transform.parent;
	}

	public void OnDrag(PointerEventData eventData)
	{
		rectTransform.anchoredPosition += eventData.delta / KeyboardControl.MainCanvas.scaleFactor;

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		canvasGroup.blocksRaycasts = true;
		itemBeingDragged = null;
		if(transform.parent == startParent)
		{
			transform.position = startPosition;
		}

	}

}
