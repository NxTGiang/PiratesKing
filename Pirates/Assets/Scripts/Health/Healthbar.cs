using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Hearth : MonoBehaviour
{
    [SerializeField] private Health playerHealthBar;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;


    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = playerHealthBar.currentHealth / playerHealthBar.getStartingHealth();
    }
}
