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
        PlayerDrawRayForward();

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
            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = moveAnimation;            
            Move();             
            GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().TargetCameraOnPlayer();
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
                    RemoveSelectableTiles();
                    GetComponent<PlayerMove>().unitTurn = false;
                    hit.transform.gameObject.GetComponent<TacticsMove>().FindSelectableTiles();
                    hit.transform.gameObject.GetComponent<TacticsMove>().turn = true;
                    hit.transform.gameObject.GetComponent<PlayerMove>().unitTurn = true;
                    tempGO = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<cakeslice.Outline>().enabled = true;
                    tacticsCamera.GetComponent<TacticsCamera>().target = hit.collider.transform;
                    GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().TurnOffNPCOutlines();  
                }
            }            
        }    

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "NPC" && !EventSystem.current.IsPointerOverGameObject()) {
                    hit.transform.gameObject.GetComponent<cakeslice.Outline>().enabled = true;
                    tacticsCamera.GetComponent<TacticsCamera>().target = hit.collider.transform;
                    //GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().TurnOffPlayerOutlines(); 
                }
            }            
        }

        if (Input.GetMouseButtonDown(0) && GetComponent<PlayerMove>().turn) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "Tile" && !EventSystem.current.IsPointerOverGameObject()) {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable && this.moving == false) {
                        if (tempGO == null) {
                            return;
                        }
                        else {
                            tempGO.GetComponent<TacticsMove>().MoveToTile(t);
                            tempGO = this.transform.gameObject;
                        }
                    }
                }
            }
        }
    }    

    public void PlayerDrawRayForward() {
        RaycastHit objectHit;        
        // Shoot raycast
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out objectHit, 50)) {
            Debug.DrawRay(transform.position, new Vector3(0, -1, 0));
        }        
    }       
}