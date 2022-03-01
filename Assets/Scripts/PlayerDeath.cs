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
        if (GetComponent<PlayerAttack>().currentHP <= 0) {
            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject() {
        yield return new WaitForSeconds(1);
        Instantiate(explosion, this.transform.position, Quaternion.Euler(45, -45, 0));
        this.gameObject.SetActive(false);
        this.gameObject.GetComponent<PlayerMove>().enabled = false;
        this.gameObject.GetComponent<PlayerAttack>().enabled = false;
        this.gameObject.tag = "Dead";
    }
}
