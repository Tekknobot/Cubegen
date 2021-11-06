using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject map;
    public GameObject movesLeft;
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
    public Text healthbar_hp;
    public Slider healthbar_slider;

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
            M1Transform.gameObject.GetComponent<SpriteRenderer>().material = M1Transform.gameObject.GetComponent<PlayerMove>().spriteOutline;
            healthbar_hp.text = M1Transform.gameObject.GetComponent<PlayerMove>().healthPoints.ToString() + " HP";
            healthbar_slider.value = M1Transform.gameObject.GetComponent<PlayerMove>().healthPoints;
        }   
        else {
            M1Transform.gameObject.GetComponent<SpriteRenderer>().material = M1Transform.gameObject.GetComponent<PlayerMove>().spriteDefault;
        }   
        if (tacticsCamera.GetComponent<TacticsCamera>().target == M2Transform) {
            portrait.GetComponent<Image>().sprite = M2; 
            unit_label.GetComponent<Text>().text = "UNIT TWO: PROJECTILE";
            M2Transform.gameObject.GetComponent<SpriteRenderer>().material = M2Transform.gameObject.GetComponent<PlayerMove>().spriteOutline;
            healthbar_hp.text = M2Transform.gameObject.GetComponent<PlayerMove>().healthPoints.ToString() + " HP";
            healthbar_slider.value = M2Transform.gameObject.GetComponent<PlayerMove>().healthPoints;
        } 
        else {
            M2Transform.gameObject.GetComponent<SpriteRenderer>().material = M2Transform.gameObject.GetComponent<PlayerMove>().spriteDefault;
        }
        if (tacticsCamera.GetComponent<TacticsCamera>().target == M3Transform) {
            portrait.GetComponent<Image>().sprite = M3; 
            unit_label.GetComponent<Text>().text = "UNIT THREE: DEFENCE";
            M3Transform.gameObject.GetComponent<SpriteRenderer>().material = M3Transform.gameObject.GetComponent<PlayerMove>().spriteOutline;
            healthbar_hp.text = M3Transform.gameObject.GetComponent<PlayerMove>().healthPoints.ToString() + " HP";
            healthbar_slider.value = M3Transform.gameObject.GetComponent<PlayerMove>().healthPoints;        
        } 
        else {
            M3Transform.gameObject.GetComponent<SpriteRenderer>().material = M3Transform.gameObject.GetComponent<PlayerMove>().spriteDefault;
        }
        if (tacticsCamera.GetComponent<TacticsCamera>().target == M4Transform) {
            portrait.GetComponent<Image>().sprite = M4; 
            unit_label.GetComponent<Text>().text = "UNIT FOUR: PROJECTILE";
            M4Transform.gameObject.GetComponent<SpriteRenderer>().material = M4Transform.gameObject.GetComponent<PlayerMove>().spriteOutline;
            healthbar_hp.text = M4Transform.gameObject.GetComponent<PlayerMove>().healthPoints.ToString() + " HP";
            healthbar_slider.value = M4Transform.gameObject.GetComponent<PlayerMove>().healthPoints;        
        } 
        else {
            M4Transform.gameObject.GetComponent<SpriteRenderer>().material = M4Transform.gameObject.GetComponent<PlayerMove>().spriteDefault;
        }
        if (tacticsCamera.GetComponent<TacticsCamera>().target == M5Transform) {
            portrait.GetComponent<Image>().sprite = M5; 
            unit_label.GetComponent<Text>().text = "UNIT FIVE: MELEE";
            M5Transform.gameObject.GetComponent<SpriteRenderer>().material = M5Transform.gameObject.GetComponent<PlayerMove>().spriteOutline;
            healthbar_hp.text = M5Transform.gameObject.GetComponent<PlayerMove>().healthPoints.ToString() + " HP";
            healthbar_slider.value = M5Transform.gameObject.GetComponent<PlayerMove>().healthPoints;        
        }    
        else {
            M5Transform.gameObject.GetComponent<SpriteRenderer>().material = M5Transform.gameObject.GetComponent<PlayerMove>().spriteDefault;
        } 

        ////

        if (tacticsCamera.GetComponent<TacticsCamera>().target == NPC1Transform) {
            portrait.GetComponent<Image>().sprite = NPC1; 
            unit_label.GetComponent<Text>().text = "ENEMY UNIT ONE: MELEE";
            NPC1Transform.gameObject.GetComponent<SpriteRenderer>().material = NPC1Transform.gameObject.GetComponent<NPCMove>().spriteOutline;
            healthbar_hp.text = NPC1Transform.gameObject.GetComponent<NPCMove>().healthPoints.ToString() + " HP";
            healthbar_slider.value = NPC1Transform.gameObject.GetComponent<NPCMove>().healthPoints;        
        }    
        else {
            NPC1Transform.gameObject.GetComponent<SpriteRenderer>().material = NPC1Transform.gameObject.GetComponent<NPCMove>().spriteDefault;
        }  
        if (tacticsCamera.GetComponent<TacticsCamera>().target == NPC2Transform) {
            portrait.GetComponent<Image>().sprite = NPC2; 
            unit_label.GetComponent<Text>().text = "ENEMY UNIT TWO: PROJECTILE";
            NPC2Transform.gameObject.GetComponent<SpriteRenderer>().material = NPC2Transform.gameObject.GetComponent<NPCMove>().spriteOutline;
            healthbar_hp.text = NPC2Transform.gameObject.GetComponent<NPCMove>().healthPoints.ToString() + " HP";
            healthbar_slider.value = NPC2Transform.gameObject.GetComponent<NPCMove>().healthPoints;         
        } 
        else {
            NPC2Transform.gameObject.GetComponent<SpriteRenderer>().material = NPC2Transform.gameObject.GetComponent<NPCMove>().spriteDefault;
        }
        if (tacticsCamera.GetComponent<TacticsCamera>().target == NPC3Transform) {
            portrait.GetComponent<Image>().sprite = NPC3; 
            unit_label.GetComponent<Text>().text = "ENEMY UNIT THREE: DEFENCE";
            NPC3Transform.gameObject.GetComponent<SpriteRenderer>().material = NPC3Transform.gameObject.GetComponent<NPCMove>().spriteOutline;
            healthbar_hp.text = NPC3Transform.gameObject.GetComponent<NPCMove>().healthPoints.ToString() + " HP";
            healthbar_slider.value = NPC3Transform.gameObject.GetComponent<NPCMove>().healthPoints;         
        } 
        else {
            NPC3Transform.gameObject.GetComponent<SpriteRenderer>().material = NPC3Transform.gameObject.GetComponent<NPCMove>().spriteDefault;
        }
        if (tacticsCamera.GetComponent<TacticsCamera>().target == NPC4Transform) {
            portrait.GetComponent<Image>().sprite = NPC4; 
            unit_label.GetComponent<Text>().text = "ENEMY UNIT FOUR: PROJECTILE";
            NPC4Transform.gameObject.GetComponent<SpriteRenderer>().material = NPC4Transform.gameObject.GetComponent<NPCMove>().spriteOutline;
            healthbar_hp.text = NPC4Transform.gameObject.GetComponent<NPCMove>().healthPoints.ToString() + " HP";
            healthbar_slider.value = NPC4Transform.gameObject.GetComponent<NPCMove>().healthPoints;         
        } 
        else {
            NPC4Transform.gameObject.GetComponent<SpriteRenderer>().material = NPC4Transform.gameObject.GetComponent<NPCMove>().spriteDefault;
        }
        if (tacticsCamera.GetComponent<TacticsCamera>().target == NPC5Transform) {
            portrait.GetComponent<Image>().sprite = NPC5; 
            unit_label.GetComponent<Text>().text = "ENEMY UNIT FIVE: MELEE";
            NPC5Transform.gameObject.GetComponent<SpriteRenderer>().material = NPC5Transform.gameObject.GetComponent<NPCMove>().spriteOutline;
            healthbar_hp.text = NPC5Transform.gameObject.GetComponent<NPCMove>().healthPoints.ToString() + " HP";
            healthbar_slider.value = NPC5Transform.gameObject.GetComponent<NPCMove>().healthPoints; 
        } 
        else {
            NPC5Transform.gameObject.GetComponent<SpriteRenderer>().material = NPC5Transform.gameObject.GetComponent<NPCMove>().spriteDefault;
        } 

        ////

        if (TurnManager.turnTeam.Count > 1) {
            movesLeft.GetComponent<Text>().text = TurnManager.turnTeam.Count.ToString() + " moves left";       
        }
        else {
            movesLeft.GetComponent<Text>().text = "last move";
        }
    }
}
