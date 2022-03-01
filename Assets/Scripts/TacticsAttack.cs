﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsAttack : MonoBehaviour 
{
	public int damage;
	public float healthUp;

	public int maxHP;   
    public float currentHP = 5;    

	public string unitName;
	public int unitLevel;     

	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public void Heal(float amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}    
}
