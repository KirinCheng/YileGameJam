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
    private Slider payMoneySlider;
    [SerializeField]
    private TMP_Text payMoneyTmp;


    public void SetSlide(int money)
    {
        payMoneySlider.minValue = 1;
        payMoneySlider.maxValue = money;
        payMoneySlider.value = payMoneySlider.minValue;
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

    public void Show()
    {
        megami.SetActive(true);
    }
}
