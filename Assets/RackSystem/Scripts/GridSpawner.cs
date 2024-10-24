using UnityEngine;

public class DynamicGridSpawner : MonoBehaviour
{
    public GameObject boxPrefab; // Prefab of the box to be instantiated
    public int rows = 5;         // Number of rows in the grid
    public int columns = 5;      // Number of columns in the grid
    public int height = 5;       // Number of layers in height
    public float additionalSpacing = 0.05f;  // Additional space between each box

    private Vector3 boxSize;
    private float xOffset, yOffset, zOffset;

    void Start()
    {
        CalculateOffsets();
        SpawnGrid();
    }

    [ContextMenu("DO it")]
    void SpawnGrid()
    {
        // Clear existing pockets and boxes before spawning new ones
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        // Loop through rows, columns, and height
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                for (int layer = 0; layer < height; layer++)
                {
                    // Calculate the position with the calculated offsets
                    float xPosition = column * xOffset;
                    float yPosition = layer * yOffset;
                    float zPosition = row * zOffset;

                    // Create a "Pocket" GameObject as a container for the box
                    GameObject pocket = new GameObject($"Pocket_{row}_{column}_{layer}");
                    pocket.transform.SetParent(this.transform);
                    pocket.transform.localScale = Vector3.one;
                    pocket.transform.localPosition = new Vector3(xPosition, yPosition, zPosition);
                    pocket.transform.localRotation = Quaternion.identity;

                    // Instantiate the box inside the pocket and reset its local transform
                    GameObject go = Instantiate(boxPrefab, pocket.transform);
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localRotation = Quaternion.identity;
                }
            }
        }
    }

    void CalculateOffsets()
    {
        if (boxPrefab == null) return;

        // Get the renderer of the prefab to determine the actual size of each box, including scale
        boxSize = boxPrefab.GetComponentInChildren<Renderer>().bounds.size;

        // Calculate offsets with additional spacing
        xOffset = boxSize.x + additionalSpacing;
        yOffset = boxSize.y + additionalSpacing;
        zOffset = boxSize.z + additionalSpacing;
    }

    // Draws gizmos to visualize the grid bounds and labels
    void OnDrawGizmos()
    {
        if (boxPrefab == null) return;

        // Ensure offsets are calculated for accurate gizmo drawing
        CalculateOffsets();
        // Get the prefab name for labeling
        string prefabName = boxPrefab.name;
        // Draw wire cubes and labels for each pocket position
        Gizmos.color = Color.yellow;
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                for (int layer = 0; layer < height; layer++)
                {
                    // Calculate the position with the calculated offsets
                    float xPosition = column * xOffset;
                    float yPosition = (layer * yOffset) + yOffset/2f;
                    float zPosition = row * zOffset;

                    // Draw a wireframe box at each pocket position
                    Vector3 pocketPosition = new Vector3(xPosition, yPosition, zPosition);
                    Gizmos.DrawWireCube(transform.position + pocketPosition, boxSize);                    
                }
            }
        }

        // Draw a label with the prefab name and coordinates
        Gizmos.color = Color.white;
        UnityEditor.Handles.Label(
            transform.localPosition + (Vector3.up * (height * (yOffset + 0.15f))),
            $"{prefabName} {rows} x {columns} x {height}"
        );
    }
}
