using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerB : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var gameObj = other.gameObject;

        if (gameObj.tag == "Player")
        {
            var playerController = gameObj.GetComponent<Health>();

            playerController.Dead();
        }
    }
}
