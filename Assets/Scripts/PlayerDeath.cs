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
        if (this.GetComponent<PlayerAttack>().currentHP <= 0.5f) {
            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject() {   
        //this.transform.GetComponent<ObjectShake>().enabled = true;
        this.transform.GetComponent<PlayerMove>().enabled = false;
        this.transform.GetComponent<PlayerAttack>().enabled = false;          
        yield return new WaitForSeconds(1);
        Instantiate(explosion, this.transform.position, Quaternion.Euler(45, -45, 0)); 
        this.transform.gameObject.SetActive(false);
        this.transform.tag = "Dead";
        this.transform.GetComponent<ObjectShake>().enabled = false;
    }
}
