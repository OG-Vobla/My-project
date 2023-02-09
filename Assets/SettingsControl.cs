using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class SettingsControl : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField] private GameObject panel;
	bool IsOpen = false;
	public void Open()
    {
		if (IsOpen)
		{
			IsOpen = false;

			panel.GetComponent<Animator>().SetBool("IsOpen", IsOpen);
			panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(443.5f, -41f);
		}
		else
		{
			IsOpen = true;
			panel.SetActive(true);
			panel.GetComponent<Animator>().SetBool("IsOpen", IsOpen);
			panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(443.5f, 51.8f);
		}
		

	}
}
