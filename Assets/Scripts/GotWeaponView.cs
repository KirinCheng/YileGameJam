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
    private Action callback;

    public void Show(string weaponName, Action _callBack)
    {
        callback = _callBack;
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
