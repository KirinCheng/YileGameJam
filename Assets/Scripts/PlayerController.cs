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


}
