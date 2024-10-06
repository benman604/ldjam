using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering.Universal;

public class ShadowCaster2DCreator : MonoBehaviour
{
    public GameObject shadowCasterPrefab; // Assign your BoxShadowCaster prefab here in the Inspector

    public void Start()
    {
        // Ensure the shadow caster prefab is assigned
        if (shadowCasterPrefab == null)
        {
            Debug.LogError("Shadow caster prefab not assigned in the inspector.");
            return;
        }

        // Get the Tilemap component
        var tilemap = GetComponent<Tilemap>();
        if (tilemap == null)
        {
            Debug.LogError("No Tilemap component found on the GameObject.");
            return;
        }

        // Ensure there's a shadow_casters container in the scene
        GameObject shadowCasterContainer = GameObject.Find("shadow_casters");
        if (shadowCasterContainer == null)
        {
            shadowCasterContainer = new GameObject("shadow_casters");
        }

        int i = 0;

        // Loop through all positions within the bounds of the tilemap
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(position) == null)
                continue;

            // Instantiate the shadow caster prefab
            GameObject shadowCaster = Instantiate(shadowCasterPrefab, shadowCasterContainer.transform);

            // Convert grid coordinates to world coordinates using Tilemap.CellToWorld
            Vector3 worldPosition = tilemap.CellToWorld(position);

            // Adjust the position by half the tile size to ensure proper alignment
            Vector3 tileOffset = new Vector3(
				tilemap.tileAnchor.x * tilemap.cellSize.x,
				tilemap.tileAnchor.y * tilemap.cellSize.y,
				tilemap.tileAnchor.z * tilemap.cellSize.z
			);

            // Set the final adjusted position
            shadowCaster.transform.position = worldPosition + tileOffset;

            // Scale the shadow caster to match the tile size
            shadowCaster.transform.localScale = new Vector3(tilemap.cellSize.x, tilemap.cellSize.y, 1);

            // Set a unique name for the shadow caster object
            shadowCaster.name = "shadow_caster_" + i;
            i++;
        }
    }
}
