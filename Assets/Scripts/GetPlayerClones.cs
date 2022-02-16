using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerClones : MonoBehaviour
{
    public bool flag = false;
    GameObject target_button;
    // Start is called before the first frame update
    void Start()
    {
        target_button = GameObject.Find("Target_btn");
    }

    // Update is called once per frame
    public void Update()
    {                
        if (flag == false) {
            target_button.GetComponent<Button>().onClick.AddListener(() => this.GetComponent<PlayerAttack>().OnTargetButton());
            flag = true;
        }
    }
}
