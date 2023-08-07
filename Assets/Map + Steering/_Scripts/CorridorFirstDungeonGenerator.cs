using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int corridorLength = 14, corridorCount = 5;
    [SerializeField][Range(0.1f, 1)] private float roomPercent = 0.8f;
    

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnds(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        for(int i = 0; i<corridors.Count; i++)
        {
            //corridors[i] = IncreaseCorridorsSizeByOne(corridors[i]);
            corridors[i] = IncreaseCorridorsSizeBy3(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }

        tileMapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);
    }


    /// <summary>
    /// Función para crear salas en los deadEnds que genera el algoritmo
    /// </summary>
    /// <param name="deadEnds"></param>
    /// <param name="roomFloors"></param>
    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            //Si en la posicion del deadEnd no se ha creado una sala, se crea y se une al diccionario de las roomFloors
            if(roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }

        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;

            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                if(floorPositions.Contains(position + direction))
                    neighboursCount++;
            }

            if(neighboursCount == 1)
                deadEnds.Add(position);

        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent); //de las posibles salas, nos quedamos solo con un % de estas para q se conviertan en salas

        //Con esto lo que hacemos es ordenar de manera random las salas potenciales del diccionario y pasarlo a una lista para poder hacer un foreach
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        //Creamos la sala y la metemos en el diccionario para evitar duplicidades
        foreach (var roomPosition in roomsToCreate) 
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor); 
        }

        return roomPositions;
    }

    /// <summary>
    /// Crea un Corridor y lo une al diccionario que tenemos para que ya no se generen mas tiles en esas posiciones,
    /// puesto que sino estaria duplicandose
    /// </summary>
    /// <param name="floorPositions"></param>
    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        for (int i =0;  i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition); //El final del pasillo lo ponemos como potencial para crear una sala
            floorPositions.UnionWith(corridor);
        }
        return corridors;
    }


    private List<Vector2Int> IncreaseCorridorsSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int previousDirection = Vector2Int.zero;

        for(int i =1; i < corridor.Count; i++)
        {
            Vector2Int directionFromCell = corridor[i] - corridor[i-1];

            if(previousDirection != Vector2Int.zero && directionFromCell != previousDirection)
            {
                for(int x = -1; x < 2; x++)
                {
                    for(int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i-1] + new Vector2Int(x,y));
                    }
                }
                previousDirection = directionFromCell;
            }
            else
            {
                //añadir una celda en la dirección + 90º
                Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);
            }
        }
        return newCorridor;
    }


    private List<Vector2Int> IncreaseCorridorsSizeBy3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for(int i = 1; i < corridor.Count; i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for(int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }

    private Vector2Int GetDirection90From(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
            return Vector2Int.right;

        if (direction == Vector2Int.right)
            return Vector2Int.down;

        if (direction == Vector2Int.down)
            return Vector2Int.left;
        
        if(direction == Vector2Int.left)
            return Vector2Int.up;    

        return Vector2Int.zero;
    }
}
