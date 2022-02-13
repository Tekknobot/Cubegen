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
            //Find the material in the array
            Material materialToUse = newMats[Random.Range(0, newMats.Length)];

            //Copy the properties to the live material
            t.GetComponent<Renderer>().material.CopyPropertiesFromMaterial(materialToUse);                    
        }
    }
}