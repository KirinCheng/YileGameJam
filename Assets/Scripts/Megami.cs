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
    private GameObject megamiAngryFace;
    [SerializeField]
    private Slider payMoneySlider;
    [SerializeField]
    private TMP_Text payMoneyTmp;


    public void SetSlide(int money)
    {
        payMoneySlider.minValue = 1;
        payMoneySlider.maxValue = money;
        payMoneySlider.value = payMoneySlider.maxValue;
    }
    public void OnSliderValueChange()
    {
        payMoneyTmp.text = payMoneySlider.value.ToString();
        if (gameManager.day <= 1)
            return;
        if (payMoneySlider.value <= 1)
            megamiAngryFace.SetActive(true);
        else
            megamiAngryFace.SetActive(false);
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
