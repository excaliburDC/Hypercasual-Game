using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "GameOver")
        {
            CancelInvoke("Landed");
            GamePlayController.instance.RestartGame();
        }
    }

}
