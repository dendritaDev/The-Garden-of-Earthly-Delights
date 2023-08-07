using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileMapVisualizer tileMapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionList);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionList);

        CreateBasicWall(tileMapVisualizer, basicWallPositions, floorPositions);
        CreateCornerWalls(tileMapVisualizer, cornerWallPositions, floorPositions);

    }

    //Creará los walls que van en las esquinas
    private static void CreateCornerWalls(TileMapVisualizer tileMapVisualizer, HashSet<Vector2Int> cornerWAllPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWAllPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionList)
            {
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tileMapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    private static void CreateBasicWall(TileMapVisualizer tileMapVisualizer, HashSet<Vector2Int> basicWallPositions, 
        HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            //Creamos el binaryType comprobando si hay un floor en todas las direcciones y añadiendole un 1 o un 0 segun haya o no
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tileMapVisualizer.PaintSingleBasicWall(position, neighboursBinaryType);
            
            tileMapVisualizer.CreateTileCollider(position);

        }
    }

    /// <summary>
    /// Con esta funcion comprobamos tile por tile si en las direcciones horizontales y verticales tiene un vecino.
    /// En caso de no tenerlo, es decir de que esa posicion no se encuentre en el diccionario de floor positions,
    /// nos guardaremos esa posicion en un nuevo diccionario que será lo que retornaremos
    /// </summary>
    /// <param name="floorPositions"></param>
    /// <param name="directionList"></param>
    /// <returns>retornamos la posicion de donde debería haber un muro</returns>
    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {

            foreach (var direction in directionList)
            {
                var neighbourPosition = position + direction;

                if(floorPositions.Contains(neighbourPosition) == false)
                {
                    wallPositions.Add(neighbourPosition);
                }

            }
        }
        return wallPositions;

    }
}
