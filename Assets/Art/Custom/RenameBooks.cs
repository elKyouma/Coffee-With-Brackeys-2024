// using UnityEngine;
// using UnityEditor;
// using System.Collections.Generic;

// public class RenameBooks : ScriptableObject
// {
//     [MenuItem("Tools/Rename Cubes Sequentially")]
//     static void RenameCubesSequentially()
//     {
//         // Create a list to hold all GameObjects starting with "Cube."
//         List<GameObject> cubeObjects = new List<GameObject>();

//         // Find all GameObjects in the scene and add those starting with "Cube." to the list
//         foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
//         {
//             if (obj.name.StartsWith("Cube."))
//             {
//                 cubeObjects.Add(obj);
//             }
//         }

//         // Sort cubeObjects by the existing number in their name if needed
//         cubeObjects.Sort((a, b) =>
//         {
//             int aIndex = int.Parse(a.name.Split('.')[1]);
//             int bIndex = int.Parse(b.name.Split('.')[1]);
//             return aIndex.CompareTo(bIndex);
//         });


//         // Rename objects with a sequential index
//         for (int i = 0; i < cubeObjects.Count; i++)
//         {
//             // Rename the object to "Cube.index"
//             cubeObjects[i].name = $"Cube.{i}";
//             Debug.Log($"Renamed to {cubeObjects[i].name}");
//         }
//     }
// }
