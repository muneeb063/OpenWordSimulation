using DG.Tweening;
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
    [SerializeField] private PickItemButton itemButton;
    [SerializeField] private Transform pickUpParent;
    [SerializeField] private GameObject inHandItem;
    [SerializeField] private RectTransform dropButton;

    RaycastHit hit;
    GameObject rayHitObject;

    public bool InOnItem { get => isOnItem; }

    private void Start()
    {
        itemButton.GetComponent<Button>().onClick.AddListener(PickUpItem);
        dropButton.GetComponent<Button>().onClick.AddListener(DropInHandItem);
    }
    //// Update is called once per frame
    //void Update()
    //{
    //    isOnItem = Physics.Raycast(transform.position, transform.forward, out hit, range, layerMask);
    //    //Debug.DrawRay(transform.position, transform.forward * range, Color.red);
    //    ItemDetected(isOnItem);
    //    pointer.color = Color.white;
    //    if (isOnItem)
    //    {
    //        rayHitObject = hit.collider.gameObject;
    //        pointer.color = Color.green;
    //        hit.collider.GetComponent<Outline>().enabled = isOnItem;
    //    }
    //    if (rayHitObject != null)
    //        if (rayHitObject.GetComponent<Outline>())
    //            rayHitObject.GetComponent<Outline>().enabled = isOnItem;
    //}

    void Update()
    {
       

        // Perform the raycast
        isOnItem = Physics.Raycast(transform.position, transform.forward, out hit, range, layerMask);
        pointer.color = Color.white;
        // Debug line to visualize the ray
        // Debug.DrawRay(transform.position, transform.forward * range, Color.red);
        if(!InHandItem())
            PickDrop();
    }

    bool InHandItem()
    {
        if (inHandItem != null) return true;
        else return false;
    }
    void PickDrop()
    {
        // Call ItemDetected with the raycast result
        ItemDetected(isOnItem);

        // Reset pointer color to white as the default state
        pointer.color = Color.white;

        // If ray hits something, process the hit object
        if (isOnItem)
        {
            // Get the object hit by the ray
            GameObject currentHitObject = hit.collider.gameObject;

            // Change the pointer color to green to indicate a hit
            pointer.color = Color.green;

            // Enable the Outline component on the hit object (if it has one)
            Outline outlineComponent = currentHitObject.GetComponent<Outline>();
            if (outlineComponent != null)
            {
                outlineComponent.enabled = true;
            }

            // If the hit object is different from the previously hit object, disable the outline on the previous one
            if (rayHitObject != null && rayHitObject != currentHitObject)
            {
                Outline previousOutlineComponent = rayHitObject.GetComponent<Outline>();
                if (previousOutlineComponent != null)
                {
                    previousOutlineComponent.enabled = false;
                }
            }

            // Update the reference to the current hit object
            rayHitObject = currentHitObject;
        }
        else
        {
            // If nothing is hit, disable the outline on the previous hit object
            if (rayHitObject != null)
            {
                Outline previousOutlineComponent = rayHitObject.GetComponent<Outline>();
                if (previousOutlineComponent != null)
                {
                    previousOutlineComponent.enabled = false;
                }

                // Clear the reference to the previously hit object since nothing is hit now
                rayHitObject = null;
            }
        }
    }

    void ItemDetected(bool isItem)
    {
        itemButton.InteractButton(isItem);
    }

    void PickUpItem()
    {
        if(rayHitObject != null)
        {
            rayHitObject.transform.parent = pickUpParent;
            rayHitObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            inHandItem = rayHitObject;
            Outline previousOutlineComponent = rayHitObject.GetComponent<Outline>();
            if (previousOutlineComponent != null)
            {
                previousOutlineComponent.enabled = false;
            }
            Rigidbody rb = inHandItem.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;
            ItemDetected(false);
            ButtonAnim(true);
        }
    }

    void DropInHandItem()
    {
        if (inHandItem != null)
        {
            inHandItem.transform.SetParent(null);
            Rigidbody rb = inHandItem.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = false;

            inHandItem = null;
            ButtonAnim(false);
        }
    }
    [SerializeField] private Vector2 showPos;
    [SerializeField] private Vector2 hidePos;
    void ButtonAnim(bool show)
    {
        if (show)
            dropButton.DOAnchorPos(showPos, .25f);
        else
            dropButton.DOAnchorPos(hidePos, .25f);
    }
}
