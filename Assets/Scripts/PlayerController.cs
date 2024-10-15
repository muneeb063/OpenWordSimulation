using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
            MetaverseEvents.OnSwitchPlayer?.Invoke(true);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
            MetaverseEvents.OnSwitchPlayer?.Invoke(false);
    }
}
