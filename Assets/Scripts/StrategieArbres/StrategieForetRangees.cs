//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting.YamlDotNet.Core;
//using UnityEngine;
//using UnityEngine.UIElements;

//public class StrategieForetRangees : StrategiesGenerrerArbres
//{
//    public override void GenererForet(GameObject prefabArbre, Bounds taille)
//    {
//        float longueur = taille.size.x;
//        float largeur = taille.size.z;

//        for (float x = 0; x < longueur; x++)
//        {
//            for (float z = 0; z < largeur; z++)
//            {
                
//                if (x % 4 == 0 && z % 4== 0)
//                {
//                    Vector3 position = new Vector3(x, 0, z);
//                    GameObject.Instantiate(prefabArbre, position, Quaternion.identity);

//                }
//            }
//        }
//    }
//}
