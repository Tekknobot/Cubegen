using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaterial : MonoBehaviour
{
    public Material liveMat;
    
    public Material[] newMats;    
    public Material defaultMat;

    void Start()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject t in tiles)
        {   
            int calc_dropChance = Random.Range (0, 101);
            if (calc_dropChance >= 88 && calc_dropChance <= 101) {
                Material materialToUse = newMats[0];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            } 
            if (calc_dropChance >= 55 && calc_dropChance <= 88) {
                Material materialToUse = newMats[1];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }
            if (calc_dropChance >= 33 && calc_dropChance <= 55) {
                Material materialToUse = newMats[2];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }
            if (calc_dropChance >= 22 && calc_dropChance <= 33) {
                Material materialToUse = newMats[3];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }
            if (calc_dropChance >= 11 && calc_dropChance <= 22) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }  
            if (calc_dropChance >= 0 && calc_dropChance <= 11) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }    
            if (calc_dropChance == 0 || calc_dropChance == 101 ) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }                                                                                                   
        }
    }
}