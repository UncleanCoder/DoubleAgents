using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzerDoor : MonoBehaviour
{

    public Sprite doorOpenSprite;
    public Sprite doorCloseSprite;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    public void OpenDoor()
    {
        spriteRenderer.sprite = doorOpenSprite;
        collider.enabled = false;
    }

    public void CloseDoor()
    {
        spriteRenderer.sprite = doorCloseSprite;
        collider.enabled = true;
    }
}
