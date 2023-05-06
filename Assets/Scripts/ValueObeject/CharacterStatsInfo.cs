using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterInfoSetting", menuName = "Create Data Asset")]
public class CharacterStatsInfo : ScriptableObject
{
    public Sprite characterSprite;
    public int hp;
    public float moveSpeed;
    public float damagedRecoveryTime;
    public float defenceDuration;

    public string anim_Idle;
    public string anim_Moving;
    public string anim_Attack;
    public string anim_Defence;
    public string anim_Damaged;
}
