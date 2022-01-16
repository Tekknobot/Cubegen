using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : TacticsAttack 
{
    public GameObject tacticsCamera;
    public bool checkTile = false;

    public GameObject attackEffect;
    public bool attacking = false;

    public bool unitTurn = false;

    public GameObject targetButton;
    public GameObject healthButton;

    public bool checkedMouse = false;

	// Use this for initialization
	void Start () 
	{
        tacticsCamera = GameObject.Find("TacticsCamera");   
	}
	
	// Update is called once per frame
	void Update () 
	{

    }

    void CheckMouse() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "NPC" && !checkedMouse) {
                    StartCoroutine(PlayerAttackCoroutine(hit));
                    checkedMouse = true;
                }
            }            
        }         
    }

    public void OnTargetButton() {
        CheckMouse();
    }

	IEnumerator PlayerAttackCoroutine(RaycastHit hit) {
        this.gameObject.GetComponent<PlayerMove>().attacking = true;
		hit.transform.gameObject.GetComponent<TacticsAttack>().TakeDamage(this.GetComponent<TacticsAttack>().damage);
		yield return new WaitForSeconds(1f);
        Instantiate(attackEffect, hit.transform.position, Quaternion.Euler(45, -45, 0));
        tacticsCamera.GetComponent<TacticsCamera>().target = hit.collider.transform;
        this.gameObject.GetComponent<PlayerMove>().attacking = false;
	}    
}
