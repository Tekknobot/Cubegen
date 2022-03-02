using UnityEngine;
using System.Collections;
using cakeslice;
     
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
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);       
        }

        if (Input.GetMouseButtonDown(1)) {
            // playerPrefabs[playerPrefabsIndex].GetComponent<PlayerMove>().tempGO = null;
            // SetUnitTurnFalse();
            // playerPrefabs[playerPrefabsIndex].GetComponent<PlayerMove>().unitTurn = true;
            // playerPrefabs = GameObject.FindGameObjectsWithTag("Player");            
            // target = playerPrefabs[playerPrefabsIndex++].transform;            
            // if (playerPrefabsIndex >= playerPrefabs.Length) {
            //     playerPrefabsIndex = 0;
            // }

            // GetComponent<TacticsCamera>().TurnOffAllOutlines();
            // CameraSelect();
        }
    }

    public void TargetCameraOnPlayer() {
        foreach (GameObject playerPrefab in playerPrefabs) {
            if (playerPrefab.GetComponent<PlayerMove>().moving == true) {
                target = playerPrefab.transform;
                playerPrefab.GetComponent<cakeslice.Outline>().enabled = true;
            }
        }
    }

    public void TargetCameraOnNPC() {      
        foreach (GameObject npcPrefab in npcPrefabs) {
            if (npcPrefab.GetComponent<NPCMove>().moving == true) {
                target = npcPrefab.transform;
                npcPrefab.GetComponent<cakeslice.Outline>().enabled = true;
            }
        }         
    }

    public void TurnOffAllOutlines() {
        foreach (GameObject playerPrefab in playerPrefabs) {
            playerPrefab.GetComponent<cakeslice.Outline>().enabled = false;
        }        
        foreach (GameObject npcPrefab in npcPrefabs) {
            npcPrefab.GetComponent<cakeslice.Outline>().enabled = false;
        }         
    }

    public void TurnOffPlayerOutlines() {
        foreach (GameObject playerPrefab in playerPrefabs) {
            playerPrefab.GetComponent<cakeslice.Outline>().enabled = false;
        }        
    }    

    public void TurnOffNPCOutlines() {      
        foreach (GameObject npcPrefab in npcPrefabs) {
            npcPrefab.GetComponent<cakeslice.Outline>().enabled = false;
        }         
    }

    public void CycleUnits() {
        playerPrefabs = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject playerPrefab in playerPrefabs) {
            playerPrefabsIndex++;
            playerPrefab.GetComponent<PlayerMove>().tempGO = null;            
            target = playerPrefab.transform; 
            GetComponent<TacticsCamera>().TurnOffAllOutlines();
            CameraSelect();         
        }                           
    }       

    public void CameraSelect() {
        //StartCoroutine(DeactivateUI());

        TurnOffAllOutlines();
        
        target = playerPrefabs[playerPrefabsIndex].transform;
        playerPrefabs[playerPrefabsIndex].GetComponent<TacticsMove>().RemoveSelectableTiles();
        playerPrefabs[playerPrefabsIndex].GetComponent<TacticsMove>().FindSelectableTiles();
        playerPrefabs[playerPrefabsIndex].GetComponent<TacticsMove>().turn = true;
        playerPrefabs[playerPrefabsIndex].GetComponent<PlayerMove>().tempGO = target.transform.gameObject;
        TurnOffAllOutlines();
        playerPrefabs[playerPrefabsIndex].GetComponent<cakeslice.Outline>().enabled = true;                               
    }

    IEnumerator DeactivateUI() {
        GameObject.Find("UI_Manager").GetComponent<UI_Manager>().targetButton.SetActive(false);    
        GameObject.Find("UI_Manager").GetComponent<UI_Manager>().healthButton.SetActive(false);
        yield return null; 
    }
}