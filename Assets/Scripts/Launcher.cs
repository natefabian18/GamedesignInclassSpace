using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//added for photon
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks//MonoBehaviour
{
	string gameVersion = "0.1"; //version
	public byte maxPlayersPerRoom = 2; //players in game
	private bool isConnecting;
	private void Awake()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
		Connect();
	}
	void Start()
	{
	}
	void Update()
	{
	}

	//photon session handling

	public void Disconnect()
	{
		PhotonNetwork.Disconnect();
	}
	public void Connect()
	{

		isConnecting = PhotonNetwork.ConnectUsingSettings();

		if (PhotonNetwork.IsConnected)
		{
			PhotonNetwork.JoinRandomRoom();
		}
		else
		{
			//create room
			PhotonNetwork.ConnectUsingSettings();
			PhotonNetwork.GameVersion = gameVersion;
		}
	}

	public override void OnConnectedToMaster()
	{
		base.OnConnectedToMaster();
		Debug.Log("connected to master");
		if (isConnecting)
		{
			PhotonNetwork.JoinRandomRoom();
			isConnecting = false;
		}
	}

	public override void OnDisconnected(DisconnectCause cause)
	{
		base.OnDisconnected(cause);
		Debug.Log($"disconnected for cause {cause.ToString()}");
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		base.OnJoinRandomFailed(returnCode, message);
		Debug.Log("no room found, so create one");
		PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
	}
	public override void OnLeftRoom()
	{
		base.OnLeftRoom();
		Debug.Log("player left room");
	}

	public override void OnJoinedRoom()
	{
		base.OnJoinedRoom();
		Debug.Log("Player Joined Room");
	}
}
