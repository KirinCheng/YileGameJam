using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterController : CharacterController
{
    private Action<MonsterController> monsterDie;
    public int dropMoney;

    private void Start()
    {
    }

    private void Update()
    {
        MonsterAi();
        CharacterActions();
    }

    private void MonsterAi()
    {
        if (controllLock)
            return;
        TurnFaceLeft();
        curState = CharacterState.Moving;
        if (transform.position.x <= -6.2f)
        {
            curState = CharacterState.Die;  
            monsterDie(this);
        }
    }

    public void SetAction(Action<MonsterController> dieAction)
    {
        monsterDie = dieAction;
    }
    protected override void DoDie()
    {
        dropMoney = realtimeStatsInfo.dropMoney;
        monsterDie(this);
    }
}
