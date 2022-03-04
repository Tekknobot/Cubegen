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
        yield return new WaitForSeconds(2);
        Instantiate(explosion, this.transform.position, Quaternion.Euler(45, -45, 0));
        this.gameObject.GetComponent<PlayerMove>().enabled = false;
        this.gameObject.GetComponent<PlayerAttack>().enabled = false;
        this.gameObject.SetActive(false);
        this.gameObject.tag = "Dead";
    }
}
