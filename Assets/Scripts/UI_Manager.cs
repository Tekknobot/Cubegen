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

    AudioSource audioData;
    public AudioClip[] clip;       

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (tacticsCamera.GetComponent<TacticsCamera>().target) {
            if (tacticsCamera.GetComponent<TacticsCamera>().target.transform.tag == "Player") {
                portrait.GetComponent<Image>().sprite = tacticsCamera.GetComponent<TacticsCamera>().target.GetComponent<SpriteRenderer>().sprite; 
                unit_label.GetComponent<Text>().text = tacticsCamera.GetComponent<TacticsCamera>().target.GetComponent<PlayerAttack>().unitName;
                healthbar_hp.text = tacticsCamera.GetComponent<TacticsCamera>().target.GetComponentInChildren<HealthBarHandler>().GetHealthBarValue() * tacticsCamera.GetComponent<TacticsCamera>().target.GetComponent<PlayerAttack>().maxHP + " HP";
                healthbar_slider.value = tacticsCamera.GetComponent<TacticsCamera>().target.GetComponent<PlayerAttack>().currentHP;
                ActivateUI();
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

        if (GameObject.Find("Map").GetComponent<TurnManager>().turnTeam.Count > 1) {
            movesLeft.GetComponent<Text>().color = Color.white;
            movesLeft.GetComponent<Text>().text = GameObject.Find("Map").GetComponent<TurnManager>().turnTeam.Count.ToString() + " moves left";       
        }
        else {
            movesLeft.GetComponent<Text>().color = Color.red;
            movesLeft.GetComponent<Text>().text = "last move";
        }
    }

    public void ActivateUI() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "Player") {
                    targetButton.SetActive(true);    
                    healthButton.SetActive(true); 
                    audioData = hit.transform.GetComponent<AudioSource>();
                    audioData.PlayOneShot(hit.transform.GetComponent<PlayerMove>().clip[0], 1);
                }
            }            
        }       
    }
}
