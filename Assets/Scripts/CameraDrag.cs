using UnityEngine;
 
public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
 
 
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = Input.mousePosition;
            GameObject.Find("TacticsCamera").GetComponent<TacticsCamera>().target = null;
            return;
        }
 
        if (!Input.GetMouseButton(2)) return;
 
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);
 
        transform.Translate(-move, Space.World);  
    }
 
 
}