using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    private void Start()
    {

    }

    private void Update()
    {
        InputCheck();
        CharacterActions();
    }
    protected virtual void InputCheck()
    {
        if (controllLock)
            return;

        //======TestCode========
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("Test GetDamged");
            AttackInfo attackInfo = new AttackInfo(10, new Vector2(10, 0), 5f);
            GotAttacked(attackInfo);
            DoDamaged();
            return;
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            Debug.Log("Heavy MachineGun");
            curWeaponInfo.isBroken = false;
            curWeaponInfo.attack = 1000;
            curWeaponDurability = 1000;
            curWeaponInfo.backOffPower = 100;
            curWeaponInfo.injureDurabilityPerAttack = 0;
            return;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            curState = CharacterState.Defencing;
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            curState = CharacterState.Attaking;
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            TurnFaceRight();
            curState = CharacterState.Moving;
            return;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            TurnFaceLeft();
            curState = CharacterState.Moving;
            return;
        }
        curState = CharacterState.Idle;
    }

    public void ResetPlayer()
    {
        controllLock = false;
        realtimeStatsInfo = characterStatsInfo;
        curHP = realtimeStatsInfo.hp;
        curWeaponInfo = new WeaponInfo();
        curWeaponDurability = 0;
        curState = CharacterState.Idle;
        TurnFaceRight();
        gotAttackTarget = false;
        attackersInfo = new AttackInfo(0,transform.position,0);
    }
}
