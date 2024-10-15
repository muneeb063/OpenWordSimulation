using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetector : MonoBehaviour
{
    [SerializeField] float range = 100;
    [SerializeField] LayerMask layerMask;
    [SerializeField] bool isOnItem = false;
    [SerializeField] Image pointer;
    public bool InOnItem { get=>isOnItem;}

    // Update is called once per frame
    void Update()
    {
        isOnItem = Physics.Raycast(transform.position, transform.forward, range, layerMask);

        pointer.color = Color.white;
        if (isOnItem) pointer.color = Color.green;
        
    }
}
