using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using BNG;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private string mapType;

    public TextMeshProUGUI OccupancyRateText_ForHovsHallar;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region UI Callback Methods

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // Add the following for each available map
    public void OnEnterButtonPressed_HovsHallar()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_HOVSHALLAR;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
    }

    #endregion

    #region Photon Callback Methods

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to servers again.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with the name: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("The Local Player: " + PhotonNetwork.NickName+ " joined to " + PhotonNetwork.CurrentRoom.Name + " Player count " + PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY))
        {
            object maptype;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out maptype))
            {
                Debug.Log("Joined the room with the map type: " + (string)maptype);
                if ((string)mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_HOVSHALLAR)
                {
                    // Load Hovs Hallar scene
                    PhotonNetwork.LoadLevel("Hovs Hallar");
                }
                /*
                else if (MultiplayerVRConstants.MAP_TYPE_VALUE_OTHERSCENE)
                {
                    // Load other scene
                    PhotonNetwork.LoadLevel("Other Scene");
                }
                */
            }
        }

        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " Joined to: " + " Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomList.Count == 0)
        {
            // There is no room.
            OccupancyRateText_ForHovsHallar.text = 0 + " / " + 2;

            // For future rooms
            // OccupancyRateText_ForOtherScene
        }

        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room.Name);
            if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_HOVSHALLAR))
            {
                // Update the Outdoor room occupancy field
                Debug.Log("Room is Hovs Hallar map. Player count is: " + room.PlayerCount);
                OccupancyRateText_ForHovsHallar.text = room.PlayerCount + " / " + 2;
            }

            // For future map
            /*
            else if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_OTHERSCENE)
            {
                Debug.Log("Room is Other Scene map. Player count is: " + room.PlayerCount)
                OccupancyRateText_ForOtherScene.text = room.PlayerCount + " / " + 2;
            }
            */
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the lobby.");
    }

    #endregion

    #region Private Methods

    private void CreateAndJoinRoom()
    {
        string randomNoomName = "Room" + mapType + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };
        // We will have several maps
        // 1. Hovs Hallar = "hovshallar"

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };

        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(randomNoomName, roomOptions);
    }

    #endregion
}
