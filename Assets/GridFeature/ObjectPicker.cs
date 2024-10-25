using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    public Camera playerCamera; // Reference to the player's camera.
    public Transform rayTransform;
    public LayerMask groundLayer; // Layer mask for ground detection.
    public float maxPickUpDistance = 5f; // Maximum distance to pick up objects.
    public GameObject placeButton; // Reference to the place button in the UI.
    private GameObject pickedObject = null;
    private Renderer pickedObjectRenderer;
    private Color originalColor;
    private Ray pickRay;
    private bool isRaycastActive = false;
    void Update()
    {
        // Handle object picking.
        if (Input.GetMouseButtonDown(0) && pickedObject == null)
        {
            //pickRay = playerCamera.ScreenPointToRay(Input.mousePosition);

            isRaycastActive = true;
            if (Physics.Raycast(rayTransform.position, transform.forward, out RaycastHit hit, maxPickUpDistance))
            {
                if (hit.collider.CompareTag("Pickable")) // Make sure the object has the "Pickable" tag.
                {
                    pickedObject = hit.collider.gameObject;
                    pickedObjectRenderer = pickedObject.GetComponent<Renderer>();
                    originalColor = pickedObjectRenderer.material.color;
                }
            }
        }

        // Move the picked object along with the camera.
        if (pickedObject != null)
        {
            pickRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            isRaycastActive = true;
            if (Physics.Raycast(rayTransform.position, transform.forward, out RaycastHit hit, maxPickUpDistance, groundLayer))
            {
                pickedObject.transform.position = hit.point;

                // Check if the object can be placed.
                if (IsPlaceable(hit.point))
                {
                    pickedObjectRenderer.material.color = Color.green;
                }
                else
                {
                    pickedObjectRenderer.material.color = Color.red;
                }
            }
        }

        // Place the object by pressing the place button.
        if (pickedObject != null && Input.GetMouseButtonDown(1)) // Replace "Place" with the name of your place button.
        {
            if (pickedObjectRenderer.material.color == Color.green)
            {
                pickedObject = null;
                pickedObjectRenderer.material.color = originalColor;
                pickedObjectRenderer = null;
            }
        }
        Debug.DrawRay(rayTransform.position, transform.forward * maxPickUpDistance, Color.red);
    }

    private bool IsPlaceable(Vector3 position)
    {
        // Perform a check to see if the object can be placed at the specified position.
        Collider[] colliders = Physics.OverlapBox(position, pickedObject.transform.localScale / 2, Quaternion.identity, groundLayer);
        return colliders.Length == 0; // Returns true if no colliders are detected at the position.
    }

    private void OnDrawGizmos()
    {
        if (isRaycastActive)
        {
            // Draw a ray to visualize the pick ray.
            Gizmos.color = Color.blue;

        }
    }
}
