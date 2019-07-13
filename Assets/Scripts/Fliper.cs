using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fliper : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public void FlipX()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    public void FlipY()
    {
        spriteRenderer.flipY = !spriteRenderer.flipY;
    }
}
