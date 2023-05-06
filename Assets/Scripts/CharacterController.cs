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
        underAttack,
        Die,
    }

    [SerializeField]
    private Timer timer;
    [SerializeField]
    protected CharacterStatsInfo characterStatsInfo;
    [SerializeField]
    protected CapsuleCollider2D collider;
    [SerializeField]
    protected Rigidbody2D rigidbody;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected Weapon weapon;
    [SerializeField]
    protected Transform rootTransform;

    protected bool controllLock;
    protected int curHP;
    protected WeaponInfo curWeaponInfo;
    protected int curWeaponDurability;

    protected CharacterStatsInfo realtimeStatsInfo;
    protected CharacterState curState;
    protected int characterDirect;

    protected bool gotAttackTarget;
    protected AttackInfo attackersInfo;

    [SerializeField]
    private WeaponInfo fakeInfo;

    public LayerMask mask;

    private void Awake()
    {
        realtimeStatsInfo = characterStatsInfo;
        curHP = realtimeStatsInfo.hp;
        GetWeapon(fakeInfo);
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
                break;
        }
    }

    protected virtual void DoIdle()
    {
        animator.Play(realtimeStatsInfo.anim_Idle);
        rigidbody.velocity = Vector2.zero;
    }
    protected virtual void DoMove()
    {
        animator.Play(realtimeStatsInfo.anim_Moving);
        rigidbody.velocity = new Vector2(characterDirect * realtimeStatsInfo.moveSpeed , 0);
    }
    protected virtual void DoAttack()
    {
        if (gotAttackTarget)
            return;
        controllLock = true;
        timer.TimerCountDown(curWeaponInfo.attackDuration, () => {
            gotAttackTarget = false;
            ControllLockRecovery(); });
        animator.Play(realtimeStatsInfo.anim_Attack);
        rigidbody.velocity = Vector2.zero;
        CharacterController enemy;
        var originPos = new Vector2(transform.position.x, transform.position.y + 0.1f);
        var size = new Vector2(0.1f,1);
        var target = Physics2D.BoxCast(originPos, size, 0 , Vector2.right * characterDirect,1, mask);
        if (target)
        {
            Debug.Log("Hit");
            gotAttackTarget = true;
            enemy = target.transform.GetComponent<CharacterController>();
            AttackInfo info = new AttackInfo(curWeaponInfo.attack,transform.position,curWeaponInfo.backOffPower);
            enemy.GotAttacked(info);
        }
    }

    protected virtual void DoDefence()
    {
        controllLock = true;
        timer.TimerCountDown(realtimeStatsInfo.defenceDuration, ControllLockRecovery);
        animator.Play(realtimeStatsInfo.anim_Defence);
        rigidbody.velocity = Vector2.zero;
    }

    public virtual void GotAttacked(AttackInfo enemyAtkInfo)
    {
        curState = CharacterState.underAttack;
        attackersInfo = enemyAtkInfo;
        curHP -= attackersInfo.damageValue;
        if (curHP <= 0)
        {
            DoDie();
            return;
        }
        DoDamaged();
    }
    protected virtual void DoDamaged()
    {
        controllLock = true;
        timer.TimerCountDown(realtimeStatsInfo.damagedRecoveryTime,ControllLockRecovery);
        animator.Play(realtimeStatsInfo.anim_Damaged);
        var enemyPosDifference = (attackersInfo.attackerPos.x - this.transform.position.x);
        if (enemyPosDifference >= 0)
            TurnFaceRight();
        else
            TurnFaceLeft();
        rigidbody.AddForce(new Vector2(attackersInfo.backOffPower * characterDirect * -1,0),ForceMode2D.Impulse);
    }

    protected virtual void DoDie()
    {
        DestroyImmediate(this.gameObject);
    }

    protected virtual void ControllLockRecovery()
    {
        controllLock = false;
        curState = CharacterState.Idle;
    }

    protected virtual void GetWeapon(WeaponInfo info)
    {
        curWeaponInfo = info;
        curWeaponDurability = info.totalDurability;
        weapon.SetSprite(info.weaponSprite);
    }

    #region Sub Methods
    protected void TurnFaceRight()
    {
        characterDirect = 1;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    protected void TurnFaceLeft()
    {
        characterDirect = -1;
        transform.localRotation = Quaternion.Euler(0,180,0);
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
