using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public float speed = 1.0f;

    GameManager gameManagerC;
    public Transform gameManager;
    
    void Start()
    {
        gameManagerC = gameManager.GetComponent<GameManager>();
    }
    void Update()
    {
        if (gameManagerC.IsShowingDialog()) return;

        transform.position += Vector3.right * speed *  Time.deltaTime;
    }
}
