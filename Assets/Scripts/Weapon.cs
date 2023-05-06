using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer weaponSpriteRenderer;
    public void SetSprite(Sprite sprite)
    {
        weaponSpriteRenderer.sprite = sprite;
    }
}
