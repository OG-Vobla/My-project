using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	public Player(string nickName, int money, int energy, int exp, int lvl)
	{
		NickName = nickName;
		Money = money;
		Energy = energy;
		Lvl = lvl;
		Exp = exp;
	}
	private int lvl;
	private int exp;
	public string NickName { get; set; }
	public int Money { get; set; }
	public int Energy { get; set; }
	public int Exp
	{
		get { return exp; }
		set
		{
			if (value >= ExpToNewLvl)
			{
				exp = value - ExpToNewLvl;
				Lvl++;
			}
			else
			{
				exp = value;
			}
		}
	}
	public int ExpToNewLvl { get; set; }
	public int Lvl
	{
		get { return lvl; }
		set
		{
			int newExp = 100;
			for (int i = 1; i <= lvl; i++)
			{
				newExp += i * 100;
			}
			ExpToNewLvl = newExp;
			lvl = value;
		}
	}
}
