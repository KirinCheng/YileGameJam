using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GotWeaponView : MonoBehaviour
{
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string getWeaponAnimaName;
    [SerializeField]
    private float showAnimaDuration;
    [SerializeField]
    private TMP_Text weaponNameTmp;
    [SerializeField]
    private Image weaponImage;
    [SerializeField]
    private Sprite weaponImageLevel1;
    [SerializeField]
    private Sprite weaponImageLevel2;
    [SerializeField]
    private Sprite weaponImageLevel3;
    private Action callback;

    public void Show(string weaponName, int weaponLevel, Action _callBack)
    {
        callback = _callBack;
        switch (weaponLevel)
        {
            case 0:
                weaponImage.sprite = weaponImageLevel1;
                break;
            case 1:
                weaponImage.sprite = weaponImageLevel2;
                break;
            case 2:
                weaponImage.sprite = weaponImageLevel3;
                break;
        }

        weaponNameTmp.text = weaponName;
        gameObject.SetActive(true);
        animator.Play(getWeaponAnimaName);
        timer.TimerCountDown(showAnimaDuration, () => {
            callback();
            Hide();
        });
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
