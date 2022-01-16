using UnityEngine;
using System.Collections;
     
public class TacticsCamera : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    public GameObject[] playerPrefabs;
    public GameObject[] npcPrefabs;

    int playerPrefabsIndex = 0;

    void Start() {
        playerPrefabs = GameObject.FindGameObjectsWithTag("Player");
        npcPrefabs = GameObject.FindGameObjectsWithTag("NPC");             
    }
     
    void Update()
    {
        // Define a target position above and behind the target transform
        if (target != null) {
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, 0));
        
            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);       
        }

        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(1)) {
            target = playerPrefabs[playerPrefabsIndex++].transform;
            if (playerPrefabsIndex >=5) {
                playerPrefabsIndex = 0;
            }
            CameraSelect();
        }
    }

    public void TargetCameraOnPlayer() {
        foreach (GameObject playerPrefab in playerPrefabs) {
            if (playerPrefab.GetComponent<PlayerMove>().moving == true) {
                target = playerPrefab.transform;
            }
        }
    }

    public void TargetCameraOnNPC() {
        foreach (GameObject npcPrefab in npcPrefabs) {
            if (npcPrefab.GetComponent<NPCMove>().moving == true) {
                target = npcPrefab.transform;
            }
        }         
    }

    public void CameraSelect() {
        if (target == playerPrefabs[0].transform) {    
            playerPrefabs[0].GetComponent<TacticsMove>().RemoveSelectableTiles();
            playerPrefabs[0].GetComponent<PlayerMove>().unitTurn = false;
            playerPrefabs[0].GetComponent<TacticsMove>().FindSelectableTiles();
            playerPrefabs[0].GetComponent<TacticsMove>().turn = true;
            playerPrefabs[0].GetComponent<PlayerMove>().unitTurn = true;
            playerPrefabs[0].GetComponent<PlayerMove>().tempGO = playerPrefabs[0].transform.gameObject;
        }     

        if (target == playerPrefabs[1].transform) {    
            playerPrefabs[1].GetComponent<TacticsMove>().RemoveSelectableTiles();
            playerPrefabs[1].GetComponent<PlayerMove>().unitTurn = false;
            playerPrefabs[1].GetComponent<TacticsMove>().FindSelectableTiles();
            playerPrefabs[1].GetComponent<TacticsMove>().turn = true;
            playerPrefabs[1].GetComponent<PlayerMove>().unitTurn = true;
            playerPrefabs[0].GetComponent<PlayerMove>().tempGO = playerPrefabs[1].transform.gameObject;
        }  

        if (target == playerPrefabs[2].transform) {    
            playerPrefabs[2].GetComponent<TacticsMove>().RemoveSelectableTiles();
            playerPrefabs[2].GetComponent<PlayerMove>().unitTurn = false;
            playerPrefabs[2].GetComponent<TacticsMove>().FindSelectableTiles();
            playerPrefabs[2].GetComponent<TacticsMove>().turn = true;
            playerPrefabs[2].GetComponent<PlayerMove>().unitTurn = true;
            playerPrefabs[0].GetComponent<PlayerMove>().tempGO = playerPrefabs[2].transform.gameObject;
        }                  

        if (target == playerPrefabs[3].transform) {    
            playerPrefabs[3].GetComponent<TacticsMove>().RemoveSelectableTiles();
            playerPrefabs[3].GetComponent<PlayerMove>().unitTurn = false;
            playerPrefabs[3].GetComponent<TacticsMove>().FindSelectableTiles();
            playerPrefabs[3].GetComponent<TacticsMove>().turn = true;
            playerPrefabs[3].GetComponent<PlayerMove>().unitTurn = true;
            playerPrefabs[0].GetComponent<PlayerMove>().tempGO = playerPrefabs[3].transform.gameObject;
        }  

        if (target == playerPrefabs[4].transform) {    
            playerPrefabs[4].GetComponent<TacticsMove>().RemoveSelectableTiles();
            playerPrefabs[4].GetComponent<PlayerMove>().unitTurn = false;
            playerPrefabs[4].GetComponent<TacticsMove>().FindSelectableTiles();
            playerPrefabs[4].GetComponent<TacticsMove>().turn = true;
            playerPrefabs[4].GetComponent<PlayerMove>().unitTurn = true;
            playerPrefabs[0].GetComponent<PlayerMove>().tempGO = playerPrefabs[4].transform.gameObject;
        }                        
    }
}