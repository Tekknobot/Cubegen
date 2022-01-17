using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool checkTile = false;

    public GameObject attackEffect;
    public bool attacking = false;

    public bool unitTurn = false;

	// Use this for initialization
	void Start () 
	{
        Init();  
        spriteDefault = GetComponent<SpriteRenderer>().material;   

        tacticsCamera = GameObject.Find("TacticsCamera");   
	}
	
	// Update is called once per frame
	void Update () 
	{
        Debug.DrawRay(transform.position, transform.forward);

        if (!turn) {
            Animator animator = this.gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = idleAnimation;  
            return;
        }

        if (!moving && this.gameObject.GetComponent<PlayerMove>().attacking && this.gameObject.GetComponent<PlayerMove>().unitTurn) {
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
            GameObject.Find("UI_Manager").GetComponent<UI_Manager>().targetButton.SetActive(false);    
            GameObject.Find("UI_Manager").GetComponent<UI_Manager>().healthButton.SetActive(false);            
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "Tile") {
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
    
        if (Input.GetMouseButtonDown(0)) {
            this.transform.gameObject.GetComponent<SpriteRenderer>().material = spriteDefault;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "Player") {
                    RemoveSelectableTiles();
                    GetComponent<PlayerMove>().unitTurn = false;
                    hit.transform.gameObject.GetComponent<TacticsMove>().FindSelectableTiles();
                    hit.transform.gameObject.GetComponent<TacticsMove>().turn = true;
                    hit.transform.gameObject.GetComponent<PlayerMove>().unitTurn = true;
                    tempGO = hit.transform.gameObject;
                    //hit.transform.gameObject.GetComponent<SpriteRenderer>().material = spriteOutline;
                    tacticsCamera.GetComponent<TacticsCamera>().target = hit.collider.transform;
                }
            }            
        }    
    }
}
