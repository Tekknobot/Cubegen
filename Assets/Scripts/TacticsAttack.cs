using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsAttack : MonoBehaviour 
{
	public int damage;
	public float healthUp;

	public int maxHP;   
    public float currentHP = 5; 
	public int maxXP;
	public int currentXP;
	public int currentLevel = 1;

	public string unitName;
	public int unitLevel; 

	public GameObject levelup;    

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

	public bool GetXP(int xp)
	{
		currentXP += xp;

		if (currentXP >= maxXP) {
			Instantiate(levelup, transform.position, Quaternion.Euler(45, -45, 0));
			currentXP = 0;
			currentLevel++;
			return true;
		}
		else {
			return false;
		}
	}	 
}
