using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset;
    public Transform playerCamera;
    private Transform uiTransform;

    // Start is called before the first frame update
    void Start()
    {
        uiTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        uiTransform.position = playerCamera.position + offset;
        transform.LookAt(playerCamera);
    }
}
