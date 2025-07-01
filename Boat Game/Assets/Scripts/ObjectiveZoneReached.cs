using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveZoneReached : MonoBehaviour
{
    public static bool triggerEntered = false; //this will be accessed in the player script, to check in update that when the player is stationary, if it is in the trigger zone
    private void Start()
    {
        triggerEntered = false; //ensures the static bool is reset
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            triggerEntered = true; //will be designated true when entered
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") triggerEntered = false; //will be designated false when exited
    }
}
