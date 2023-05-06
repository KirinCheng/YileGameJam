using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Megami : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject megami;
    [SerializeField]
    private GameObject yuushaGetTheWeapon;
    [SerializeField]
    private Slider payMoneySlider;
    [SerializeField]
    private TMP_Text payMoneyTmp;


    public void SetSlide()
    {
        payMoneySlider.minValue = 0;
        payMoneySlider.maxValue = gameManager.money;
        payMoneySlider.value = 0;
    }
    public void OnSliderValueChange()
    {
        payMoneyTmp.text = payMoneySlider.value.ToString();
    }

    public void PayForWeapon()
    {
        gameManager.PrayOrPayTheMegami((int)payMoneySlider.value);
        megami.SetActive(false);
    }
}
