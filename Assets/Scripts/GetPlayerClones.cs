using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerClones : MonoBehaviour
{
    public bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {                
        if (flag == false) {
            gameObject.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("M1(Clone)").GetComponent<PlayerAttack>().OnTargetButton());
            gameObject.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("M2(Clone)").GetComponent<PlayerAttack>().OnTargetButton());
            gameObject.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("M3(Clone)").GetComponent<PlayerAttack>().OnTargetButton());
            gameObject.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("M4(Clone)").GetComponent<PlayerAttack>().OnTargetButton());
            gameObject.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("M5(Clone)").GetComponent<PlayerAttack>().OnTargetButton());
            flag = true;
        }
    }
}
