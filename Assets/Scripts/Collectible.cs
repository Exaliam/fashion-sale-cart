using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int coinValue;

    float rotationSpeed = 10f;
    GameObject parent; //settare come parent il ground che spawnerà questo oggetto

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
