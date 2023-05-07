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
    private Animator realtimeAnimator;
    [SerializeField]
    private string getWeaponAnimaName;
    [SerializeField]
    private TMP_Text weaponNameTmp;
    private Action callback;

    private void Awake()
    {
        realtimeAnimator.runtimeAnimatorController = animator.runtimeAnimatorController;
    }
    public void Show(string weaponName, Action _callBack)
    {
        callback = _callBack;
        weaponNameTmp.text = weaponName;
        gameObject.SetActive(true);
        realtimeAnimator.runtimeAnimatorController = animator.runtimeAnimatorController;
        realtimeAnimator.Play(getWeaponAnimaName);
        timer.TimerCountDown(realtimeAnimator.GetCurrentAnimatorStateInfo(0).length, () => {
            callback();
            Hide();
        });
    }
    public void Hide()
    {
        realtimeAnimator.runtimeAnimatorController = null;
        gameObject.SetActive(false);
    }
}
