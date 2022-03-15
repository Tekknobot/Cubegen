using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaterial : MonoBehaviour
{
    public Material liveMat;
    
    public Material[] newMats;    
    public Material defaultMat;
    public GameObject waterTile;
    public GameObject complex;
    public GameObject tower;
    public GameObject arena;
    public GameObject antena;
    public GameObject building_3;
    public GameObject building_4;
    public GameObject buidling_5;

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
            if (calc_dropChance >= 8 && calc_dropChance <= 29) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
            }   
            if (calc_dropChance == 7) {
                Material materialToUse = newMats[3];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                t.transform.position = new Vector3(t.transform.position.x, 0.125f, t.transform.position.z);
                t.transform.localScale = new Vector3(t.transform.localScale.x, 1.25f, t.transform.localScale.z);
                var antenaGO = Instantiate(antena, new Vector3(t.transform.position.x, 1.5f, t.transform.position.z), Quaternion.Euler(45, -45, 0));               
                antenaGO.transform.parent = t.transform;
            }
            if (calc_dropChance == 6) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                t.transform.position = new Vector3(t.transform.position.x, -0.125f, t.transform.position.z);
                t.transform.localScale = new Vector3(t.transform.localScale.x, 0.75f, t.transform.localScale.z);
                var waterTileGO = Instantiate(waterTile, new Vector3(t.transform.position.x, 0.26f, t.transform.position.z), Quaternion.Euler(90, 0, 0));
                waterTileGO.transform.parent = t.transform;
            }

            //buildings
            if (calc_dropChance == 5) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                var complexGO = Instantiate(complex, new Vector3(t.transform.position.x, 1f, t.transform.position.z), Quaternion.Euler(45, -45, 0));
                complexGO.transform.parent = t.transform;
            } 
            if (calc_dropChance == 4) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                var towerGO = Instantiate(tower, new Vector3(t.transform.position.x, 1.2f, t.transform.position.z), Quaternion.Euler(45, -45, 0));
                towerGO.transform.parent = t.transform;
            }
            if (calc_dropChance == 3) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                var arenaGO = Instantiate(arena, new Vector3(t.transform.position.x, 1f, t.transform.position.z), Quaternion.Euler(45, -45, 0));
                arenaGO.transform.parent = t.transform;
            }   

            //common buidlings
            if (calc_dropChance == 2) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                var building_3GO = Instantiate(building_3, new Vector3(t.transform.position.x, 1f, t.transform.position.z), Quaternion.Euler(45, -45, 0));
                building_3GO.transform.parent = t.transform;
            } 
            if (calc_dropChance == 1) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                var building_4GO = Instantiate(building_4, new Vector3(t.transform.position.x, 1f, t.transform.position.z), Quaternion.Euler(45, -45, 0));
                building_4GO.transform.parent = t.transform;
            }
            if (calc_dropChance == 0) {
                Material materialToUse = newMats[4];
                t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);
                var building_5GO = Instantiate(buidling_5, new Vector3(t.transform.position.x, 1f, t.transform.position.z), Quaternion.Euler(45, -45, 0));
                building_5GO.transform.parent = t.transform;
            }                      
        }
    }
}