using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;



public class PlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
{
    public float speed = 5;
    public int health = 5;
    public Sprite remoteSprite;
    public int lookSpeed = 20;

//--------------
    private float updateRotationAmount = 0;
    private float speedAmount = 0;
  //  private MeshRenderer meshRenderer;
    private Vector3 updatedPosition;
    private Vector3 direction;

    private Vector3 revicedPos;
    private Quaternion rotation;

    void Start()
    {
        updatedPosition = transform.position;
       
        if (!photonView.IsMine)
        {
            SpriteRenderer s = GetComponent<SpriteRenderer>();
            s.sprite = remoteSprite;
            this.transform.localScale = new Vector3(0.1f, 0.1f);
            transform.position = new Vector3(transform.position.x + 2, transform.position.y, -2);
        }

    }

    void GetInputs()
    {
        speedAmount = 0;
        updateRotationAmount = 0;
        if (Input.GetKey(KeyCode.W))
        {
            speedAmount = speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            speedAmount = -speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            updateRotationAmount += -lookSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            updateRotationAmount += lookSpeed * Time.deltaTime;
        }
     
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            GetInputs();
            transform.Rotate(0, 0, updateRotationAmount);
            Vector3 dir = transform.TransformDirection(Vector3.right); // does two things: rotates forward and then translates to world space
            dir *= speedAmount;
            transform.position += dir;
        } else
        {
            transform.position = Vector3.Lerp(revicedPos, transform.position, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(rotation, transform.rotation, Time.deltaTime);
        }
    
    }

    public void restoreHealth(int value)
    {
        this.health += value;
    }

    #region PUN Callbacks

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.transform.position);
            stream.SendNext(this.transform.rotation);
            Debug.Log("Sent");
            //stream.SendNext(direction);
            //stream.SendNext(updateRotationAmount);
            //stream.SendNext(speedAmount);
        }
        else
        {
            //this.transform.position = (Vector3)stream.ReceiveNext();
            //this.transform.rotation = (Quaternion)stream.ReceiveNext();


            //updatedPosition = (Vector3)stream.ReceiveNext();
            Debug.Log("Rcvd");
            //direction = (Vector3)stream.ReceiveNext();
            //updateRotationAmount = (float) stream.ReceiveNext();
            //speedAmount = (float)stream.ReceiveNext();
            //Vector3 temp = (Vector3)stream.ReceiveNext();
            //Debug.Log("Received" + updatedPosition.ToString());
            //float x = Mathf.Lerp(temp.x, this.transform.position.x, 0.2f);
            //float y = Mathf.Lerp(temp.y, this.transform.position.y, 0.2f);
            //float z = Mathf.Lerp(temp.z, this.transform.position.z, 0.2f);
            //transform.position = new Vector3(x, y, z);
        }
    }

    #endregion

}