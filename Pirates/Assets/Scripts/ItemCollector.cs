using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int countCoins = 0;
    private int countHealth = 0;
    private int countSpeed = 0;
    private int countStrenth = 0;
    [SerializeField] private Text coinsText;
    [SerializeField] private Text healthText;
    [SerializeField] private Text speedText;
    [SerializeField] private Text strengthText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            countCoins++;
            coinsText.text = countCoins.ToString();
        }
        if (collision.gameObject.CompareTag("Emerald"))
        {
            Destroy(collision.gameObject);
            countCoins+=5;
            coinsText.text = countCoins.ToString();
        }
        if (collision.gameObject.CompareTag("Diamond"))
        {
            Destroy(collision.gameObject);
            countCoins += 10;
            coinsText.text = countCoins.ToString();
        }
        if (collision.gameObject.CompareTag("Health"))
        {
            Destroy(collision.gameObject);
            countHealth ++;
            healthText.text = countHealth.ToString();
        }
        if (collision.gameObject.CompareTag("Strength"))
        {
            Destroy(collision.gameObject);
            countStrenth++;
            strengthText.text = countStrenth.ToString();
        }
        if (collision.gameObject.CompareTag("Speed"))
        {
            Destroy(collision.gameObject);
            countSpeed++;
            speedText.text = countSpeed.ToString();
        }
    }
    public void showNumberOfItem(int itemID)
    {
        if (itemID == 1)
        {
            countHealth--;
            healthText.text = countHealth.ToString();
        }
        if (itemID == 2)
        {
            countSpeed--;
            speedText.text = countSpeed.ToString();
        }
        if (itemID == 3)
        {
            countStrenth--;
            strengthText.text = countStrenth.ToString();
        }
    }
    public int getNumOfHealth() { return countHealth; }
    public int getNumOfSpeed() { return countSpeed; }
    public int getNumOfStrenth() {  return countStrenth; }
}
