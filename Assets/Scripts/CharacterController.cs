using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public enum CharacterState
    {
        Idle,
        Moving,
        Attaking,
        Defencing,
        underAttack
    }

    [SerializeField]
    protected CharacterStatsInfo characterStatsInfo;
    [SerializeField]
    protected CapsuleCollider2D collider;
    [SerializeField]
    protected Rigidbody2D rigidbody;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected SpriteRenderer characterSpriteRender;
    [SerializeField]
    protected SpriteRenderer weaponSpriteRenderer;

    private Timer timer = new Timer();

    protected bool controllLock;
    protected int curHP;
    protected WeaponInfo curWeaponInfo;
    protected int curWeaponDurability;

    protected CharacterStatsInfo realtimeStatsInfo;
    protected CharacterState curState;
    protected int characterDirect;

    protected AttackInfo attackersInfo;

    private void Awake()
    {
        realtimeStatsInfo = characterStatsInfo;
    }

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
        if (Input.GetKeyDown(KeyCode.D))
        {
            curState = CharacterState.Defencing;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            curState = CharacterState.Attaking;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            TurnFaceRight();
            curState = CharacterState.Moving;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            TurnFaceLeft();
            curState = CharacterState.Moving;
        }
        if (!Input.anyKey)
        {
            curState = CharacterState.Idle;
        }

        //======TestCode========
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("Test GetDamged");
            AttackInfo attackInfo = new AttackInfo(10,new Vector2(10,0),0.1f);
            GotAttacked(attackInfo);
        }

    }
    protected virtual void CharacterActions()
    {
        switch (curState)
        {
            case CharacterState.Idle:
                DoIdle();
                break;
            case CharacterState.Moving:
                DoMove();
                break;
            case CharacterState.Attaking:
                DoAttack();
                break;
            case CharacterState.Defencing:
                DoDefence();
                break;
            case CharacterState.underAttack:
                DoDamaged();
                break;
            default:
                break;
        }
    }

    protected virtual void DoIdle()
    {
        Debug.Log("Do Idle");
        animator.Play(realtimeStatsInfo.anim_Idle);
        rigidbody.velocity = Vector2.zero;
    }
    protected virtual void DoMove()
    {
        Debug.Log("Do Move");
        animator.Play(realtimeStatsInfo.anim_Moving);
        rigidbody.velocity = new Vector2(characterDirect * realtimeStatsInfo.moveSpeed , 0);
    }
    protected virtual void DoAttack()
    {
        Debug.Log("Do Attack");
        animator.Play(realtimeStatsInfo.anim_Attack);
        rigidbody.velocity = Vector2.zero;
    }

    protected virtual void DoDefence()
    {
        Debug.Log("Do Defence");
        animator.Play(realtimeStatsInfo.anim_Defence);
        rigidbody.velocity = Vector2.zero;
    }

    public virtual void GotAttacked(AttackInfo enemyAtkInfo)
    {
        curState = CharacterState.underAttack;
        attackersInfo = enemyAtkInfo;
    }

    protected virtual void DoDamaged()
    {
        Debug.Log("Do Damaged");
        controllLock = true;
        timer.TimerCountDown(realtimeStatsInfo.damagedRecoveryTime,DamagedRecovery);
        animator.Play(realtimeStatsInfo.anim_Damaged);
        var enemyPosDifference = (attackersInfo.attackerPos.x - this.transform.position.x);
        if (enemyPosDifference >= 0)
            TurnFaceRight();
        else
            TurnFaceLeft();
        rigidbody.AddForce(new Vector2(attackersInfo.backOffPower * characterDirect * -1,0),ForceMode2D.Impulse);
        curHP -= attackersInfo.damageValue;
    }

    protected virtual void DamagedRecovery()
    {
        controllLock = false;
        curState = CharacterState.Idle;
    }
    protected virtual void GetWeapon(WeaponInfo info)
    {
        curWeaponInfo = info;
        curWeaponDurability = info.totalDurability;
        weaponSpriteRenderer.sprite = info.weaponSprite;
    }

    #region Sub Methods
    protected void TurnFaceRight()
    {
        characterDirect = 1;
        characterSpriteRender.flipX = false;
        weaponSpriteRenderer.flipX = false;
        var weaponXpos = weaponSpriteRenderer.transform.localPosition.x >= 0 ? weaponSpriteRenderer.transform.localPosition.x : Mathf.Abs(weaponSpriteRenderer.transform.localPosition.x);
        weaponSpriteRenderer.transform.localPosition = new Vector2(weaponXpos, weaponSpriteRenderer.transform.localPosition.y);
    }
    protected void TurnFaceLeft()
    {
        characterDirect = -1;
        characterSpriteRender.flipX = true;
        weaponSpriteRenderer.flipX = true;
        var weaponXpos = weaponSpriteRenderer.transform.localPosition.x < 0? weaponSpriteRenderer.transform.localPosition.x : weaponSpriteRenderer.transform.localPosition.x * -1;
        weaponSpriteRenderer.transform.localPosition = new Vector2(weaponXpos, weaponSpriteRenderer.transform.localPosition.y);
    }

    #endregion
}

public class AttackInfo
{
    public int damageValue;
    public Vector2 attackerPos;
    public float backOffPower;

    public AttackInfo(int damage, Vector2 pos, float backOff)
    {
        damageValue = damage;
        attackerPos = pos;
        backOffPower = backOff;
    }
}
