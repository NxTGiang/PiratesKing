using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int countCoins = 0;
    [SerializeField] private Text coinsText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            countCoins+=2;
            Debug.Log("Coin");
            coinsText.text = countCoins.ToString();
        }
        if (collision.gameObject.CompareTag("Emerald"))
        {
            Destroy(collision.gameObject);
            countCoins+=5;
            Debug.Log("Emerald");
            coinsText.text = countCoins.ToString();
        }
    }
}
