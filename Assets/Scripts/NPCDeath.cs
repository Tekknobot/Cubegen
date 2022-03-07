using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDeath : MonoBehaviour
{
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<NPCAttack>().currentHP <= 0) {
            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject() {          
        this.transform.GetComponent<NPCMove>().enabled = false;
        this.transform.GetComponent<NPCAttack>().enabled = false;        
        this.transform.GetComponent<ObjectShake>().enabled = true;
        this.transform.GetComponent<NPCMove>().RemoveSelectableTiles(); 
        yield return new WaitForSeconds(1f);
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerUnit in playerUnits) {
            playerUnit.transform.GetComponent<PlayerMove>().attacking = false;
        }         
        Instantiate(explosion, this.transform.position, Quaternion.Euler(45, -45, 0));
        this.transform.gameObject.SetActive(false);
        this.transform.tag = "Dead";    
        this.transform.GetComponent<ObjectShake>().enabled = false;    
    }
}
