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
    public GameObject buildingsTile1;
    public GameObject buildingsTile2;
    public GameObject miscTile;

    void Start()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject t in tiles)
        {   
            int calc_dropChance = Random.Range (0, 101);
            if (calc_dropChance >= 70 && calc_dropChance <= 101) {
                Material materialToUse = newMats[0];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            } 
            if (calc_dropChance >= 40 && calc_dropChance <= 69) {
                Material materialToUse = newMats[1];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }
            if (calc_dropChance >= 30 && calc_dropChance <= 39) {
                Material materialToUse = newMats[2];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }
            if (calc_dropChance >= 27 && calc_dropChance <= 29) {
                Material materialToUse = newMats[3];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                t.transform.position = new Vector3(t.transform.position.x, 0.125f, t.transform.position.z);
                t.transform.localScale = new Vector3(t.transform.localScale.x, 1.25f, t.transform.localScale.z);
                var miscTileGO = Instantiate(miscTile, new Vector3(t.transform.position.x, 1.25f, t.transform.position.z), Quaternion.Euler(45, -45, 0));               
                miscTileGO.transform.parent = t.transform;
            }
            if (calc_dropChance >= 24 && calc_dropChance <= 26) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                t.transform.position = new Vector3(t.transform.position.x, -0.125f, t.transform.position.z);
                t.transform.localScale = new Vector3(t.transform.localScale.x, 0.75f, t.transform.localScale.z);
                var waterTileGO = Instantiate(waterTile, new Vector3(t.transform.position.x, 0.26f, t.transform.position.z), Quaternion.Euler(90, 0, 0));
                waterTileGO.transform.parent = t.transform;
            }  
            if (calc_dropChance >= 5 && calc_dropChance <= 23) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }   

            //buildings
            if (calc_dropChance >= 4 && calc_dropChance <= 6) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                var buildingTileGO = Instantiate(buildingsTile, new Vector3(t.transform.position.x, 0.9f, t.transform.position.z), Quaternion.Euler(45, -45, 0));
                buildingTileGO.transform.parent = t.transform;
            } 
            if (calc_dropChance >= 2 && calc_dropChance <= 3) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                var buildingTileGO1 = Instantiate(buildingsTile1, new Vector3(t.transform.position.x, 0.9f, t.transform.position.z), Quaternion.Euler(45, -45, 0));
                buildingTileGO1.transform.parent = t.transform;
            }
            if (calc_dropChance >= 0 && calc_dropChance <= 1) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                var buildingTileGO2 = Instantiate(buildingsTile2, new Vector3(t.transform.position.x, 0.9f, t.transform.position.z), Quaternion.Euler(45, -45, 0));
                buildingTileGO2.transform.parent = t.transform;
            }            
        }
    }
}