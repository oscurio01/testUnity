using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    public int index;
    public Transform gameManager;

    GameManager gameManagerC;

    void Start()
    {
        gameManagerC = gameManager.GetComponent<GameManager>();
    }


    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        gameManagerC.OnTriggerDialog(index);
    }




}
