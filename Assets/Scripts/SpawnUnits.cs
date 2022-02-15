using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnits : MonoBehaviour
{
    public GameObject[] unit_spawn_points;   
    public GameObject[] unit_prefabs;
    public GameObject[] player_clones;
    public GameObject[] npc_clones;

    public bool spawned = false;

    int spawn_points_index = 0;
    int spawn_points_index2 = 0;
    int k = 0;

    // Start is called before the first frame update
    void Start()
    {
        unit_spawn_points = GameObject.FindGameObjectsWithTag("Tile");                
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn() {
        yield return new WaitForSeconds(0);
        foreach (GameObject prefab in unit_prefabs) {
            spawn_points_index = Random.Range(0, unit_spawn_points.Length);
            if (spawn_points_index == spawn_points_index2) {
                Debug.Log(spawn_points_index +" "+" "+ spawn_points_index2);
                Instantiate(prefab, unit_spawn_points[spawn_points_index+2].transform);
            }
            else {
                Instantiate(prefab, unit_spawn_points[spawn_points_index].transform);
                prefab.transform.position = new Vector3(prefab.transform.position.x, prefab.transform.position.y, prefab.transform.position.z);
                GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target = prefab.transform;
            }
            spawn_points_index2 = spawn_points_index;
        }
        GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().playerPrefabs = GameObject.FindGameObjectsWithTag("Player");
        GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().npcPrefabs = GameObject.FindGameObjectsWithTag("NPC"); 
        foreach (GameObject prefab in GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().playerPrefabs) {
            GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().playerPrefabs[k].GetComponent<PlayerMove>().Init(); 
            k += 1;  
        }   
        foreach (GameObject prefab in GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().npcPrefabs) {
            GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().npcPrefabs[k].GetComponent<NPCMove>().Init();
            k += 1;  
        }          
    }
}
