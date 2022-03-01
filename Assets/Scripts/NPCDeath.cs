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
        yield return new WaitForSeconds(1);
        Instantiate(explosion, this.transform.position, Quaternion.Euler(45, -45, 0));
        this.gameObject.SetActive(false);
        this.gameObject.GetComponent<NPCMove>().enabled = false;
        this.gameObject.GetComponent<NPCAttack>().enabled = false;
        this.gameObject.tag = "Dead";        
    }
}
