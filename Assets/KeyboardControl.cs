using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

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
	[SerializeField] private GameObject Energy;
	[SerializeField] private GameObject Money;
	[SerializeField] private Transform MessagePanel;
	[SerializeField] private Transform LvlUpPanel;
	[SerializeField] private GameObject Exp;
	[SerializeField] private GameObject Lvl; 
	[SerializeField] private UnityEngine.UI.Image LvlUpReward;
	[SerializeField] private GameObject SvitokBtn;
	[SerializeField] private GameObject SvitokImage;
	[SerializeField] private GameObject CoobokBtn;
	[SerializeField] private GameObject CoobokImage;
	[SerializeField] private GameObject CoolCoobokBtn;
	[SerializeField] private GameObject CoolCoobokImage;
	[SerializeField] private GameObject BrainBtn;
	[SerializeField] private GameObject BrainImage;
	[SerializeField] private GameObject SovaBtn;
	[SerializeField] private GameObject SovaImage;
	[SerializeField] private GameObject AtomBtn;
	[SerializeField] private GameObject AtomImage;
	[SerializeField] private GameObject OlimpBtn;
	[SerializeField] private GameObject OlimpImage;
	[SerializeField] private GameObject EnergyLvlReward;
	[SerializeField] private GameObject MoneyLvlReward;
	[SerializeField] private GameObject ContinBtn;
	[SerializeField] private GameObject NextBtn;
	[SerializeField] private GameObject HomeBtn;
	public static bool MessageIsOpen = false;
	private float timeForAnswer = 90f;
	public static Question question;
	public static List<Question> ques;
	public static List<Question> CompleteQues;
	private List<int> RndPlace;
	private int numOfLastSym;
	private List<GameObject> ButtonsInAnswer;
	bool TimerIsStart = false;
	bool AnswerIsCorect = false;
	public static bool LvlUpIsOpen = false;
	public void StartGame(string Category)
	{
		
		if(MainScript.player.Energy == 0)
		{
			MessagePanel.GetComponentInChildren<Text>().text = "У вас недостаточно енергии, но вы можете её купить! ";
			OpenMessage();
			return;
		}
		MainScript.player.Energy = MainScript.player.Energy - 1;
		Energy.GetComponent<Text>().text = MainScript.player.Energy.ToString();
		TimerIsStart = true;
		MainCanvas = newMainCanvas;
		ques = new List<Question>();
		foreach (var i in Question.FindAllInDb())
		{
			if ((Category == i.Category || Category == "Случайные" ) )
			{
				foreach (var a in CompleteQues)
				{

				}
				
			}
		}
		Debug.Log("Kol ques " + ques.Count); 
		Debug.Log("Kol compques " + CompleteQues.Count);
		if (ques.Count == 0)
		{
			MessagePanel.GetComponentInChildren<Text>().text = "Вы ответили на все вопросы.";
			OpenMessage();
			return;
		}
		question = ques[UnityEngine.Random.Range(0, ques.Count)];
		DialogBegin.GetComponent<Text>().text = "Внимание вопрос:";
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
	public void OpenMessage()
	{
		MessageIsOpen = true;
		Debug.Log("NoOpen");
		MessagePanel.GetComponent<Animator>().SetBool("IsOpen", MessageIsOpen);
		MessagePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(819, 0);
	}
	public void StopGame()
	{
		if (Panel.childCount!= 0)
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
	public void NextQues()
	{
		NextBtn.SetActive(false);
		if (AnswerIsCorect)
		{
			StopGame();
			StartGame(question.Category);
		}
		else
		{
			StopGame();
			HomeBtn.GetComponent<Button>().onClick.Invoke();
		}
		

		
	}
	void Start()
    {
		UpdateRewards();
		CompleteQues = new List<Question>();

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
				DialogBegin.GetComponent<Text>().text = "Время вышло";
				DialogBegin.GetComponent<Text>().color = Color.red;
				AnswerIsCorect = false;
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
				QuesTextBox.GetComponent<Text>().text = "Правильный ответ был:" + KeyboardControl.question.Answer;
				DialogBegin.GetComponent<Text>().text = "Не правильный ответ.";
				DialogBegin.GetComponent<Text>().color = Color.red;
				TimerIsStart = false;
				NextBtn.SetActive(true);
				AnswerIsCorect = false;
				return;
			}

		}
		
		QuesTextBox.GetComponent<Text>().text = "Правильный ответ был:" + KeyboardControl.question.Answer;
		DialogBegin.GetComponent<Text>().text = " Правильный ответ";
		DialogBegin.GetComponent<Text>().color = Color.green;
		NextBtn.SetActive(true);
		AnswerIsCorect = true;
		CompleteQues.Add(question);
		if (MainScript.player.ExpToNewLvl <= MainScript.player.Exp + 100)
		{
			LvlUpOpen();
			
		}
		MainScript.player.Exp += 100;
		Exp.GetComponent<Text>().text = ($"{MainScript.player.Exp}/{MainScript.player.ExpToNewLvl}");
		Lvl.GetComponent<Text>().text = MainScript.player.Lvl.ToString();
		TimerIsStart = false;
	}
	public void LvlUpOpen()
	{
		LvlUpIsOpen = true;
		Debug.Log("NoOpen");
		LvlUpPanel.GetComponent<Animator>().SetBool("IsOpen", LvlUpIsOpen);
		LvlUpPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(442, 253);
	}
	public void LvlUpClose()
	{
		LvlUpIsOpen = false;
		Debug.Log("NoOpen");
		LvlUpPanel.GetComponent<Animator>().SetBool("IsOpen", LvlUpIsOpen);
		LvlUpPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-701, 253);
		MainScript.player.Energy += int.Parse(EnergyLvlReward.GetComponent<Text>().text);
		MainScript.player.Money += int.Parse(MoneyLvlReward.GetComponent<Text>().text);
		Energy.GetComponent<Text>().text = MainScript.player.Energy.ToString();
		Money.GetComponent<Text>().text = MainScript.player.Money.ToString();
		UpdateRewards();
	}
	public void UpdateRewards()
	{
		if (MainScript.player.Lvl >= 1)
		{
			
			SvitokBtn.SetActive(true);
			SvitokImage.SetActive(false);
			LvlUpReward.sprite = AtomBtn.GetComponent<UnityEngine.UI.Image>().sprite;
			EnergyLvlReward.GetComponent<Text>().text = "100";
			MoneyLvlReward.GetComponent<Text>().text = "10";
			if (MainScript.player.Lvl >= 2)
			{
				AtomBtn.SetActive(true);
				AtomImage.SetActive(false);
				LvlUpReward.sprite = OlimpBtn.GetComponent<UnityEngine.UI.Image>().sprite;
				EnergyLvlReward.GetComponent<Text>().text = "200";
				MoneyLvlReward.GetComponent<Text>().text = "15";
				if (MainScript.player.Lvl >= 3)
				{
					OlimpBtn.SetActive(true);
					OlimpImage.SetActive(false);
					LvlUpReward.sprite = SovaBtn.GetComponent<UnityEngine.UI.Image>().sprite;
					EnergyLvlReward.GetComponent<Text>().text = "300";
					MoneyLvlReward.GetComponent<Text>().text = "20";
					if (MainScript.player.Lvl >= 4)
					{
						SovaBtn.SetActive(true);
						SovaImage.SetActive(false);
						LvlUpReward.sprite = CoobokBtn.GetComponent<UnityEngine.UI.Image>().sprite;
						EnergyLvlReward.GetComponent<Text>().text = "400";
						MoneyLvlReward.GetComponent<Text>().text = "25";
						if (MainScript.player.Lvl >= 5)
						{
							CoobokBtn.SetActive(true);
							CoobokImage.SetActive(false);
							LvlUpReward.sprite = BrainBtn.GetComponent<UnityEngine.UI.Image>().sprite;
							EnergyLvlReward.GetComponent<Text>().text = "500";
							MoneyLvlReward.GetComponent<Text>().text = "30";
							if (MainScript.player.Lvl >= 6)
							{
								BrainBtn.SetActive(true);
								BrainImage.SetActive(false);
								LvlUpReward.sprite = CoolCoobokBtn.GetComponent<UnityEngine.UI.Image>().sprite;
								EnergyLvlReward.GetComponent<Text>().text = "600";
								MoneyLvlReward.GetComponent<Text>().text = "35";
								if (MainScript.player.Lvl >= 7)
								{
									CoolCoobokBtn.SetActive(true);
									CoolCoobokImage.SetActive(false);


								}
							}
						}
					}
				}
			}
		}
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