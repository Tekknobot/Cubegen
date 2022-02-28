using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : TacticsAttack 
{
    public GameObject map;

    public GameObject tacticsCamera;
    public bool checkTile = false;

    public GameObject attackEffect;
    public bool attacked = false;

    public bool unitTurn = false;

    public GameObject targetButton;
    public GameObject healthButton;

    public GameObject tempPlayerUnit;
    public GameObject tempNPCUnit;

    public bool checkedMouse = false;

    public float speed = 0.2f;
    public float step;

	// Use this for initialization
	void Start () 
	{
        tacticsCamera = GameObject.Find("TacticsCamera");   
        map = GameObject.Find("Map");
	}
	
	// Update is called once per frame
	void Update () 
	{  
        step = speed * Time.deltaTime;
    }

    public void CheckMouse(GameObject tempPlayerUnit) {
        StartCoroutine(WaitForCheck(tempPlayerUnit));       
    }

    public void OnTargetButton() {
        tempPlayerUnit = tacticsCamera.GetComponent<TacticsCamera>().target.gameObject;
        CheckMouse(tempPlayerUnit);
    }

	IEnumerator PlayerAttackCoroutine(GameObject hit, GameObject tempPlayerUnit) {
        tempPlayerUnit.GetComponent<PlayerMove>().attacking = true;
		yield return new WaitForSeconds(1f);
        hit.GetComponent<TacticsAttack>().TakeDamage(tempPlayerUnit.GetComponent<TacticsAttack>().damage);
        Instantiate(attackEffect, hit.transform.position, Quaternion.Euler(45, -45, 0)); 
        hit.GetComponent<NPCMove>().pushed = true;
        Tile t = hit.GetComponent<NPCMove>().GetTargetTile(hit.transform.gameObject);
        Tile t2 = t.adjacencyList[Random.Range(0,t.adjacencyList.Count)];
        hit.GetComponent<NPCMove>().MoveToTile(t2);
        hit.GetComponent<NPCMove>().moveSpeed = 4;               
        yield return new WaitUntil(()=> hit.GetComponent<NPCMove>().pushed == false);
        hit.GetComponent<NPCMove>().moveSpeed = 2;      
        tempPlayerUnit.GetComponent<PlayerMove>().attacking = false;
        hit.GetComponent<NPCMove>().RemoveSelectableTiles();
        //TurnManager.EndTurn();
	}  

    IEnumerator WaitForCheck(GameObject tempPlayerUnit) {
        yield return new WaitUntil(()=> Input.GetMouseButtonDown(0));
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.625f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "NPC") {
                tempNPCUnit = tacticsCamera.GetComponent<TacticsCamera>().target.gameObject;
                Animator animator = tempPlayerUnit.GetComponent<Animator>();
                animator.runtimeAnimatorController = tempPlayerUnit.GetComponent<PlayerMove>().attackAnimation;                
                StartCoroutine(PlayerAttackCoroutine(tempNPCUnit.transform.gameObject, tempPlayerUnit));
            }
        }
        checkedMouse = true;                
    }
}
