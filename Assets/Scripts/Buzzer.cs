using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buzzer : MonoBehaviour
{
    public Sprite buzzerOnSprite;
    public Sprite buzzerOffSprite;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Buzzer Pressed");
            spriteRenderer.sprite = buzzerOnSprite;
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("BuzzerDoor"))
            {
                go.GetComponent<BuzzerDoor>().OpenDoor();
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Buzzer Unpressed");
            spriteRenderer.sprite = buzzerOffSprite;
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("BuzzerDoor"))
            {
                go.GetComponent<BuzzerDoor>().CloseDoor();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Buzzer Pressed");
            spriteRenderer.sprite = buzzerOnSprite;
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("BuzzerDoor"))
            {
                go.GetComponent<BuzzerDoor>().OpenDoor();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Buzzer Unpressed");
            spriteRenderer.sprite = buzzerOffSprite;
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("BuzzerDoor"))
            {
                go.GetComponent<BuzzerDoor>().CloseDoor();
            }
        }
    }
}
