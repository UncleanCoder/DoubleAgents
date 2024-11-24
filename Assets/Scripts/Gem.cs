using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Got gem");
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.GameWon();
        }
    }
}
