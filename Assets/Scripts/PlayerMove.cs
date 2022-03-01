using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cakeslice;
using UnityEngine.EventSystems;

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

    public bool unitTurn = false;
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
        else if (!moving && !this.gameObject.GetComponent<PlayerMove>().attacking) {
            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = idleAnimation;            
            //FindSelectableTiles();
            CheckMouse();             
        }
        else if (moving) {
            GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().TargetCameraOnPlayer();
            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = moveAnimation;            
            Move();             
            // GameObject.Find("UI_Manager").GetComponent<UI_Manager>().targetButton.SetActive(false);    
            // GameObject.Find("UI_Manager").GetComponent<UI_Manager>().healthButton.SetActive(false);            
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

        var directionToEnemy = enemyTransform - this.transform.position;
        var projectionOnRight = Vector3.Dot(directionToEnemy, this.transform.right);

        if (projectionOnRight < 0) {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (projectionOnRight > 0) {
            GetComponent<SpriteRenderer>().flipX = true;
        }        
	}

    void LateUpdate(){
        oldPositionX = transform.position.x;
        oldPositionZ = transform.position.z;
    }

    void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0)) {
            //this.GetComponent<cakeslice.Outline>().enabled = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "Player" && !EventSystem.current.IsPointerOverGameObject()) {
                    tempGO = null;
                    GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().SetUnitTurnFalse();
                    RemoveSelectableTiles();
                    hit.transform.gameObject.GetComponent<TacticsMove>().FindSelectableTiles();
                    hit.transform.gameObject.GetComponent<TacticsMove>().turn = true;
                    hit.transform.gameObject.GetComponent<PlayerMove>().unitTurn = true;
                    tempGO = hit.transform.gameObject;
                    GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().TurnOffAllOutlines();
                    GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target = this.gameObject.transform;
                    hit.transform.gameObject.GetComponent<cakeslice.Outline>().enabled = true;
                    tacticsCamera.GetComponent<TacticsCamera>().target = hit.collider.transform;  
                    //audioData = GetComponent<AudioSource>();
                    //audioData.PlayOneShot(clip[0], 1);                    
                }
            }            
        }    

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "NPC" && !EventSystem.current.IsPointerOverGameObject()) {
                    tempGO = null;
                    GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().SetUnitTurnFalse();
                    RemoveSelectableTiles();
                    hit.transform.gameObject.GetComponent<TacticsMove>().FindSelectableTiles();
                    GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().TurnOffAllOutlines();
                    hit.transform.gameObject.GetComponent<cakeslice.Outline>().enabled = true;
                    tacticsCamera.GetComponent<TacticsCamera>().target = hit.collider.transform; 
                }
            }            
        }

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "Tile" && !EventSystem.current.IsPointerOverGameObject()) {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable && this.moving == false && this.unitTurn == true) {
                        if (tempGO == null || t.current) {
                            return;
                        }
                        else {
                            tempGO.GetComponent<PlayerMove>().MoveToTile(t);
                            tempGO = this.transform.gameObject;
                        }
                    }
                }
            }
        }
    }

    public void StartSphere() {
        StartCoroutine(Sphere());
    }

    IEnumerator Sphere() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.65f);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.tag != "Player") {
                enemyTransform = hitCollider.transform.position;
            }
            yield return null;
        }              
    }                
}