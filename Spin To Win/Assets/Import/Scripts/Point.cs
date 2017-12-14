using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour {

    public GameObject prefPoint;
    
    
    public void SetPoints()
    {
        Transform tfPoints = transform.Find("Points");

        tfPoints.GetComponent<RadialLayout>().MaxAngle = GameConfig.instance.degreePrize;
        tfPoints.GetComponent<RadialLayout>().StartAngle = GameConfig.instance.degreePrize / 2;
        
        for(int i = 0; i<GameConfig.instance.countPrizes; i++)
        {
            GameObject temp = Instantiate(prefPoint, transform.position, Quaternion.identity, tfPoints);
        }
    }
}
