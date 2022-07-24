using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] Rigidbody currentBody; //assicurarsi che il rigidbody non ruoti e sia bloccato su y e z
    [SerializeField] float moveVelocity;
    Camera mainCamera;
    bool isDragging;

    void Start()
    {
        mainCamera = Camera.main;
        currentBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(currentBody == null)
        {
            Debug.LogError("Error 003: missing Player Rigidbody"); 
            return; 
        }

        if(!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if(isDragging)
            {
                currentBody.isKinematic = false;
            }

            isDragging = false;
            return;
        }
        else
        {
            isDragging = true;
            Move();
        }
    }

    void Move()
    {
        currentBody.isKinematic = true;
        Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, mainCamera.nearClipPlane));
        float distanceAdjuster = mainCamera.fieldOfView * 2.1f;
        currentBody.position = new Vector3(Mathf.Lerp(transform.position.x, worldPos.x * distanceAdjuster, Time.deltaTime * moveVelocity), currentBody.position.y, currentBody.position.z);
    }
}
