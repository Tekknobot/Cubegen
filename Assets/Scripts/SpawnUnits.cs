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

    public GameObject[] secondunit_prefabs;

    public GameObject[] playerPrefabs;
    public GameObject[] npcPrefabs;

    public int npcDead;
    
    public int[] spawn_points_array;

    public bool spawned = false;

    int spawn_points_index = 0;
    int j = 0;
    int k = 0;      

    public AudioSource audioData;
    public AudioClip[] clip;

    public bool flag;

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
            SceneManager.LoadScene(0);                                    
        }

        if (npcDead == 5 && flag == false) {
            //SceneManager.LoadScene(0);
            flag = true;
        }
    }

    IEnumerator Spawn() {
        StartCoroutine(FadeAudioSource.StartFade(audioData, 16, 1f));

        foreach (GameObject tile in unit_spawn_points) {
            if (tile.transform.localScale.y != 1) {
                list_unit_spawn_points.Remove(tile);
            }
        }

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
            prefab.GetComponent<NPCMove>().FinishTurn();
            j += 1;  
        }    

        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerUnit in playerUnits) {
            playerUnit.transform.parent = null;
            playerUnit.transform.localScale = new Vector3(1, 1, 1);
            playerUnit.GetComponent<PlayerMove>().FindSelectableTiles();
            playerUnit.GetComponent<PlayerMove>().MoveToTile(playerUnit.GetComponent<PlayerMove>().GetTargetTile(playerUnit));
            playerUnit.GetComponent<PlayerMove>().RemoveSelectableTiles();                      
        }
        GameObject[] npcUnits = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npcUnit in npcUnits) {
            npcUnit.transform.parent = null;
            npcUnit.transform.localScale = new Vector3(1, 1, 1);
            npcUnit.GetComponent<NPCMove>().FindSelectableTiles();
            npcUnit.GetComponent<NPCMove>().MoveToTile(npcUnit.GetComponent<NPCMove>().GetTargetTile(npcUnit));
            npcUnit.GetComponent<NPCMove>().RemoveSelectableTiles(); 
        }  
              
        StartCoroutine(EnableCameraScript());
        yield return null;    
    }

    IEnumerator EnableCameraScript() {
        yield return new WaitForSeconds(7);
        GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().enabled = true;
        spawned = true;
    }
}
