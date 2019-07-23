using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fliper : MonoBehaviour
{
    public delegate void FlipsChange(bool isFlip);
    public static event FlipsChange OnFlips;

    public SpriteRenderer spriteRenderer;

    public void FlipX()
    {
        bool isFlip = !spriteRenderer.flipX;
        OnFlips?.Invoke(isFlip);

        spriteRenderer.flipX = isFlip;
    }

    public void FlipY()
    {
        bool isFlip = !spriteRenderer.flipY;
        OnFlips?.Invoke(isFlip);

        spriteRenderer.flipY = isFlip;
    }
}
