using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerClones : MonoBehaviour
{
    public bool flag = false;
    GameObject target_button;
    GameObject health_button;

    // Start is called before the first frame update
    void Start()
    {
        target_button = GameObject.Find("Target_btn");
        health_button = GameObject.Find("Health_btn");
    }

    // Update is called once per frame
    void Update()
    {                
        if (flag == false) {
            target_button.GetComponent<Button>().onClick.AddListener(() => this.GetComponent<PlayerAttack>().OnTargetButton());
            health_button.GetComponent<Button>().onClick.AddListener(() => this.GetComponent<PlayerAttack>().OnHealthButton());
            flag = true;
        }
    }
}
