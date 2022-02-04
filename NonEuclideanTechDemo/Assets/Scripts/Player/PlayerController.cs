using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] CharacterController playerController;
    [SerializeField] Camera playerCamera;

    [Header("Settings")]
    [SerializeField] float speed = 12f;
    [SerializeField] float gravity = -9.81f;
    [HideInInspector] public float carryDistance;

    //Input Values
    float x = 0;
    //float y = 0; //Unused, TODO: Implement Jump Later
    float z = 0;
    bool mouse1 = false;
    bool interact = false;

    //Control Variables
    bool carrying = false;
    bool looking = false;

    //Others
    Transform carryingObject;
    TelescopeController telescope;

    Vector3 velocity;

    private void Start()
    {
        if(playerController == null)
            playerController = this.GetComponent<CharacterController>();
        if (playerCamera == null)
            playerCamera = this.GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (!looking)
            MyInput(); //Run Inputs at start of Update
        else
            TelescopeInputs();

        #region Movement
        Vector3 move = transform.right * x + transform.forward * z;

        playerController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        playerController.Move(velocity * Time.deltaTime);
        #endregion

        #region Interacting
        if (carrying)
        {
            Carry(carryingObject);
        }

        if (interact && !carrying && !looking)
        {
            int screenX = Screen.width / 2;
            int screenY = Screen.height / 2;
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(screenX, screenY));

            if (Physics.Raycast(ray, out var hit))
            {
                Interactable interactable = hit.transform.GetComponent<Interactable>();
                if (interactable == null) return;
                //else if (Vector3.Distance(this.transform.position, interactable.transform.position) > interactable.radius) return; // Unused radius

                switch (interactable.type)
                {
                    case InteractType.PickUp:
                        interactable.beingCarried = true;
                        carrying = true;
                        Carry(interactable.transform);
                        break;
                    case InteractType.Telescope:
                        interact = false;
                        interactable.telescopeController.isActive = true;
                        playerCamera.enabled = false;
                        playerCamera.GetComponent<CameraController>().enabled = false;
                        telescope = interactable.telescopeController;
                        looking = true;
                        break;
                }
            }
        }
        else if (!interact && carrying)
        {
            Drop();
        }
        #endregion

        #region Telescope
        if (mouse1)
        {
            mouse1 = false;
            looking = false;
            telescope.isActive = false;
            playerCamera.enabled = true;
            playerCamera.GetComponent<CameraController>().enabled = true;
            telescope = null;
        }
        #endregion
    }

    void MyInput()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.E))
            interact = !interact;
    }

    void TelescopeInputs()
    {
        mouse1 = Input.GetMouseButtonDown(0);
    }

    void Carry(Transform o)
    {
        carryingObject = o;
        o.transform.position = playerCamera.transform.position + playerCamera.transform.forward * carryDistance;
    }

    public void Drop()
    {
        carryingObject.GetComponent<Interactable>().beingCarried = false;
        carryingObject = null;
        carrying = false;
    }
}
