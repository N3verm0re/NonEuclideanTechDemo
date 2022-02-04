using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

    private bool playerIsOveralapping = false;

    private void Update()
    {
        if (playerIsOveralapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
            Debug.Log($"player dotProduct with {transform.name} is {dotProduct}");

            if (dotProduct < 0f)
            {
                Debug.Log($"Teleporting player from {transform.name} to {receiver.name}");
                player.GetComponent<CharacterController>().enabled = false;

                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = receiver.position + positionOffset;

                player.GetComponent<CharacterController>().enabled = true;
                PortalTextureSetup.TextureManager.SwapPlayerWorld();
                playerIsOveralapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player overlapping with {transform.name}");
            playerIsOveralapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player is no longer overlapping with {transform.name}");
            playerIsOveralapping = false;
        }
    }
}
