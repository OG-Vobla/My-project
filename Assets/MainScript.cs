using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MainScript : MonoBehaviour
{
    static public Player player;
	[SerializeField] private GameObject TimerText;
	[SerializeField] private GameObject NickName;
	[SerializeField] private GameObject Energy;
	[SerializeField] private GameObject Money;
	[SerializeField] private GameObject Lvl;
	[SerializeField] private GameObject Exp;
	[SerializeField] private Transform MessagePanel;
	[SerializeField] private Image LvlUpReward;
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

	float timeForNewEnergy = 900f;
	public static bool MessageIsOpen = false;
	public static bool LvlUpIsOpen = false;
	public void OpenMessage()
	{
		MessageIsOpen = true;
		Debug.Log("NoOpen");
		MessagePanel.GetComponent<Animator>().SetBool("IsOpen", MessageIsOpen);
		MessagePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(819, 0);
	}
	public void CloseMessage()
	{
		MessageIsOpen = false;
		Debug.Log("Open");
		MessagePanel.GetComponent<Animator>().SetBool("IsOpen", MessageIsOpen);
		MessagePanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0 );

	}

	void Awake()
	{
		
		player = new Player("Boss", 100, 1, 10, 1);
		Debug.Log(player.NickName);
		Debug.Log(player.Energy.ToString());
		Debug.Log(player.Money.ToString());
		Debug.Log(player.Lvl.ToString());
		Debug.Log($"{player.Exp}/{player.ExpToNewLvl}");
		NickName.GetComponent<Text>().text = player.NickName;
		Energy.GetComponent<Text>().text = player.Energy.ToString();
		Money.GetComponent<Text>().text = player.Money.ToString();
		Lvl.GetComponent<Text>().text = player.Lvl.ToString();
		Exp.GetComponent<Text>().text = ($"{player.Exp}/{player.ExpToNewLvl}");
		//UpdateRewards();
	}

	public void UpdateRewards()
	{
		if (player.Lvl >= 1)
		{
			SvitokBtn.SetActive(true);	
			SvitokImage.SetActive(false);
			LvlUpReward.sprite = AtomBtn.GetComponent<Image>().sprite;
			if (player.Lvl >= 2)
			{
				AtomBtn.SetActive(true);
				AtomImage.SetActive(false);
				LvlUpReward.sprite = OlimpBtn.GetComponent<Image>().sprite;
				if (player.Lvl >= 3)
				{
					OlimpBtn.SetActive(true);
					OlimpImage.SetActive(false);
					LvlUpReward.sprite = SovaBtn.GetComponent<Image>().sprite;
					if (player.Lvl >= 4)
					{
						SovaBtn.SetActive(true);
						SovaImage.SetActive(false);
						LvlUpReward.sprite = CoobokBtn.GetComponent<Image>().sprite;
						if (player.Lvl >= 5)
						{
							CoobokBtn.SetActive(true);
							CoobokImage.SetActive(false);
							LvlUpReward.sprite = BrainBtn.GetComponent<Image>().sprite;
							if (player.Lvl >= 6)
							{
								BrainBtn.SetActive(true);
								BrainImage.SetActive(false);
								LvlUpReward.sprite = CoolCoobokBtn.GetComponent<Image>().sprite;
								if (player.Lvl >= 7)
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
	void FixedUpdate()
	{
		if (timeForNewEnergy > 0)
		{
			timeForNewEnergy -= Time.deltaTime;
		}
		else
		{
			timeForNewEnergy = 900f;
			Energy.GetComponent<Text>().text = (int.Parse(Energy.GetComponent<Text>().text) + 1).ToString();
			player.Energy = player.Energy +  1;
			MessagePanel.GetComponentInChildren<Text>().text = "Ура! + 1 Енергия";
			OpenMessage();
		}
		DisplayTime(timeForNewEnergy);

	}
	void DisplayTime(float time)
	{
		if (time < 0)
		{
			time = 0;
		}
		float minutes = Mathf.FloorToInt(time / 60);
		float sec = Mathf.FloorToInt(time % 60);
		TimerText.GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, sec);
	}

}
