using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Main Camera").GetComponent<CameraShake>().shakeDuration = 0.1f;
        GameObject.Find("Main Camera").GetComponent<CameraShake>().enabled = true;
        GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
