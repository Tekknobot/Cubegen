using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject map;
    public GameObject tacticsCamera;

    public GameObject movesLeft;
    public GameObject portrait;

    public Transform M1Transform;
    public Transform M2Transform;
    public Transform M3Transform;
    public Transform M4Transform;
    public Transform M5Transform;    

    public Sprite M1;
    public Sprite M2;
    public Sprite M3;
    public Sprite M4;
    public Sprite M5;

    public Transform NPC1Transform;
    public Transform NPC2Transform;
    public Transform NPC3Transform;
    public Transform NPC4Transform;
    public Transform NPC5Transform;    

    public Sprite NPC1;
    public Sprite NPC2;
    public Sprite NPC3;  
    public Sprite NPC4;
    public Sprite NPC5;         

    public Text unit_label;
    public Text healthbar_hp;
    public Slider healthbar_slider;

    public GameObject targetButton;
    public GameObject healthButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // M1Transform = GameObject.Find("M1(Clone)").transform;
        // M2Transform = GameObject.Find("M2(Clone)").transform;
        // M3Transform = GameObject.Find("M3(Clone)").transform;
        // M4Transform = GameObject.Find("M4(Clone)").transform;
        // M5Transform = GameObject.Find("M5(Clone)").transform;

        // NPC1Transform = GameObject.Find("M6(Clone)").transform;
        // NPC2Transform = GameObject.Find("M7(Clone)").transform;
        // NPC3Transform = GameObject.Find("M8(Clone)").transform;
        // NPC4Transform = GameObject.Find("M9(Clone)").transform;
        // NPC5Transform = GameObject.Find("M10(Clone)").transform;

        if (tacticsCamera.GetComponent<TacticsCamera>().target) {
            if (tacticsCamera.GetComponent<TacticsCamera>().target.transform.tag == "Player") {
                portrait.GetComponent<Image>().sprite = tacticsCamera.GetComponent<TacticsCamera>().target.GetComponent<SpriteRenderer>().sprite; 
                unit_label.GetComponent<Text>().text = tacticsCamera.GetComponent<TacticsCamera>().target.GetComponent<PlayerAttack>().unitName;
                healthbar_hp.text = tacticsCamera.GetComponent<TacticsCamera>().target.GetComponent<PlayerAttack>().currentHP.ToString() + " HP";
                healthbar_slider.value = tacticsCamera.GetComponent<TacticsCamera>().target.GetComponent<PlayerAttack>().currentHP;
                StartCoroutine(DeactivateUI());
            }  
            if (tacticsCamera.GetComponent<TacticsCamera>().target.transform.tag == "NPC") {
                portrait.GetComponent<Image>().sprite = tacticsCamera.GetComponent<TacticsCamera>().target.GetComponent<SpriteRenderer>().sprite; 
                unit_label.GetComponent<Text>().text = tacticsCamera.GetComponent<TacticsCamera>().target.GetComponent<NPCAttack>().unitName;
                healthbar_hp.text = tacticsCamera.GetComponent<TacticsCamera>().target.GetComponent<NPCAttack>().currentHP.ToString() + " HP";
                healthbar_slider.value = tacticsCamera.GetComponent<TacticsCamera>().target.GetComponent<NPCAttack>().currentHP;
                targetButton.SetActive(false);    
                healthButton.SetActive(false);
            }                                   
        }   
        // if (tacticsCamera.GetComponent<TacticsCamera>().target == M2Transform) {
        //     portrait.GetComponent<Image>().sprite = M2; 
        //     unit_label.GetComponent<Text>().text = M2Transform.gameObject.GetComponent<TacticsAttack>().unitName;
        //     healthbar_hp.text = M2Transform.gameObject.GetComponent<PlayerAttack>().currentHP.ToString() + " HP";
        //     healthbar_slider.value = M2Transform.gameObject.GetComponent<PlayerAttack>().currentHP;
        //     StartCoroutine(DeactivateUI());            
        // } 
        // if (tacticsCamera.GetComponent<TacticsCamera>().target == M3Transform) {
        //     portrait.GetComponent<Image>().sprite = M3; 
        //     unit_label.GetComponent<Text>().text = M3Transform.gameObject.GetComponent<TacticsAttack>().unitName;
        //     healthbar_hp.text = M3Transform.gameObject.GetComponent<PlayerAttack>().currentHP.ToString() + " HP";
        //     healthbar_slider.value = M3Transform.gameObject.GetComponent<PlayerAttack>().currentHP; 
        //     StartCoroutine(DeactivateUI());                  
        // } 
        // if (tacticsCamera.GetComponent<TacticsCamera>().target == M4Transform) {
        //     portrait.GetComponent<Image>().sprite = M4; 
        //     unit_label.GetComponent<Text>().text = M4Transform.gameObject.GetComponent<TacticsAttack>().unitName;
        //     healthbar_hp.text = M4Transform.gameObject.GetComponent<PlayerAttack>().currentHP.ToString() + " HP";
        //     healthbar_slider.value = M4Transform.gameObject.GetComponent<PlayerAttack>().currentHP;   
        //     StartCoroutine(DeactivateUI());                 
        // }  
        // if (tacticsCamera.GetComponent<TacticsCamera>().target == M5Transform) {
        //     portrait.GetComponent<Image>().sprite = M5; 
        //     unit_label.GetComponent<Text>().text = M5Transform.gameObject.GetComponent<TacticsAttack>().unitName;
        //     healthbar_hp.text = M5Transform.gameObject.GetComponent<PlayerAttack>().currentHP.ToString() + " HP";
        //     healthbar_slider.value = M5Transform.gameObject.GetComponent<PlayerAttack>().currentHP;  
        //     StartCoroutine(DeactivateUI());                 
        // }          

        ////

        // if (tacticsCamera.GetComponent<TacticsCamera>().target == NPC1Transform) {
        //     portrait.GetComponent<Image>().sprite = NPC1; 
        //     unit_label.GetComponent<Text>().text = NPC1Transform.gameObject.GetComponent<TacticsAttack>().unitName;
        //     healthbar_hp.text = NPC1Transform.gameObject.GetComponent<NPCAttack>().currentHP.ToString() + " HP";
        //     healthbar_slider.value = NPC1Transform.gameObject.GetComponent<NPCAttack>().currentHP;    
        //     targetButton.SetActive(false);    
        //     healthButton.SetActive(false);
        // }     
        // if (tacticsCamera.GetComponent<TacticsCamera>().target == NPC2Transform) {
        //     portrait.GetComponent<Image>().sprite = NPC2; 
        //     unit_label.GetComponent<Text>().text = NPC2Transform.gameObject.GetComponent<TacticsAttack>().unitName;
        //     healthbar_hp.text = NPC2Transform.gameObject.GetComponent<NPCAttack>().currentHP.ToString() + " HP";
        //     healthbar_slider.value = NPC2Transform.gameObject.GetComponent<NPCAttack>().currentHP;  
        //     targetButton.SetActive(false);    
        //     healthButton.SetActive(false);                   
        // } 
        // if (tacticsCamera.GetComponent<TacticsCamera>().target == NPC3Transform) {
        //     portrait.GetComponent<Image>().sprite = NPC3; 
        //     unit_label.GetComponent<Text>().text = NPC3Transform.gameObject.GetComponent<TacticsAttack>().unitName;
        //     healthbar_hp.text = NPC3Transform.gameObject.GetComponent<NPCAttack>().currentHP.ToString() + " HP";
        //     healthbar_slider.value = NPC3Transform.gameObject.GetComponent<NPCAttack>().currentHP;    
        //     targetButton.SetActive(false);    
        //     healthButton.SetActive(false);                 
        // } 
        // if (tacticsCamera.GetComponent<TacticsCamera>().target == NPC4Transform) {
        //     portrait.GetComponent<Image>().sprite = NPC4; 
        //     unit_label.GetComponent<Text>().text = NPC4Transform.gameObject.GetComponent<TacticsAttack>().unitName;
        //     healthbar_hp.text = NPC4Transform.gameObject.GetComponent<NPCAttack>().currentHP.ToString() + " HP";
        //     healthbar_slider.value = NPC4Transform.gameObject.GetComponent<NPCAttack>().currentHP;   
        //     targetButton.SetActive(false);    
        //     healthButton.SetActive(false);                  
        // }    
        // if (tacticsCamera.GetComponent<TacticsCamera>().target == NPC5Transform) {
        //     portrait.GetComponent<Image>().sprite = NPC5; 
        //     unit_label.GetComponent<Text>().text = NPC5Transform.gameObject.GetComponent<TacticsAttack>().unitName;
        //     healthbar_hp.text = NPC5Transform.gameObject.GetComponent<NPCAttack>().currentHP.ToString() + " HP";
        //     healthbar_slider.value = NPC5Transform.gameObject.GetComponent<NPCAttack>().currentHP;
        //     targetButton.SetActive(false);    
        //     healthButton.SetActive(false);                     
        // }            

        ////

        if (TurnManager.turnTeam.Count > 1) {
            movesLeft.GetComponent<Text>().color = Color.white;
            movesLeft.GetComponent<Text>().text = TurnManager.turnTeam.Count.ToString() + " moves left";       
        }
        else {
            movesLeft.GetComponent<Text>().color = Color.red;
            movesLeft.GetComponent<Text>().text = "last move";
        }
    }

    IEnumerator DeactivateUI() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "Player") {
                    targetButton.SetActive(true);    
                    healthButton.SetActive(true); 
                }
            }            
        }  
        yield return null;       
    }
}
