using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cakeslice;
using UnityEngine.EventSystems;
using ActionCode2D.Renderers;

public class PlayerMove : TacticsMove 
{
    public bool selected = false;

    public RuntimeAnimatorController moveAnimation;
    public RuntimeAnimatorController idleAnimation;
    public RuntimeAnimatorController attackAnimation;

    public float oldPositionX;
    public float oldPositionZ;

    public GameObject tempGO;

    public Material spriteDefault;
    public Material spriteOutline;

    public GameObject tacticsCamera;

    public GameObject attackEffect;
    public bool attacking = false;

    public Vector3 enemyTransform;

    AudioSource audioData;
    public AudioClip[] clip;    

	// Use this for initialization
	void Start () 
	{
        //Init();  
        spriteDefault = GetComponent<SpriteRenderer>().material;   
        tacticsCamera = GameObject.Find("TacticsCamera");   
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (pushed) {

        }
        else if (!turn && !this.GetComponent<PlayerMove>().attacking) {
            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = idleAnimation;
            //this.GetComponent<cakeslice.Outline>().enabled = false;    
            GetComponent<SpriteGhostTrailRenderer>().enabled = false;        
            return;
        }

        if (!turn) {
            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = idleAnimation;  
            return;
        }

        if (!moving && this.gameObject.GetComponent<PlayerMove>().attacking) {
            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = attackAnimation;                      
        }
        
        if (!moving && !this.gameObject.GetComponent<PlayerMove>().attacking) {
            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = idleAnimation;            
            //FindSelectableTiles();
            moveSpeed = 2;
            GetComponent<SpriteGhostTrailRenderer>().enabled = false;
            Select();             
        }
        
        if (moving) {
            GameObject.Find("PlayerTurnStatus_text").GetComponent<Text>().text = " ";
            GameObject.Find("EnemyTurnStatus_text").GetComponent<Text>().text = " ";            
            GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().TargetCameraOnPlayer();
            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = moveAnimation;  
            GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target = this.gameObject.transform;
            //this.transform.position = new Vector3(this.transform.position.x, 0.8889084f, this.transform.position.z);          
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

        PlayerWithinRadius();
	}

    void LateUpdate() {
        oldPositionX = transform.position.x;
        oldPositionZ = transform.position.z;
    }

    void Select() {
        if (Input.GetMouseButtonDown(0)) {
            GameObject.Find("Main Camera").GetComponent<CameraShake>().enabled = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "Player" && !EventSystem.current.IsPointerOverGameObject()) {
                    GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");
                    foreach (GameObject playerUnit in playerUnits) {
                        playerUnit.GetComponent<PlayerMove>().tempGO = null;
                    }
                    RemoveSelectableTiles();
                    hit.transform.gameObject.GetComponent<TacticsMove>().FindSelectableTiles();
                    hit.transform.gameObject.GetComponent<TacticsMove>().turn = true;
                    tempGO = hit.transform.gameObject;
                    GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().TurnOffAllOutlines();
                    GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target = this.gameObject.transform;
                    hit.transform.gameObject.GetComponent<cakeslice.Outline>().enabled = true;
                    tacticsCamera.GetComponent<TacticsCamera>().target = hit.collider.transform;                      
                }
            }            
        }    

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "NPC" && !EventSystem.current.IsPointerOverGameObject()) {
                    GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");
                    foreach (GameObject playerUnit in playerUnits) {
                        playerUnit.GetComponent<PlayerMove>().tempGO = null;
                    }
                    RemoveSelectableTiles();
                    hit.transform.gameObject.GetComponent<TacticsMove>().FindSelectableTiles();
                    hit.transform.gameObject.GetComponent<cakeslice.Outline>().enabled = true;
                    tacticsCamera.GetComponent<TacticsCamera>().target = hit.collider.transform; 
                    GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().TurnOffAllOutlines();
                }
            }            
        }

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "Tile" && !EventSystem.current.IsPointerOverGameObject()) {                    
                    Tile t = hit.collider.GetComponent<Tile>();
                    if (t.selectable && this.moving == false && turn == true) {
                        if (tempGO == null || t.current) {
                            return;
                        }
                        else {
                            tempGO.GetComponent<PlayerMove>().MoveToTile(t);
                        }
                    }
                }
            }
        }
    }     

    public void PlayerWithinRadius()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 1f);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.transform.tag == "NPC") {
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