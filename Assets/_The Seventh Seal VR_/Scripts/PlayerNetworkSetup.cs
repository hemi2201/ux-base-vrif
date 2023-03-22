using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    public GameObject LocalXRRigGameobject;


    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            // The Player is local
            LocalXRRigGameobject.SetActive(true);
        }
        else
        {
            // The player is remote
            LocalXRRigGameobject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
