using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("速度")]
    private float movespeed;
    [SerializeField, Header("回転速")]
    private float rotspeed;
    [SerializeField]
    private GameObject CameraObj;
    [SerializeField]
    private string cameraTag;

    private CharacterController cCon;
    private Camera mycamera;
    private CameraMove CM;

    private Vector3 vecInput;
    private Vector3 velocity;


    // Use this for initialization
    void Start()
    {
        cCon = GetComponent<CharacterController>();
        mycamera = CameraObj.GetComponent<Camera>();
        CM = CameraObj.GetComponent<CameraMove>();
        CM.RotSpeed = rotspeed;
        CM.PlayerObj = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector3(0f, Input.GetAxis("Horizontal2"), 0f);
        transform.Rotate(velocity * rotspeed);

        vecInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        velocity = Quaternion.Euler(0f, transform.localEulerAngles.y, 0f) * vecInput;
        velocity *= movespeed;
        cCon.Move(velocity * Time.deltaTime);


    }
}
