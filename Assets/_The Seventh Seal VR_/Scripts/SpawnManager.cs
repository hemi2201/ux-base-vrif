using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using BNG;

public class SpawnManager : MonoBehaviour
{
    //[SerializeField] GameObject GenericVRPlayerPrefab;

    //public Vector3 spawnPosition;

    [Tooltip("Name of the Player object to spawn. Must be in a /Resources folder.")]
    public string RemotePlayerObjectName = "RemotePlayer";

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            //PhotonNetwork.Instantiate(GenericVRPlayerPrefab.name, spawnPosition, Quaternion.Euler(0.0f, 90.0f, 0f));

            // Network Instantiate the object used to represent our player. This will have a View on it and represent the player         
            GameObject player = PhotonNetwork.Instantiate(RemotePlayerObjectName, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            BNG.NetworkPlayer np = player.GetComponent<BNG.NetworkPlayer>();
            if (np)
            {
                np.transform.name = "MyRemotePlayer";
                np.AssignPlayerObjects();
            }


        }
    }

    
}
