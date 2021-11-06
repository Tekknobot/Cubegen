using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject tacticsCamera;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tacticsCamera.GetComponent<TacticsCamera>().target == M1Transform) {
            portrait.GetComponent<Image>().sprite = M1; 
            unit_label.GetComponent<Text>().text = "UNIT ONE: MELEE";
        }      
        if (tacticsCamera.GetComponent<TacticsCamera>().target == M2Transform) {
            portrait.GetComponent<Image>().sprite = M2; 
            unit_label.GetComponent<Text>().text = "UNIT TWO: PROJECTILE";
        } 
        if (tacticsCamera.GetComponent<TacticsCamera>().target == M3Transform) {
            portrait.GetComponent<Image>().sprite = M3; 
            unit_label.GetComponent<Text>().text = "UNIT THREE: DEFENCE";
        } 
        if (tacticsCamera.GetComponent<TacticsCamera>().target == M4Transform) {
            portrait.GetComponent<Image>().sprite = M4; 
            unit_label.GetComponent<Text>().text = "UNIT FOUR: PROJECTILE";
        } 
        if (tacticsCamera.GetComponent<TacticsCamera>().target == M5Transform) {
            portrait.GetComponent<Image>().sprite = M5; 
            unit_label.GetComponent<Text>().text = "UNIT FIVE: MELEE";
        }     

        if (tacticsCamera.GetComponent<TacticsCamera>().target == NPC1Transform) {
            portrait.GetComponent<Image>().sprite = NPC1; 
            unit_label.GetComponent<Text>().text = "ENEMY UNIT ONE: MELEE";
        }      
        if (tacticsCamera.GetComponent<TacticsCamera>().target == NPC2Transform) {
            portrait.GetComponent<Image>().sprite = NPC2; 
            unit_label.GetComponent<Text>().text = "ENEMY UNIT TWO: PROJECTILE";
        } 
        if (tacticsCamera.GetComponent<TacticsCamera>().target == NPC3Transform) {
            portrait.GetComponent<Image>().sprite = NPC3; 
            unit_label.GetComponent<Text>().text = "ENEMY UNIT THREE: DEFENCE";
        } 
        if (tacticsCamera.GetComponent<TacticsCamera>().target == NPC4Transform) {
            portrait.GetComponent<Image>().sprite = NPC4; 
            unit_label.GetComponent<Text>().text = "ENEMY UNIT FOUR: PROJECTILE";
        } 
        if (tacticsCamera.GetComponent<TacticsCamera>().target == NPC5Transform) {
            portrait.GetComponent<Image>().sprite = NPC5; 
            unit_label.GetComponent<Text>().text = "ENEMY UNIT FIVE: MELEE";
        }                 
    }
}
