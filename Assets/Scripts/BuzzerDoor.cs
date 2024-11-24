using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzerDoor : MonoBehaviour
{

    public Sprite doorOpenSprite;
    public Sprite doorCloseSprite;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void OpenDoor()
    {
        spriteRenderer.sprite = doorOpenSprite;
        boxCollider.enabled = false;
    }

    public void CloseDoor()
    {
        spriteRenderer.sprite = doorCloseSprite;
        boxCollider.enabled = true;
    }
}
