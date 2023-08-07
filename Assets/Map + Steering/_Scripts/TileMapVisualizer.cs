using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTileMap;

    [SerializeField]
    private TileBase floorTile, floorTilePrueba, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull,
        wallInnerCornerDownLeft, wallInnerCornerDownRight, 
        wallDiagonalCornerDownLeft, wallDiagonalCornerDownRight,
        wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

    public GameObject boxColliderPrefab;

    public Transform parentMapColliders;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    //public void PaintFloorTiles2(Vector2Int floorPositions)
    //{
    //    PaintSingleTile(floorTilemap, floorTilePrueba, floorPositions);
    //}

    public void CreateTileCollider(Vector2Int position)
    {
        
        Instantiate(boxColliderPrefab, new Vector3(position.x, position.y, 0), Quaternion.identity, parentMapColliders);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tileMap, tile, position);
        }
        
    }

    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if(WallByteTypes.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if (WallByteTypes.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallByteTypes.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }
        else if (WallByteTypes.wallBottm.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallByteTypes.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }

        if (tile!= null)
            PaintSingleTile(wallTileMap, tile, position);

    }

    private void PaintSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePosition, tile);
    }


    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallByteTypes.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        else if (WallByteTypes.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if (WallByteTypes.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallByteTypes.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallByteTypes.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        else if (WallByteTypes.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallByteTypes.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        else if (WallByteTypes.wallBottmEightDirections.Contains(typeAsInt))
        {
            tile = wallBottom;
        }


        if (tile != null)
            PaintSingleTile(wallTileMap, tile, position);

    }



    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
        
        for(int i =0; i<parentMapColliders.childCount; i++)
        {
            GameObject hijo = parentMapColliders.GetChild(i).gameObject;

            Destroy(hijo);
        }
        
    }
}
