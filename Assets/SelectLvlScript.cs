using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLvlScript : MonoBehaviour
{
	[SerializeField] private GameObject GamePanel; 
	[SerializeField] private GameObject TemaPanel;
	public static bool GameIsOpen = false;
	public static bool TemaIsOpen = false;
	public void OpenGame()
	{
		GameIsOpen = true;
		Debug.Log("NoOpen");
		GamePanel.GetComponent<Animator>().SetBool("IsOpen", GameIsOpen);
		GamePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(443.5f, 51.8f);
	}
	public void CloseGame()
	{
		GameIsOpen = false;
		Debug.Log("Open");
		GamePanel.GetComponent<Animator>().SetBool("IsOpen", GameIsOpen);
		GamePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(443.5f, -41f);

	}

	public void OpenTema()
	{
		TemaIsOpen = true;
		Debug.Log("NoOpen");
		TemaPanel.GetComponent<Animator>().SetBool("IsOpen", TemaIsOpen);
		TemaPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(443.5f, 51.8f);
	}
	public void CloseTema()
	{
		TemaIsOpen = false;

		Debug.Log("Open");
		TemaPanel.GetComponent<Animator>().SetBool("IsOpen", TemaIsOpen);
		TemaPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(443.5f, -41f);

	}
	public void Home()
	{
		if(TemaIsOpen)
		{
			CloseTema();
		}
		if (GameIsOpen)
		{
			CloseGame();
		}
		
	}
}
