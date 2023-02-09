using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLvlScript : MonoBehaviour
{
	[SerializeField] private GameObject GamePanel; 
	[SerializeField] private GameObject TemaPanel;
	bool GameIsOpen = false;
	bool TemaIsOpen = false;
	public void OpenGame()
	{
		Debug.Log("NoOpen");
		GameIsOpen = true;
		GamePanel.GetComponent<Animator>().SetBool("IsOpen", GameIsOpen);
		GamePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(443.5f, 51.8f);
	}
	public void CloseGame()
	{
		Debug.Log("Open");
		GameIsOpen = false;
		GamePanel.GetComponent<Animator>().SetBool("IsOpen", GameIsOpen);
		GamePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(443.5f, -41f);

	}

	public void OpenTema()
	{
		Debug.Log("NoOpen");
		TemaIsOpen = true;
		TemaPanel.GetComponent<Animator>().SetBool("IsOpen", TemaIsOpen);
		TemaPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(443.5f, 51.8f);
	}
	public void CloseTema()
	{
		
		Debug.Log("Open");
		TemaIsOpen = false;
		TemaPanel.GetComponent<Animator>().SetBool("IsOpen", TemaIsOpen);
		TemaPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(443.5f, -41f);

	}
	public void Home()
	{
		//CloseTema();
		CloseGame();
	}
}
