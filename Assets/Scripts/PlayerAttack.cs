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
        Debug.DrawRay(transform.position, new Vector3(1, 0, 0));   
    }

    public void CheckMouse() {
        StartCoroutine(WaitForCheck());       
    }

    public void OnTargetButton() {
        CheckMouse();
    }

	IEnumerator PlayerAttackCoroutine(GameObject hit) {
        this.gameObject.GetComponent<PlayerMove>().attacking = true;
		yield return new WaitForSeconds(1f);
        hit.GetComponent<TacticsAttack>().TakeDamage(this.GetComponent<TacticsAttack>().damage);
        this.gameObject.GetComponent<PlayerMove>().attacking = false;
        Instantiate(attackEffect, hit.transform.position, Quaternion.Euler(45, -45, 0));        
        tacticsCamera.GetComponent<TacticsCamera>().target = hit.transform;
        TurnManager.EndTurn();
        GameObject.Find("Target_btn").GetComponent<GetPlayerClones>().flag = false;
	}  

    IEnumerator WaitForCheck() {
        yield return new WaitUntil(()=> Input.GetMouseButtonDown(0));
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 0.625f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "NPC") {
                Animator animator = this.gameObject.GetComponent<Animator>();
                animator.runtimeAnimatorController = this.gameObject.GetComponent<PlayerMove>().attackAnimation;                
                StartCoroutine(PlayerAttackCoroutine(hitCollider.transform.gameObject));
            }
        }
        checkedMouse = true;                
    }  
}
