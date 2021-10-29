using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticsMove 
{
    GameObject target;

    public RuntimeAnimatorController moveAnimation;
    public RuntimeAnimatorController idleAnimation;

    public float oldPositionX;
    public float oldPositionZ;

	// Use this for initialization
	void Start () 
	{
        Init();
	}
	
	// Update is called once per frame
	void Update () 
	{
        Debug.DrawRay(transform.position, transform.forward);

        if (!turn)
        {
            Animator animator = GameObject.FindGameObjectWithTag("NPC").gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = idleAnimation;            
            return;
        }

        if (!moving)
        {            
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();
            actualTargetTile.target = true;
        }
        else
        {
            Animator animator = GameObject.FindGameObjectWithTag("NPC").gameObject.GetComponent<Animator>();
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

    void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }

    void FindNearestTarget()
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
}
