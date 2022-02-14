using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaterial : MonoBehaviour
{
    public Material liveMat;
    
    public Material[] newMats;    
    public Material defaultMat;
    public GameObject waterTile;

    void Start()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject t in tiles)
        {   
            int calc_dropChance = Random.Range (0, 101);
            if (calc_dropChance >= 55 && calc_dropChance <= 101) {
                Material materialToUse = newMats[0];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            } 
            if (calc_dropChance >= 33 && calc_dropChance <= 55) {
                Material materialToUse = newMats[1];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }
            if (calc_dropChance >= 22 && calc_dropChance <= 33) {
                Material materialToUse = newMats[2];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }
            if (calc_dropChance >= 11 && calc_dropChance <= 22) {
                Material materialToUse = newMats[3];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }
            if (calc_dropChance >= 0 && calc_dropChance <= 11) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                t.transform.position = new Vector3(t.transform.position.x, -0.125f, t.transform.position.z);
                t.transform.localScale = new Vector3(t.transform.localScale.x, 0.75f, t.transform.localScale.z);
                Instantiate(waterTile, new Vector3(t.transform.position.x, 0.26f, t.transform.position.z), Quaternion.Euler(90, 0, 0));
            }                                                                                              
        }
    }
}