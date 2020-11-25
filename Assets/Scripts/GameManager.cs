using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[Serializable]
public class ColorPlayerPicker
{
	public List<Material> freeColors;

	public Material GetFreeMaterial()
	{
		var index = UnityEngine.Random.Range(0, freeColors.Count - 1);

		var material = freeColors[index];

		freeColors.RemoveAt(index);

		return material;
	}
}

#pragma warning disable 649

/// <summary>
/// Game manager.
/// Connects and watch Photon Status, Instantiate Player
/// Deals with quiting the room and the game
/// Deals with level loading (outside the in room synchronization)
/// </summary>
/// <remarks>
/// I need a base class of GameManager for future multiple mods of games
/// </remarks>
public class GameManager : MonoBehaviourPunCallbacks
{
	public ColorPlayerPicker colorPlayerPicker;

	[SerializeField]
	private ScreenManager screenManager;

	#region Public Fields

	static public GameManager Instance;

	#endregion

	#region Private Fields

	private GameObject instance;

	private PlayerController localPlayerController;

	private Health localPlayerHealth;

	[Tooltip("The prefab to use for representing the player")]
	[SerializeField]
	private GameObject playerPrefab;

	#endregion

	#region MonoBehaviour CallBacks

	/// <summary>
	/// MonoBehaviour method called on GameObject by Unity during initialization phase.
	/// </summary>
	void Start()
	{
		colorPlayerPicker = new ColorPlayerPicker();

		Instance = this;

		Debug.Log("Joined random room");

		screenManager.ShowLoginUI();
	}

	/// <summary>
	/// Invokes after user typed nickname or after it has been fetched from firebase
	/// or google play account
	/// </summary>
	public void SpawnLocalPlayer(string nickname)
    {
		if (playerPrefab == null)
		{ // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.
			Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
		}
		else
		{
			if (PlayerManager.LocalPlayerInstance == null)
			{
				Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

				// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
				var playerInst = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);

				var playerLocal = PhotonNetwork.LocalPlayer.TagObject as GameObject;

				localPlayerController = playerLocal.GetComponent<PlayerController>();
				// This part looks good
				localPlayerHealth = playerLocal.GetComponent<Health>();

				localPlayerHealth.localPlayerDead += LocalPlayerDead;

				// But this is not. Nickname should be in another place
				localPlayerController.SetNickname(nickname);

				PhotonNetwork.LocalPlayer.NickName = nickname;
			}
			else
			{
				Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
			}
		}

		screenManager.ShowGameUI();
	}

    private void OnDisable()
    {
		localPlayerHealth.localPlayerDead -= LocalPlayerDead;
	}

    #endregion

    private void LocalPlayerDead()
    {
		//screenManager.ShowDeadUI();

		ResetHisPosition();
    }

    private void ResetHisPosition()
    {
		localPlayerHealth.gameObject.transform.position = new Vector3(0f, 5f, 0f);
	}

    #region Photon Callbacks

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
    }

    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
	{
		SceneManager.LoadScene("PunBasics-Launcher");
	}

	#endregion

	#region Public Methods

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}

	public void QuitApplication()
	{
		Application.Quit();
	}

	#endregion
}
