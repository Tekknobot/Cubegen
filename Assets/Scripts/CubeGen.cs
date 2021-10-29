using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGen : MonoBehaviour
{
	public Sprite block;
	public GameObject cell;
    public GameObject grid;
    public GameObject sprite;
	public int xSize, ySize, zSize;
    public GameObject[,] cells;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 offset = cell.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offset.x, offset.y);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateBoard (float xOffset, float yOffset) {
        cells = new GameObject[xSize, ySize];    

        float startX = transform.position.x;    
        float startY = transform.position.y;

        for (int x = 0; x < xSize; x++) {      
            for (int y = 0; y < ySize; y++) {
                GameObject newTile = Instantiate(cell, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), Quaternion.identity);
				cells[x, y] = newTile;

				newTile.transform.parent = transform; 

				Sprite newTileSprite = block;
				newTile.GetComponent<SpriteRenderer>().sprite = newTileSprite; 
			}	
        } 

        transform.rotation = Quaternion.Euler(90,0,0);   
    } 
}
