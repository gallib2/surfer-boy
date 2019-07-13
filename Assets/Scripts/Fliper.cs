using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fliper : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipX()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    public void FlipY()
    {
        spriteRenderer.flipY = !spriteRenderer.flipY;
    }
}
