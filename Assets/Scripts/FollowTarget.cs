using UnityEngine;
using System.Collections;
     
public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    public GameObject[] tilePrefabs;

    //int tilePrefabsIndex = 0;

    void Start() {
        tilePrefabs = GameObject.FindGameObjectsWithTag("TileTarget");             
    }
     
    void Update()
    {
        // Define a target position above and behind the target transform
        if (target != null) {
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, 0));
        
            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);       
        }

        //target = tilePrefabs[tilePrefabsIndex++].transform;
    }
}