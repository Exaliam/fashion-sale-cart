using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] Rigidbody currentBody; //assicurarsi che il rigidbody non ruoti e sia bloccato su y e z
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        currentBody = GetComponentInChildren<Rigidbody>();
    }

    void Update()
    {
        if(currentBody == null)
        {
            Debug.LogError("Error 003: missing Player Rigidbody"); 
            return; 
        }

        if(Touchscreen.current.primaryTouch.press.isPressed)
        {
            Move();
        }
        else
        {
            currentBody.isKinematic = false;
        }
    }

    void Move()
    {
        currentBody.isKinematic = true;
        Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
        //Il Player si deve muovere in base alla posizione del dito sullo schermo, ma lo script sotto non è giusto
        //Vector3 worldPos = mainCamera.ScreenToWorldPoint(touchPos);
        //currentBody.position = worldPos;
    }
}
