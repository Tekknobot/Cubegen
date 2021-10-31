using UnityEngine;
using System.Collections;
     
public class TacticsCamera : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    public GameObject[] playerPrefabs;
    public GameObject[] npcPrefabs;

    void Start() {
        playerPrefabs = GameObject.FindGameObjectsWithTag("Player");
        npcPrefabs = GameObject.FindGameObjectsWithTag("NPC");             
    }
     
    void Update()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, 0));
     
        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);       
    }

    public void TargetCamera() {
        foreach (GameObject playerPrefab in playerPrefabs) {
            if (playerPrefab.GetComponent<PlayerMove>().turn == true) {
                target = playerPrefab.transform;
            }
        }

        foreach (GameObject npcPrefab in npcPrefabs) {
            if (npcPrefab.GetComponent<PlayerMove>().turn == true) {
                target = npcPrefab.transform;
            }
        }         
    }
}