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

    public bool checkedMouse = false;

	// Use this for initialization
	void Start () 
	{
        tacticsCamera = GameObject.Find("TacticsCamera");   
        map = GameObject.Find("Map");
	}
	
	// Update is called once per frame
	void Update () 
	{

    }

    public void CheckMouse() {
        StartCoroutine(WaitForCheck());       
    }

    public void OnTargetButton() {
        CheckMouse();
    }

	IEnumerator PlayerAttackCoroutine(RaycastHit hit) {
        this.gameObject.GetComponent<PlayerMove>().attacking = true;
		yield return new WaitForSeconds(1f);
        hit.transform.gameObject.GetComponent<TacticsAttack>().TakeDamage(this.GetComponent<TacticsAttack>().damage);
        this.gameObject.GetComponent<PlayerMove>().attacking = false;
        Instantiate(attackEffect, hit.transform.position, Quaternion.Euler(45, -45, 0));        
        tacticsCamera.GetComponent<TacticsCamera>().target = hit.collider.transform;
        TurnManager.EndTurn();
        GameObject.Find("Target_btn").GetComponent<GetPlayerClones>().flag = false;
	}  

    IEnumerator WaitForCheck() {
        yield return new WaitUntil(()=> Input.GetMouseButtonDown(0));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            float distance = Vector3.Distance(this.transform.position, hit.transform.position);
            if (hit.collider.tag == "NPC" && distance <= 1f) {
                Animator animator = this.gameObject.GetComponent<Animator>();
                animator.runtimeAnimatorController = this.gameObject.GetComponent<PlayerMove>().attackAnimation;                
                StartCoroutine(PlayerAttackCoroutine(hit));
            }
            checkedMouse = true; 
        }            
    }  
}
