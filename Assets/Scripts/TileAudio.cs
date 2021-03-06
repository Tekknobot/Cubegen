using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAudio : MonoBehaviour
{
    public AudioSource audioData;
    public AudioClip[] clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.transform.tag == "Player" || other.transform.tag == "NPC") && GameObject.Find("Map").GetComponent<SpawnUnits>().spawned == false) {
            audioData = GetComponent<AudioSource>();
            audioData.PlayOneShot(clip[0], 1);
            GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target = other.transform;
        }
    }    
}
