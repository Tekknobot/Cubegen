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

    public GameObject bullet;

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

        Debug.DrawRay(transform.position, new Vector3(0, 0, 1)); 
        Debug.DrawRay(transform.position, new Vector3(0, 0, -1));
        Debug.DrawRay(transform.position, new Vector3(1, 0, 0));
        Debug.DrawRay(transform.position, new Vector3(-1, 0, 0));
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
        tempPlayerUnit.GetComponentInChildren<HealthBarHandler>().SetHealthBarValue(((float)tempPlayerUnit.GetComponent<PlayerAttack>().currentHP/(float)tempPlayerUnit.GetComponent<PlayerAttack>().maxHP));     
        StartCoroutine(PlayerDodge());
    }    

    public void OnLaunchButton() {
        tempPlayerUnit = tacticsCamera.GetComponent<TacticsCamera>().target.gameObject;
        CheckMouse(tempPlayerUnit);        
    }

	IEnumerator PlayerAttackCoroutine(GameObject hit, GameObject tempPlayerUnit) {
        tempPlayerUnit.GetComponent<PlayerMove>().attacking = true;
        audioData = GetComponent<AudioSource>();
        audioData.PlayOneShot(clip[0], 1);       
		yield return new WaitForSeconds(1f);
        hit.GetComponent<TacticsAttack>().TakeDamage(tempPlayerUnit.GetComponent<TacticsAttack>().damage);
        tempPlayerUnit.GetComponent<TacticsAttack>().GetXP(1);
        hit.GetComponentInChildren<HealthBarHandler>().SetHealthBarValue((float)hit.GetComponent<NPCAttack>().currentHP/(float)hit.GetComponent<NPCAttack>().maxHP);
        Instantiate(attackEffect, hit.transform.position, Quaternion.Euler(45, -45, 0)); 
        hit.GetComponent<NPCMove>().pushed = true;
        Tile t = hit.GetComponent<NPCMove>().GetTargetTile(hit.transform.gameObject);
        if (t.adjacencyList.Count == 0) {
            hit.GetComponent<NPCMove>().moveSpeed = 2;      
            tempPlayerUnit.GetComponent<PlayerMove>().attacking = false;
            hit.GetComponent<NPCMove>().RemoveSelectableTiles();
            this.GetComponent<PlayerMove>().tempGO = null;
        }
        Tile t2 = t.adjacencyList[Random.Range(0,t.adjacencyList.Count)];   
        if (t2.walkable == true) {
            hit.GetComponent<NPCMove>().MoveToTile(t2);
            hit.GetComponent<NPCMove>().moveSpeed = 4;                
        }   
        yield return new WaitUntil(()=> hit.GetComponent<NPCMove>().pushed == false);
        hit.GetComponent<NPCMove>().moveSpeed = 2;      
        tempPlayerUnit.GetComponent<PlayerMove>().attacking = false;
        hit.GetComponent<NPCMove>().RemoveSelectableTiles();
        this.GetComponent<PlayerMove>().tempGO = null;
        //TurnManager.EndTurn();
	}  

    IEnumerator PlayerDodge() {
        Tile t = GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target.transform.GetComponent<PlayerMove>().GetTargetTile(GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target.transform.gameObject);
        if (t.adjacencyList.Count == 0) {  
            GameObject.Find("Map").GetComponent<TurnManager>().EndTurn();
            GameObject.Find("PlayerTurnStatus_text").GetComponent<Text>().text = "Player is trapped: Player turn over.";
        }          
        Tile t2 = t.adjacencyList[Random.Range(0,t.adjacencyList.Count)];
        if (t2.walkable == true) {
            GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target.transform.GetComponent<PlayerMove>().MoveToTile(t2);                 
        }  
        GameObject.Find("Health_btn").SetActive(false);
        yield return null;
    }

    IEnumerator WaitForCheck(GameObject tempPlayerUnit) {
        yield return new WaitUntil(()=> Input.GetMouseButtonDown(0));
        if (GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target.transform.gameObject) {
            if (GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target.transform.tag == "NPC") {
                if (Vector3.Distance (tempPlayerUnit.transform.position, GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target.transform.position) < 1.25f) {
                    Animator animator = tempPlayerUnit.GetComponent<Animator>();
                    animator.runtimeAnimatorController = tempPlayerUnit.GetComponent<PlayerMove>().attackAnimation; 
                    if (animator.runtimeAnimatorController == this.GetComponent<PlayerMove>().attackAnimation) {
                        StartCoroutine(PlayerAttackCoroutine(GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target.transform.gameObject, tempPlayerUnit));                     
                    }
                }
            }

            if (GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target.transform.tag == "NPC") {
                Vector3 heading = GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target.transform.position - tempPlayerUnit.transform.position;
                float distance = heading.magnitude;
                Vector3 direction = heading / distance;
                Debug.Log(direction);                        
                if (direction == new Vector3(0,0,1) || direction == new Vector3(0,0,-1) || direction == new Vector3(1,0,0) || direction == new Vector3(-1,0,0)) {
                    Debug.Log("Direction check");
                    if (Vector3.Distance (tempPlayerUnit.transform.position, GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target.transform.position) > 1.25f) {
                        Animator animator = tempPlayerUnit.GetComponent<Animator>();
                        animator.runtimeAnimatorController = tempPlayerUnit.GetComponent<PlayerMove>().attackAnimation; 
                        if (animator.runtimeAnimatorController == this.GetComponent<PlayerMove>().attackAnimation) {
                            GetComponent<LaunchProjectile>().DrawPath(tempPlayerUnit.transform, GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target.transform);
                            Instantiate(bullet, this.transform.position, Quaternion.identity);
                            //GetComponent<LaunchProjectile>().Launch(tempPlayerUnit.transform, GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target.transform);
                        }                            
                    }                   
                }
            }
        }                                  
    }    
}
