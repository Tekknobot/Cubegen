using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionCode2D.Renderers;

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

    float speed;  
    public GameObject bullet; 

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

        if (turn) {
            Cursor.lockState = CursorLockMode.None;
        }

        if (pushed) {
            
        }
        else if (!turn && !this.GetComponent<NPCMove>().attacking) {
            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = idleAnimation; 
            moveSpeed = 2;  
            if (GameObject.Find("Map").GetComponent<SpawnUnits>().spawned == true && GetComponent<NPCAttack>().meleeUnit == true) { 
                GetComponent<SpriteGhostTrailRenderer>().enabled = false;        
            }                                
            return;
        }

        if (!moving && !this.GetComponent<NPCMove>().attacking) {    
            Animator animator = this.gameObject.GetComponent<Animator>();        
            animator.runtimeAnimatorController = idleAnimation;
            if (GameObject.Find("Map").GetComponent<SpawnUnits>().spawned == true && GetComponent<NPCAttack>().meleeUnit == true) { 
                GetComponent<SpriteGhostTrailRenderer>().enabled = false;        
            }            
            moveSpeed = 2;
            MoveToTile(GetTargetTile(this.transform.gameObject));
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();           
            actualTargetTile.target = true; 
        }

        if (this.GetComponent<NPCMove>().attacking) {   
            Animator animator = this.gameObject.GetComponent<Animator>();        
            animator.runtimeAnimatorController = attackAnimation;
        }

        if (moving) {
            Cursor.lockState = CursorLockMode.Locked;
            if (GameObject.Find("Map").GetComponent<SpawnUnits>().spawned == true) {
                GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().TargetCameraOnNPC();
            }
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

        if (!this.GetComponent<NPCMove>().attacking) {
            NPCRadius();
        }

        if (this.GetComponent<NPCAttack>().currentLevel == 2) {
            this.GetComponent<NPCAttack>().damage = 2;
        }     
        if (this.GetComponent<NPCAttack>().currentLevel == 3) {
            this.GetComponent<NPCAttack>().damage = 3;
        }   
        if (this.GetComponent<NPCAttack>().currentLevel == 4) {
            this.GetComponent<NPCAttack>().damage = 4;
        }     
        if (this.GetComponent<NPCAttack>().currentLevel == 5) {
            this.GetComponent<NPCAttack>().damage = 5;
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
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 0.75f);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.transform.tag == "Player") {
                NPCAttackFunction(hitCollider.transform.gameObject);
                break;
            }
        }     
    }   

    public void PlayerWithinLaunchRadius() {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, new Vector3(0, 0, 1), out hit, 100) ||
            Physics.Raycast(this.transform.position, new Vector3(0, 0, -1), out hit, 100) ||
            Physics.Raycast(this.transform.position, new Vector3(1, 0, 0), out hit, 100) ||
            Physics.Raycast(this.transform.position, new Vector3(-1, 0, 0), out hit, 100)) {
            if (hit.transform.tag == "Player") {
                if (Vector3.Distance (hit.transform.position, this.transform.position) > 1.25f) {
                    Animator animator = this.GetComponent<Animator>();
                    animator.runtimeAnimatorController = this.GetComponent<NPCMove>().attackAnimation; 
                    if (animator.runtimeAnimatorController == this.GetComponent<NPCMove>().attackAnimation && this.GetComponent<NPCAttack>().meleeUnit == false) {
                        GetComponent<LaunchProjectile>().DrawPath(this.transform, hit.transform);
                        this.GetComponent<TacticsAttack>().GetXP(1);
                        Instantiate(bullet, this.transform.position, Quaternion.identity);                            
                    }                   
                }   
                if (Vector3.Distance (hit.transform.position, this.transform.position) > 1.25f) {
                    Tile t = hit.transform.GetComponent<PlayerMove>().GetTargetTile(hit.transform.gameObject);
                    Tile t2 = t.adjacencyList[Random.Range(0,t.adjacencyList.Count)];
                    if (t2.walkable == true && t.adjacencyList.Count > 0) {                    
                        Animator animator = this.GetComponent<Animator>();
                        animator.runtimeAnimatorController = this.GetComponent<NPCMove>().attackAnimation; 
                        if (animator.runtimeAnimatorController == this.GetComponent<NPCMove>().attackAnimation && this.GetComponent<NPCAttack>().meleeUnit == true) {        
                            audioData = GetComponent<AudioSource>();
                            audioData.PlayOneShot(clip[2], 1); 
                            GetComponent<SpriteGhostTrailRenderer>().enabled = true;                                            
                            GetComponent<RushMelee>().DrawPath(this.transform, hit.transform);
                            this.GetComponent<TacticsAttack>().GetXP(1);
                            // this.GetComponent<NPCMove>().moveSpeed = 12;                            
                            // this.GetComponent<NPCMove>().MoveToTile(t2);
                            Instantiate(bullet, this.transform.position, Quaternion.identity); 
                        }     
                    }                      
                }                                   
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
        this.GetComponent<TacticsAttack>().GetXP(1);
        Animator animator = this.gameObject.GetComponent<Animator>();
        animator.runtimeAnimatorController = this.gameObject.GetComponent<NPCMove>().attackAnimation;        
		yield return new WaitForSeconds(1f);
        attacking = false;
        hit.GetComponentInChildren<HealthBarHandler>().SetHealthBarValue((float)hit.GetComponent<PlayerAttack>().currentHP/hit.GetComponent<PlayerAttack>().maxHP);
        Instantiate(attackEffect, hit.transform.position, Quaternion.Euler(45, -45, 0));
        hit.GetComponent<PlayerMove>().pushed = true;
        Tile t = hit.GetComponent<PlayerMove>().GetTargetTile(hit.transform.gameObject);
        Tile t2 = t.adjacencyList[Random.Range(0,t.adjacencyList.Count)];
        if (t2.walkable == true && t.adjacencyList.Count > 0) {
            hit.GetComponent<PlayerMove>().MoveToTile(t2);           
            hit.GetComponent<PlayerMove>().moveSpeed = 4;      
        }        
        GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().CycleUnits();
        yield return new WaitUntil(()=> hit.GetComponent<PlayerMove>().pushed == false);
        hit.GetComponent<PlayerMove>().moveSpeed = 2; 
        //TurnManager.EndTurn();
	}

    public IEnumerator AttackNow() {
        yield return new WaitUntil(()=> moving == false);
        if (GameObject.Find("Map").GetComponent<SpawnUnits>().spawned == true) {
            this.GetComponent<NPCMove>().PlayerWithinRadius();
            GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target = GameObject.Find("Map").gameObject.transform; 
        }        
    }

    public IEnumerator LaunchNow() {
        yield return new WaitUntil(()=> moving == false);
        if (GameObject.Find("Map").GetComponent<SpawnUnits>().spawned == true) {
            this.GetComponent<NPCMove>().PlayerWithinLaunchRadius();
            GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target = GameObject.Find("Map").gameObject.transform; 
        }        
    }    

    public void NPCRadius()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 1f);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.transform.tag == "Player") {
                if (this.transform.position.x < hitCollider.transform.position.x) {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                if (this.transform.position.x > hitCollider.transform.position.x) {
                    GetComponent<SpriteRenderer>().flipX = true;
                }     
                if (this.transform.position.z < hitCollider.transform.position.z) {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                if (this.transform.position.z > hitCollider.transform.position.z) {
                    GetComponent<SpriteRenderer>().flipX = true;
                }                           
                break;
            }
        }
    }      
}
