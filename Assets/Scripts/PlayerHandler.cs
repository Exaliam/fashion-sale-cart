using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] CartHandler cart;
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

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Collectible" || other.tag == "Coin")
        {
            GameObject collectible = other.gameObject;

            switch(other.tag)
            {
            case "Coin":
                CollectCoin(collectible);
                break;
            case "Collectible":
                CollectClothes(collectible);
                break;
            default:
                break;
            }
        }
        else if(other.tag == "Finish")
        {
            GameManager.Instance.UpdateGameState(GameState.victory);
        }
        else if(other.tag == "Enemy")
        {
            //enemy steal clothes
        }
        else if(other.tag == "Rack")
        {
            //hurting racks let you lose the game
        }
        else
        {
            Debug.LogError("Error 004: no tag detected for " + other.name + " object");
        }
    }

    void Move()
    {
        currentBody.isKinematic = true;
        Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, mainCamera.nearClipPlane));
        float distanceAdjuster = mainCamera.fieldOfView;
        currentBody.position = new Vector3(Mathf.Lerp(transform.position.x, worldPos.x * distanceAdjuster, Time.deltaTime * moveVelocity), currentBody.position.y, currentBody.position.z);
    }

    void CollectCoin(GameObject coin)
    {
        int coinValue = coin.GetComponent<Collectible>().coinValue;
        GameManager.Instance.CollectCoin(coinValue);
        Destroy(coin.gameObject);
    }

    void CollectClothes(GameObject clothes)
    {
        int coinValue = clothes.GetComponent<Collectible>().coinValue;
        Renderer rend = clothes.GetComponent<Renderer>();
        Color clothesColor = rend.material.color;
        GameManager.Instance.StackClothes(clothesColor, coinValue);
        cart.GetClothesInCart(clothesColor);
        Destroy(clothes);
    }
}
