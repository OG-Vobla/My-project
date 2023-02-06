using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAnswerScript : MonoBehaviour
{
	[SerializeField] private Transform AnswerPanel;
	[SerializeField] private GameObject QuesTextBox;
	[SerializeField] private GameObject DialogBegin;

	public void CheckAnswer()
	{
		for (int i = 0; i < AnswerPanel.childCount; i++)
		{
			if (KeyboardControl.question.Answer != AnswerPanel.GetChild(i).GetComponentInChildren<UnityEngine.UI.Button>().GetComponentInChildren<Text>().text)
			{
				QuesTextBox.GetComponent<Text>().text = "Otvet bul:" + KeyboardControl.question.Answer;
				DialogBegin.GetComponent<Text>().text = "Ne Pravilnui otvet";
				DialogBegin.GetComponent<Text>().color = Color.red;
				return;
			}


		}
		QuesTextBox.GetComponent<Text>().text = "Otvet bul:" + KeyboardControl.question.Answer;
		DialogBegin.GetComponent<Text>().text = " Pravilnui otvet";
		DialogBegin.GetComponent<Text>().color = Color.green;
	}
}
