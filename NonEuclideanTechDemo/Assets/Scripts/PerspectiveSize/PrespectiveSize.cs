using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PrespectiveSize : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] Interactable i;
    [SerializeField] Transform player;
    [SerializeField] Camera playerCamera;

    //Values
    float startingDistance;
    Vector3 startingScale;
    float currentDistance;
    Vector3 currentScale;

    void Start()
    {
        if (i == null)
            i = this.GetComponent<Interactable>();
        if (player == null)
            player = GameObject.Find("Player").transform;
        if (playerCamera == null)
            playerCamera = player.GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (!i.beingCarried)
        {
            //TODO: better, stylized shadows
            //Enbale Shadows
            this.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.On;

            //Save refference values
            startingDistance = Vector3.Distance(this.transform.position, player.position);
            startingScale = this.transform.localScale;
        }
        else
        {
            //Disable Shadows
            this.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;

            //TODO: Fix object clipping in terrain and illsuion breaking when too close to the camera
            //Push Object Away
            int screenX = Screen.width / 2;
            int screenY = Screen.height / 2;
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(screenX, screenY));
            LayerMask terrain = LayerMask.GetMask("Terrain");

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, terrain))
            {
                player.GetComponent<PlayerController>().carryDistance = Vector3.Distance(player.position, hit.point) - i.radius/2 - 0.5f;
            }
            else
            {
                player.GetComponent<PlayerController>().carryDistance = currentDistance;
            }

            //Calculate and Set Scale
            currentDistance = Vector3.Distance(this.transform.position, player.position);
            currentScale = startingScale * (currentDistance / startingDistance);
            this.transform.localScale = currentScale;
            i.radius = transform.localScale.x;
        }
    }
}
