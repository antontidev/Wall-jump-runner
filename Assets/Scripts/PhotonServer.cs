using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonServer : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Play()
    {
        if (!PhotonNetwork.InRoom)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join room and failed");
        PhotonNetwork.CreateRoom(null, new RoomOptions{ MaxPlayers = 4});
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined random room");

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
