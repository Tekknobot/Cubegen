using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticsMove 
{
    public GameObject target;

    public RuntimeAnimatorController moveAnimation;
    public RuntimeAnimatorController idleAnimation;
    public RuntimeAnimatorController attackAnimation;

    public float oldPositionX;
    public float oldPositionZ;

    public Material spriteDefault;
    public Material spriteOutline;    

    public GameObject tacticsCamera;
    public GameObject attackEffect;
    public bool attacking = false;

    AudioSource audioData;
    public AudioClip[] clip;    

	// Use this for initialization
	void Start () 
	{
        //Init();
        tacticsCamera = GameObject.Find("TacticsCamera"); 
	}
	
	// Update is called once per frame
	void Update () 
	{
        Debug.DrawRay(transform.position, new Vector3(0, 0, 1)); 
        Debug.DrawRay(transform.position, new Vector3(0, 0, -1));
        Debug.DrawRay(transform.position, new Vector3(1, 0, 0));
        Debug.DrawRay(transform.position, new Vector3(-1, 0, 0));

        if (pushed) {
            
        }
        else if (!turn && !this.GetComponent<NPCMove>().attacking) {
            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = idleAnimation;           
            return;
        }

        if (!moving && !this.GetComponent<NPCMove>().attacking) {    
            Animator animator = this.gameObject.GetComponent<Animator>();        
            animator.runtimeAnimatorController = idleAnimation;
            MoveToTile(GetTargetTile(this.gameObject));
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();
            actualTargetTile.target = true;            
            pushed = false;
        }

        if (this.GetComponent<NPCMove>().attacking) {    
            Animator animator = this.gameObject.GetComponent<Animator>();        
            animator.runtimeAnimatorController = attackAnimation;
        }

        if (moving) {
            GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().TargetCameraOnNPC();
            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = moveAnimation;             
            Move();
        }     

        if (transform.position.x > oldPositionX) {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (transform.position.x < oldPositionX) {
            GetComponent<SpriteRenderer>().flipX = true;
        }    
        if (transform.position.z > oldPositionZ) {
            GetComponent<SpriteRenderer>().flipX = false;
        }     
        if (transform.position.z < oldPositionZ) {
            GetComponent<SpriteRenderer>().flipX = true;
        }        
	}

    void LateUpdate(){
        oldPositionX = transform.position.x;
        oldPositionZ = transform.position.z;
    }       

    public void CalculatePath()
    {
        if (turn == false) {
            target = this.transform.gameObject;
            Tile targetNearestTile = GetTargetTile(target);
            FindPath(targetNearestTile);
            return;            
        }        
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
        StartCoroutine(AttackNow());
    } 

    public void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);

            if (d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }

        target = nearest;
    }

    public void PlayerWithinRadius()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(0, 0, 1), out hit, 1) ||
            Physics.Raycast(transform.position, new Vector3(0, 0, -1), out hit, 1) ||
            Physics.Raycast(transform.position, new Vector3(1, 0, 0), out hit, 1) ||
            Physics.Raycast(transform.position, new Vector3(-1, 0, 0), out hit, 1)) {
            if (hit.transform.tag == "Player") {
                NPCAttackFunction(hit.transform.gameObject);
            }
        }
    }     

    public void NPCAttackFunction(GameObject target) {
        StartCoroutine(NPCAttackCoroutine(target));
    }  

	IEnumerator NPCAttackCoroutine(GameObject hit) {
        audioData = GetComponent<AudioSource>();
        audioData.PlayOneShot(clip[1], 1);        
        ComputeAdjacencyLists(this.GetComponent<NPCMove>().jumpHeight, this.GetComponent<NPCMove>().GetTargetTile(this.gameObject));
        attacking = true;
        hit.transform.gameObject.GetComponent<TacticsAttack>().TakeDamage(this.GetComponent<TacticsAttack>().damage);
        Animator animator = this.gameObject.GetComponent<Animator>();
        animator.runtimeAnimatorController = this.gameObject.GetComponent<NPCMove>().attackAnimation;        
		yield return new WaitForSeconds(1f);
        attacking = false;
        hit.GetComponentInChildren<HealthBarHandler>().SetHealthBarValue((float)hit.GetComponent<PlayerAttack>().currentHP/hit.GetComponent<PlayerAttack>().maxHP);
        Instantiate(attackEffect, hit.transform.position, Quaternion.Euler(45, -45, 0));
        hit.GetComponent<PlayerMove>().pushed = true;
        Tile t = hit.GetComponent<PlayerMove>().GetTargetTile(hit.transform.gameObject);
        Tile t2 = t.adjacencyList[Random.Range(0,t.adjacencyList.Count)];
        if (t2.walkable == true) {
            hit.GetComponent<PlayerMove>().MoveToTile(t2);           
            hit.GetComponent<PlayerMove>().moveSpeed = 4;      
        }        
        yield return new WaitUntil(()=> hit.GetComponent<PlayerMove>().pushed == false);
        hit.GetComponent<PlayerMove>().moveSpeed = 2; 
        GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().CycleUnits();
        //TurnManager.EndTurn();
	}

    IEnumerator AttackNow() {
        yield return new WaitUntil(()=> moving == false);
        if (GameObject.Find("Map").GetComponent<SpawnUnits>().spawned == true) {
            this.GetComponent<NPCMove>().PlayerWithinRadius();
            GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target = GameObject.Find("Map").gameObject.transform; 
        }        
    }
}
