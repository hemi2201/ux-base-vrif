using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class KnifeHit : MonoBehaviour
{
    private bool hasHit;
    private Grabbable theGrab;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        var theKnife = transform.parent;
        theGrab = theKnife.GetComponent<Grabbable>();
        rb = theKnife.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasHit && !theGrab.BeingHeld) ShootRaycast();
        if (hasHit && !theGrab.BeingHeld)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
        if (hasHit && theGrab.BeingHeld)
        {
            hasHit = false;
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }

    private void ShootRaycast()
    {
        hasHit = Physics.Raycast(transform.position, transform.up, out RaycastHit hit, 0.2f);
    }
}
