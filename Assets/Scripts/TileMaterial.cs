using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaterial : MonoBehaviour
{
    public Material liveMat;
    
    public Material[] newMats;    
    public Material defaultMat;
    public GameObject waterTile;
    public GameObject buildingsTile;

    void Start()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject t in tiles)
        {   
            int calc_dropChance = Random.Range (0, 101);
            if (calc_dropChance >= 66 && calc_dropChance <= 101) {
                Material materialToUse = newMats[0];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            } 
            if (calc_dropChance >= 44 && calc_dropChance <= 66) {
                Material materialToUse = newMats[1];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }
            if (calc_dropChance >= 33 && calc_dropChance <= 44) {
                Material materialToUse = newMats[2];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }
            if (calc_dropChance >= 27 && calc_dropChance <= 33) {
                Material materialToUse = newMats[3];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                t.transform.position = new Vector3(t.transform.position.x, 0.125f, t.transform.position.z);
                t.transform.localScale = new Vector3(t.transform.localScale.x, 1.25f, t.transform.localScale.z);               
            }
            if (calc_dropChance >= 22 && calc_dropChance <= 27) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                t.transform.position = new Vector3(t.transform.position.x, -0.125f, t.transform.position.z);
                t.transform.localScale = new Vector3(t.transform.localScale.x, 0.75f, t.transform.localScale.z);
                var waterTileGO = Instantiate(waterTile, new Vector3(t.transform.position.x, 0.26f, t.transform.position.z), Quaternion.Euler(90, 0, 0));
                waterTileGO.transform.parent = t.transform;
            }  
            if (calc_dropChance >= 3 && calc_dropChance <= 22) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }   
            if (calc_dropChance >= 0 && calc_dropChance <= 3) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                var buildingTileGO = Instantiate(buildingsTile, new Vector3(t.transform.position.x, 0.875f, t.transform.position.z), Quaternion.Euler(45, -45, 0));
                buildingTileGO.transform.parent = t.transform;
            } 

        }
    }
}