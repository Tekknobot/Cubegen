using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour 
{
    public Dictionary<string, List<TacticsMove>> units = new Dictionary<string, List<TacticsMove>>();
    public Queue<string> turnKey = new Queue<string>();
    public Queue<TacticsMove> turnTeam = new Queue<TacticsMove>();

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (turnTeam.Count == 0) {
            InitTeamTurnQueue();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public void InitTeamTurnQueue()
    {
        List<TacticsMove> teamList = this.units[turnKey.Peek()];

        foreach (TacticsMove unit in teamList) {
            turnTeam.Enqueue(unit);
        }

        StartTurn();
        GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().TurnOffAllOutlines();
    }

    public void StartTurn()
    {
        if (turnTeam.Count > 0) {
            turnTeam.Peek().BeginTurn();
        }
    }

    public void EndTurn()
    {
        TacticsMove unit = turnTeam.Dequeue();
        unit.FinishTurn();

        if (turnTeam.Count > 0) {
            StartTurn();
        }
        else {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
            StartTurn();
        }  
    }    

    public void AddUnit(TacticsMove unit)
    {
        List<TacticsMove> list;

        if (!this.units.ContainsKey(unit.tag))
        {
            list = new List<TacticsMove>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
    }
}
