using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnEnemyCurvedLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject objectToMove;
    public float speed;

    private Vector3[] positions = new Vector3[397];
    private Vector3[] pos;
    private int index = 0;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GameObject.Find("LineRenderer").GetComponent<LineRenderer>();
        lineRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.3f));
        pos = GetLinePointsInWorldSpace();
        objectToMove = this.gameObject;
        objectToMove.transform.position = pos[index];
    }

    Vector3[] GetLinePointsInWorldSpace()
    {
        //Get the positions which are shown in the inspector 
        lineRenderer.GetPositions(positions);
        
        //the points returned are in world space
        return positions;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        lineRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.3f));
    }

    void Move()
    {
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, pos[index], speed * Time.deltaTime);

        if (objectToMove.transform.position == pos[index]) {
            index += 1;
        }

        if (index == pos.Length) {
            index = 0;
            this.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Tile") {
            Instantiate(explosion, transform.position, Quaternion.Euler(45, -45, 0));
            GameObject[] npcUnits;
            npcUnits = GameObject.FindGameObjectsWithTag("NPC");
            foreach (GameObject npcUnit in npcUnits) {
                collision.transform.GetComponent<TacticsAttack>().TakeDamage(npcUnit.GetComponent<NPCAttack>().damage);
                break;
            }            
            collision.transform.GetComponentInChildren<HealthBarHandler>().SetHealthBarValue((float)collision.transform.GetComponent<PlayerAttack>().currentHP/(float)collision.transform.GetComponent<PlayerAttack>().maxHP);
            collision.transform.GetComponent<PlayerMove>().pushed = true;
            Tile t = collision.transform.GetComponent<PlayerMove>().GetTargetTile(collision.transform.gameObject);
            Tile t2 = t.adjacencyList[Random.Range(0,t.adjacencyList.Count)];
            if (t2.walkable == true && t.adjacencyList.Count > 0) {
                collision.transform.GetComponent<PlayerMove>().MoveToTile(t2);           
                collision.transform.GetComponent<PlayerMove>().moveSpeed = 4;      
            }              
            Destroy(this.gameObject);
        }
    }    
}