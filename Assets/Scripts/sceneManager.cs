using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class sceneManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void OnStartMEssage(PhotonMessageInfo info) {
        Debug.Log("im the giant rat that makes all of the rules");
    }

    public void InstantiatePlayer() {
        //GameObject p = PhotonNetwork.Instantiate("Player", new Vector3(0,0,0));
        PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity, 0);


        if (PhotonNetwork.IsMasterClient) {
            PhotonView view = PhotonView.Get(this);
            view.RPC("OnStartMEssage", RpcTarget.AllBuffered); //no params
        }
    }
}
