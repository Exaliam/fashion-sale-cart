using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartHandler : MonoBehaviour
{
    public Transform stack;
    public List<GameObject> stackedClothes;

    [SerializeField] GameObject clothesPrefab;
    float localHeight = 0.003f;

    public void GetClothesInCart(Color clothesColor)
    {
        GameObject clothes = Instantiate(clothesPrefab, stack.position, Quaternion.identity);
        clothes.GetComponent<Renderer>().material.color = clothesColor;
        stackedClothes.Add(clothes);
        clothes.transform.SetParent(stack);
        clothes.transform.localPosition = new Vector3(clothes.transform.localPosition.x, clothes.transform.localPosition.y, localHeight * (float)stackedClothes.Count);
    }
}
