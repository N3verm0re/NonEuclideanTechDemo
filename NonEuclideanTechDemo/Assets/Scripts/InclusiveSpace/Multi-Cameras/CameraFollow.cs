using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerCamera;
    public Transform myPortal;
    public Transform otherPortal;

    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        transform.position = myPortal.position + playerOffsetFromPortal;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(myPortal.rotation, otherPortal.rotation);

        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameDirection = portalRotationalDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameDirection, Vector3.up);
    }
}
