using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public interface INetworkCamera
{
    void ConnectCamera(Transform player);
    void DisconnectCamera();

}

public class NetworkCameraController : MonoBehaviourPunCallbacks, INetworkCamera
{
    [SerializeField]
    private CinemachineVirtualCamera vCam;

    private int currentPlayer = 0;

    public void ConnectCamera(Transform player)
    {
        vCam.LookAt = vCam.Follow = player;
    }

    public void DisconnectCamera()
    {
        vCam.LookAt = vCam.Follow = null;
    }

    public void NextTargetPlayer()
    {
        var playerList = PhotonNetwork.PlayerListOthers;

        if (playerList.Length > 0)
        {

            if (currentPlayer > playerList.Length - 1)
            {
                currentPlayer = 0;
            }

            var player = playerList[currentPlayer++];

            var targetObj = player.TagObject as GameObject;

            ConnectCamera(targetObj.transform);
        }
    }
}
