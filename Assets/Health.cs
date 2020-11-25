using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

/// <summary>
/// Basic health of the player
/// maybe it's better to create a base class for such things
/// </summary>
public class Health : MonoBehaviourPunCallbacks, IPunObservable
{
    /// <summary>
    /// Base health of the player
    /// </summary>
    private float health = 100;

    /// <summary>
    /// Message used to indicate that local player is actually dead
    /// used by GameManagers and other stuffs
    /// </summary>
    public Action localPlayerDead; 

    /// <summary>
    /// Applies damage only on local photonView
    /// another players gets his health from network
    /// serialization
    /// </summary>
    /// <param name="damage"></param>
    public void GetDamage(float damage)
    {
        if (photonView.IsMine)
        {
            health -= damage;

            if (health < 0.001f)
            {
                Dead();
            }
        }
    }

    /// <summary>
    /// Used to indicate that player is actually dead
    /// manage this state in GameManager
    /// </summary>
    public void Dead()
    {
        localPlayerDead?.Invoke();
    }

    /// <summary>
    /// Used to up your hp
    /// </summary>
    /// <param name="value"></param>
    public void IncreaseHealth(float value)
    {
        health += value;
    }

    /// <summary>
    /// Don't know is it actually works
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (float)stream.ReceiveNext();
        }
    }
}
