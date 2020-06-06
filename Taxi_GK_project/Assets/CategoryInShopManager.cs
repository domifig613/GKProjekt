using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryInShopManager : MonoBehaviour
{
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private int price;

    public int Price => price;

    public void RefreshButtons(bool carBought)
    {
        buyButton.SetActive(!carBought);
        equipButton.SetActive(carBought);
    }
}
