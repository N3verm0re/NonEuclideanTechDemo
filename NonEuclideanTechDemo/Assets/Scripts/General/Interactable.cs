using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactable Settings")]
    public InteractType type;

    [Header("PickUp Type Settings")]
    public float radius = 2f; //Unused
    public bool beingCarried = false;
    //public bool collidingTerrain = false; remnant code from previous implementation, keeping it in case its useful later
    public Rigidbody rb;

    [Header("Telescope Type Settings")]
    public Transform cameraPivot;
    public TelescopeController telescopeController;

    [Header("PlayerController Refference")]
    [SerializeField] PlayerController player;

    private void Start()
    {
        if(type == InteractType.PickUp)
            rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (type == InteractType.PickUp)
        {
            if (beingCarried)
            {
                rb.isKinematic = true;
                rb.detectCollisions = false;
            }
            else
            {
                rb.isKinematic = false;
                rb.detectCollisions = true;
            }
        }
    }
}

public enum InteractType
{
    PickUp = 0,
    Telescope = 1
}
