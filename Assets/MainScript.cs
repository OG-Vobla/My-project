using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MainScript : MonoBehaviour
{
    private Player player;
	[SerializeField] private GameObject TimerText;
	[SerializeField] private GameObject NickName;
	[SerializeField] private GameObject Energy;
	[SerializeField] private GameObject Money;
	[SerializeField] private GameObject Lvl;
	[SerializeField] private GameObject Exp;

	float timeForNewEnergy = 900f;
	 void Start()
	{

		player = new Player("Boss", 100, 12, 10, 1);
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
