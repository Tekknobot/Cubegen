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
    public GameObject healthEffect;
    public bool attacked = false;

    public bool unitTurn = false;

    public GameObject targetButton;
    public GameObject healthButton;

    public GameObject tempPlayerUnit;
    public GameObject tempNPCUnit;

    public bool checkedMouse = false;

    public float speed = 0.2f;
    public float step;

    AudioSource audioData;
    public AudioClip[] clip;

    public bool flag = false;

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

    public void OnHealthButton() {
        tempPlayerUnit = tacticsCamera.GetComponent<TacticsCamera>().target.gameObject;
        Instantiate(healthEffect, new Vector3(tempPlayerUnit.transform.position.x, tempPlayerUnit.transform.position.y-0.5f, tempPlayerUnit.transform.position.z), Quaternion.Euler(270, 0, 0)); 
        tempPlayerUnit.GetComponent<PlayerAttack>().Heal(tempPlayerUnit.GetComponent<PlayerAttack>().healthUp);
        tempPlayerUnit.GetComponentInChildren<HealthBarHandler>().SetHealthBarValue(((float)tempPlayerUnit.GetComponent<PlayerAttack>().currentHP/tempPlayerUnit.GetComponent<PlayerAttack>().maxHP));     
        //TurnManager.EndTurn();
    }    

	IEnumerator PlayerAttackCoroutine(GameObject hit, GameObject tempPlayerUnit) {
        tempPlayerUnit.GetComponent<PlayerMove>().attacking = true;
        audioData = GetComponent<AudioSource>();
        audioData.PlayOneShot(clip[0], 1);       
		yield return new WaitForSeconds(1f);
        hit.GetComponent<TacticsAttack>().TakeDamage(tempPlayerUnit.GetComponent<TacticsAttack>().damage);
        hit.GetComponentInChildren<HealthBarHandler>().SetHealthBarValue((float)hit.GetComponent<NPCAttack>().currentHP/hit.GetComponent<NPCAttack>().maxHP);
        Instantiate(attackEffect, hit.transform.position, Quaternion.Euler(45, -45, 0)); 
        hit.GetComponent<NPCMove>().pushed = true;
        Tile t = hit.GetComponent<NPCMove>().GetTargetTile(hit.transform.gameObject);
        Tile t2 = t.adjacencyList[Random.Range(0,t.adjacencyList.Count)];
        if (t2.walkable == true) {
            hit.GetComponent<NPCMove>().MoveToTile(t2);
            hit.GetComponent<NPCMove>().moveSpeed = 4;               
        }
        yield return new WaitUntil(()=> hit.GetComponent<NPCMove>().pushed == false);
        hit.GetComponent<NPCMove>().moveSpeed = 2;      
        tempPlayerUnit.GetComponent<PlayerMove>().attacking = false;
        hit.GetComponent<NPCMove>().RemoveSelectableTiles();
        flag = false;
        //TurnManager.EndTurn();
	}  

    IEnumerator WaitForCheck(GameObject tempPlayerUnit) {
        yield return new WaitUntil(()=> Input.GetMouseButtonDown(0));
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 1f);
        foreach (var hitCollider in hitColliders)
        {         
            if (hitCollider.tag == "NPC") {
                Animator animator = tempPlayerUnit.GetComponent<Animator>();
                animator.runtimeAnimatorController = tempPlayerUnit.GetComponent<PlayerMove>().attackAnimation; 
                if (flag == false) {
                    StartCoroutine(PlayerAttackCoroutine(hitCollider.transform.gameObject, tempPlayerUnit));
                    flag = true;
                }               
            }
            if (hitCollider.tag == "Player") {
                break;
            }
        }
        checkedMouse = true;                
    }
}
