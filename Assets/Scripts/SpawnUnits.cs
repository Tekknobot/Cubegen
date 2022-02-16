using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnUnits : MonoBehaviour
{
    public GameObject[] unit_spawn_points;   
    public GameObject[] unit_prefabs;
    public GameObject[] player_clones;
    public GameObject[] npc_clones;
    public List<GameObject> list_unit_spawn_points;     

    public int[] spawn_points_array;

    public bool spawned = false;

    int spawn_points_index = 0;
    int spawn_points_index2 = 0;
    int j = 0;
    int k = 0;

    // Start is called before the first frame update
    void Start()
    {
        unit_spawn_points = GameObject.FindGameObjectsWithTag("Tile"); 
        foreach (GameObject unit_spawn_point in unit_spawn_points) {
            list_unit_spawn_points.Add(unit_spawn_point);
        }
        StartCoroutine(Spawn());               
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator Spawn() {
        yield return new WaitForSeconds(0f);
        foreach (GameObject prefab in unit_prefabs) {
            spawn_points_index = Random.Range(0, list_unit_spawn_points.Count);
            Instantiate(prefab, list_unit_spawn_points[spawn_points_index].transform);
            list_unit_spawn_points.RemoveAt(spawn_points_index);
        }
        GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().playerPrefabs = GameObject.FindGameObjectsWithTag("Player"); 
        foreach (GameObject prefab in GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().playerPrefabs) {
            GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().playerPrefabs[k].GetComponent<PlayerMove>().Init();  
            k += 1;  
        }   
        GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().npcPrefabs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject prefab in GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().npcPrefabs) {
            GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().npcPrefabs[j].GetComponent<NPCMove>().Init();
            prefab.GetComponent<TacticsMove>().FinishTurn();
            j += 1;  
        }    

        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerUnit in playerUnits) {
            playerUnit.transform.parent = null;
            playerUnit.transform.localScale = new Vector3(1, 1, 1);
        }
        GameObject[] npcUnits = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npcUnit in npcUnits) {
            npcUnit.transform.parent = null;
            npcUnit.transform.localScale = new Vector3(1, 1, 1);
        }  
              
        StartCoroutine(EnableCameraScript());    
    }

    IEnumerator EnableCameraScript() {
        yield return new WaitForSeconds(7);
        GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().enabled = true;
    }
}
