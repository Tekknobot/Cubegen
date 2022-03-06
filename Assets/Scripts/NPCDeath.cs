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
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; 
        Tile t = this.gameObject.transform.GetComponent<TacticsMove>().GetTargetTile(this.gameObject); 
        this.gameObject.transform.position = new Vector3(t.transform.position.x, 1, t.transform.position.z);     
        yield return new WaitForSeconds(1);
        Instantiate(explosion, this.transform.position, Quaternion.Euler(45, -45, 0));
        this.gameObject.GetComponent<NPCMove>().enabled = false;
        this.gameObject.GetComponent<NPCAttack>().enabled = false;
        this.gameObject.SetActive(false);
        this.gameObject.tag = "Dead";        
    }
}
