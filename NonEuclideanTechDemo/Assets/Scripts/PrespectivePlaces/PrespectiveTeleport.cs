using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrespectiveTeleport : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] Transform refferencePlane;
    [SerializeField] Transform targetTeleport;
    [SerializeField] Camera telescopeCamera;

    bool linedUp = false;

}
