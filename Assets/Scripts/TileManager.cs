using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tile hiddenInteractableTile;
    public Dictionary<Vector3, GameObject> dictVectorUse;
    void Start()
    {
        foreach (Vector3Int position in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);
            if (tile != null) { interactableMap.SetTile(position, hiddenInteractableTile); }

        }
        dictVectorUse = new Dictionary<Vector3, GameObject>();
    }

    public bool IsInteractable(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);
        if (tile != null)
        {
            if (tile.name == "Tilled Dirt_5")
            {
                return true;
            }
        }
        return false;
    }

    public Vector3 CordinateTile(Vector3Int position)
    {
        return interactableMap.CellToLocal(position);
    }
}
