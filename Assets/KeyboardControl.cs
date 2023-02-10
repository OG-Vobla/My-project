using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class KeyboardControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject btn;
	[SerializeField] private GameObject AnswerBtn;
	[SerializeField] private Transform Panel;
	[SerializeField] private Transform AnswerPanel;
	static public Canvas MainCanvas;
	[SerializeField] private Canvas newMainCanvas;
	[SerializeField] private GameObject QuesTextBox;
	[SerializeField] private GameObject DialogBegin;
	[SerializeField] private GameObject TimerText;
	private float timeForAnswer = 90f;
	public static Question question;
    private List<int> RndPlace;
	private int numOfLastSym;
	private List<GameObject> ButtonsInAnswer;
	bool TimerIsStart = false;
	public void StartGame()
	{
		TimerIsStart = true;
		MainCanvas = newMainCanvas;
		question = Question.FindAllInDb()[0];
		DialogBegin.GetComponent<Text>().text = "Vnimanie vopros:";
		DialogBegin.GetComponent<Text>().color = Color.blue;
		QuesTextBox.GetComponent<Text>().text = question.Description;
		question.Answer = question.Answer.ToUpper();
		RndPlace = new List<int>();
		ButtonsInAnswer = new List<GameObject>();
		for (int i = 0; i < question.Answer.Length; i++)
		{
			RndPlace.Add(UnityEngine.Random.Range(0, 29));
			while (RndPlace.Any(s => s == RndPlace[i] && RndPlace.IndexOf(s) != i))
			{
				RndPlace[i] = (UnityEngine.Random.Range(0, 29));
			}
			Debug.Log(RndPlace[i]);
			var newBtn = Instantiate(AnswerBtn, AnswerPanel);
			/*
			newBtn.GetComponentInChildren<Text>().text = "";
			ButtonsInAnswer.Add(newBtn);*/
		}
		for (int i = 0; i < 30; i++)
		{
			var oldBtn = Instantiate(AnswerBtn, Panel);
			var newBtn = Instantiate(btn, Panel);
			newBtn.transform.SetParent(oldBtn.transform);
			newBtn.GetComponentInChildren<Text>().text = ((char)UnityEngine.Random.Range(1040, 1072)).ToString();
			for (int j = 0; j < RndPlace.Count; j++)
			{
				if (RndPlace[j] == i)
				{
					newBtn.GetComponentInChildren<Text>().text = question.Answer[j].ToString();

				}
			}

		}
	}
	public void StopGame()
	{
		if (Panel.GetChildCount()!= 0)
		{
			TimerIsStart = false;
			timeForAnswer = 90f;
			for (int i = 0; i < 30; i++)
			{
				Destroy(Panel.GetChild(i).gameObject);

			}
			for (int i = 0; i < question.Answer.Length; i++)
			{
				Destroy(AnswerPanel.GetChild(i).gameObject);
			}
		}

	}
	void Start()
    {


	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if (TimerIsStart)
		{
			if (timeForAnswer > 0)
			{
				timeForAnswer -= Time.deltaTime;
			}
			else
			{
				timeForAnswer = 0;
				QuesTextBox.GetComponent<Text>().text = "";
				DialogBegin.GetComponent<Text>().text = "Vrem'a vushlo";
				DialogBegin.GetComponent<Text>().color = Color.red;

			}
			DisplayTime(timeForAnswer);
		}


	}
	void DisplayTime(float time)
	{
		if(time < 0)
		{
			time = 0;
		}
		float minutes = Mathf.FloorToInt(time/60);
		float sec = Mathf.FloorToInt(time % 60);
		TimerText.GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, sec);
	}
	public void CheckAnswer()
	{
		for (int i = 0; i < AnswerPanel.childCount; i++)
		{
			if (KeyboardControl.question.Answer[i].ToString() != AnswerPanel.GetChild(i).GetComponentInChildren<UnityEngine.UI.Button>().GetComponentInChildren<Text>().text)
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

namespace UnityEngine.EventSystems
{
	public interface IHasChanged : IEventSystemHandler
	{
		void HasChanged()
		{
		}
	}
}