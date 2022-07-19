using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] float rotationSpeed; //da togliere dall'editor appena troviamo la giusta velocità
    GameObject parent; //settare come parent il ground che spawnerà questo oggetto

    void FixedUpdate()
    {
        //se il gioco è in play
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
