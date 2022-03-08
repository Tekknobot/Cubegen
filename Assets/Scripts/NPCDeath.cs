using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDeath : MonoBehaviour
{
    public GameObject explosion;
    public bool flag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<NPCAttack>().currentHP <= 0 && this.GetComponent<NPCDeath>().flag == false) {
            StartCoroutine(DestroyObject());
            this.GetComponent<NPCDeath>().flag = true;
        }
    }

    IEnumerator DestroyObject() {           
        Tile t = this.GetComponent<NPCMove>().GetTargetTile(this.transform.gameObject);
        Tile t2 = t.adjacencyList[Random.Range(0,t.adjacencyList.Count)];
        this.GetComponent<NPCMove>().MoveToTile(t2);
        this.transform.GetComponent<NPCMove>().RemoveSelectableTiles(); 
        yield return new WaitForSeconds(0.5f);
        this.transform.GetComponent<ObjectShake>().enabled = true;
        yield return new WaitForSeconds(1f);
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerUnit in playerUnits) {
            playerUnit.transform.GetComponent<PlayerMove>().attacking = false;
        }         
        Instantiate(explosion, this.transform.position, Quaternion.Euler(45, -45, 0));
        this.transform.GetComponent<NPCMove>().enabled = false;
        this.transform.GetComponent<NPCAttack>().enabled = false;         
        this.transform.tag = "NPCDead";    
        this.transform.GetComponent<ObjectShake>().enabled = false;
        this.transform.GetComponent<SpriteRenderer>().enabled = false; 
        this.transform.GetComponentInChildren<Canvas>().enabled = false;   
        this.transform.gameObject.SetActive(false);
        GameObject.Find("Map").GetComponent<SpawnUnits>().npcDead++;   

    }
}
