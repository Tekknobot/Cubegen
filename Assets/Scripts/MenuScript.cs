// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;

// public class MenuScript 
// {
//     [MenuItem("Tools/Assign Tile Material")]
//     public static void AssignTileMaterial()
//     {
//         GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
//         Material material = Resources.Load<Material>("Tile_default");

//         foreach (GameObject t in tiles)
//         {
//             t.GetComponent<Renderer>().material = material;
//         }
//     }

//     [MenuItem("Tools/Assign Tile Script")]
//     public static void AssignTileScript()
//     {
//         GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

//         foreach (GameObject t in tiles)
//         {
//             t.AddComponent<Tile>();
//         }
//     }

//     [MenuItem("Tools/Assign TextureMap to Cube")]
//     public static void AssignTextureMapScript()
//     {
//         GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        
//         foreach (GameObject t in tiles) {
//             Mesh mesh = t.GetComponent<MeshFilter>().sharedMesh;
//             Vector2[] UVs = new Vector2[mesh.vertices.Length];
//             // Front
//             UVs[0] = new Vector2(0.0f, 0.0f);
//             UVs[1] = new Vector2(0.333f, 0.0f);
//             UVs[2] = new Vector2(0.0f, 0.333f);
//             UVs[3] = new Vector2(0.333f, 0.333f);
//             // Top
//             UVs[4] = new Vector2(0.334f, 0.333f);
//             UVs[5] = new Vector2(0.666f, 0.333f);
//             UVs[8] = new Vector2(0.334f, 0.0f);
//             UVs[9] = new Vector2(0.666f, 0.0f);
//             // Back
//             UVs[6] = new Vector2(1.0f, 0.0f);
//             UVs[7] = new Vector2(0.667f, 0.0f);
//             UVs[10] = new Vector2(1.0f, 0.333f);
//             UVs[11] = new Vector2(0.667f, 0.333f);
//             // Bottom
//             UVs[12] = new Vector2(0.0f, 0.334f);
//             UVs[13] = new Vector2(0.0f, 0.666f);
//             UVs[14] = new Vector2(0.333f, 0.666f);
//             UVs[15] = new Vector2(0.333f, 0.334f);
//             // Left
//             UVs[16] = new Vector2(0.334f, 0.334f);
//             UVs[17] = new Vector2(0.334f, 0.666f);
//             UVs[18] = new Vector2(0.666f, 0.666f);
//             UVs[19] = new Vector2(0.666f, 0.334f);
//             // Right        
//             UVs[20] = new Vector2(0.667f, 0.334f);
//             UVs[21] = new Vector2(0.667f, 0.666f);
//             UVs[22] = new Vector2(1.0f, 0.666f);
//             UVs[23] = new Vector2(1.0f, 0.334f);
//             mesh.uv = UVs;
//         }
//     }
// }
