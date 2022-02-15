using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDeath : MonoBehaviour
{
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
        this.gameObject.SetActive(false);
    }
}
