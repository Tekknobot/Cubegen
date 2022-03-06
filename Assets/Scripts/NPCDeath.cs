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
        this.transform.GetComponent<Rigidbody>().useGravity = true;
        this.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; 
        Tile t = this.transform.GetComponent<TacticsMove>().GetTargetTile(this.gameObject); 
        this.transform.position = new Vector3(t.transform.position.x, 1, t.transform.position.z);     
        yield return new WaitForSeconds(1);
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerUnit in playerUnits) {
            playerUnit.transform.GetComponent<TacticsMove>().moving = false;
        }        
        Instantiate(explosion, this.transform.position, Quaternion.Euler(45, -45, 0));
        this.gameObject.GetComponent<NPCMove>().enabled = false;
        this.gameObject.GetComponent<NPCAttack>().enabled = false;
        this.gameObject.SetActive(false);
        this.gameObject.tag = "Dead";        
    }
}
