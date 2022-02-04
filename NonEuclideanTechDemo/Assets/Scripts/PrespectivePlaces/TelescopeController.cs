using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeController : MonoBehaviour
{
    [Header("Assingables")]
    [SerializeField] Camera telescopeCamera;
    [SerializeField] PlayerController playerController;
    [SerializeField] PrespectiveTeleport teleporter;

    [Header("Settings")]
    [SerializeField] float sensitivity = 300f;
    float xRotation;

    [Header("Activity Status")]
    public bool isActive;

    private void Update()
    {
        if (isActive)
        {
            telescopeCamera.enabled = true;
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            mouseX = Mathf.Clamp(mouseX, -30f, 30f);
            this.transform.RotateAround(this.GetComponent<Interactable>().cameraPivot.position, Vector3.up, mouseX);
        }
        else
        {
            telescopeCamera.enabled = false;
        }
    }
}
