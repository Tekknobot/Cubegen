using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<PlayerAttack>().currentHP <= 0.1f) {
            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject() {
        this.transform.GetComponent<Rigidbody>().useGravity = true;
        this.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Tile t = this.transform.GetComponent<TacticsMove>().GetTargetTile(this.transform.gameObject); 
        this.transform.position = new Vector3(t.transform.position.x, 1, t.transform.position.z);        
        yield return new WaitForSeconds(2);
        Instantiate(explosion, this.transform.position, Quaternion.Euler(45, -45, 0));
        this.transform.GetComponent<PlayerMove>().enabled = false;
        this.transform.GetComponent<PlayerAttack>().enabled = false;
        this.transform.gameObject.SetActive(false);
        this.transform.tag = "Dead";
    }
}
