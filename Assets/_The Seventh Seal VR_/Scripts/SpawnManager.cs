using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using BNG;

public class SpawnManager : MonoBehaviour
{
    //[SerializeField] GameObject GenericVRPlayerPrefab;

    public Transform[] startPos;
    public Transform XRRig;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;

    [Tooltip("Name of the Player object to spawn. Must be in a /Resources folder.")]
    public string RemotePlayerObjectName = "RemotePlayer";

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            XRRig.position = startPos[0].position;
            spawnPosition = startPos[0].position;
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            spawnRotation = startPos[1].localRotation;

            XRRig.position = startPos[1].position;
            XRRig.localRotation = spawnRotation;
            spawnPosition = startPos[1].position;
        }

        if (PhotonNetwork.IsConnectedAndReady)
        {
            //PhotonNetwork.Instantiate(GenericVRPlayerPrefab.name, spawnPosition, Quaternion.Euler(0.0f, 90.0f, 0f));

            // Network Instantiate the object used to represent our player. This will have a View on it and represent the player         
            GameObject player = PhotonNetwork.Instantiate(RemotePlayerObjectName, spawnPosition, spawnRotation, 0);
            BNG.NetworkPlayer np = player.GetComponent<BNG.NetworkPlayer>();
            if (np)
            {
                np.transform.name = "MyRemotePlayer";
                np.AssignPlayerObjects();
            }


        }
    }

    
}
