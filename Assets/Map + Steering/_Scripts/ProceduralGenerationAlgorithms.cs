using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithms
{
    #region Random Walk Algorithm
    /// <summary>
    /// SimpleRandomWalk
    /// </summary>
    /// <param name="startPosition">posicion inicial</param>
    /// <param name="walkLength"> numero de pasos que hará el algoritmo antes de pararse</param>
    /// <returns></returns>
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        Vector2Int prevPosition = startPosition;

        for(int i = 0; i < walkLength; i++)
        {
            Vector2Int newPosition = prevPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            prevPosition = newPosition;
        }

        return path;
        
    }

    /// <summary>
    /// Para crear los pasillos, en este caso utilizamos una lista en vez de un diccionario, porque la lista permitirá
    /// recordar la última posición y por tanto hacer ahi el pasillo
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="corridorLength"></param>
    /// <returns>Lista de las posiciones del corridor</returns>
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for(int i =0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }
    #endregion

    #region BinarySpaceAlgorithm

    //BoundsInt: Represents an axis aligned bounding box with all values as integers.
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceTosplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();

        List<BoundsInt> roomsList = new List<BoundsInt>();

        roomsQueue.Enqueue(spaceTosplit);

        while(roomsQueue.Count >0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if(Random.value < 0.5f)
                {
                    if(room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if(room.size.x >= minWidth * 2)
                    {
                        SplitVerticallly(minWidth, roomsQueue, room);
                    }
                    else if(room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                  
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVerticallly(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }

        return roomsList;
    }

    private static void SplitVerticallly(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
                                        new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);

        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
                                        new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    #endregion
}


public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>
    {
            new Vector2Int(0,1), //up
            new Vector2Int(1,0), //right
            new Vector2Int(0,-1), //down
            new Vector2Int(-1,0) //left
    };

    public static List<Vector2Int> diagonalDirectionList = new List<Vector2Int>
    {
            new Vector2Int(1,1), //up-right
            new Vector2Int(1,-1), //right-down
            new Vector2Int(-1,-1), //down-left
            new Vector2Int(-1,1) //left-up
    };

    public static List<Vector2Int> eightDirectionList = new List<Vector2Int>
    {
            new Vector2Int(0,1), //up
            new Vector2Int(1,1), //up-right
            new Vector2Int(1,0), //right
            new Vector2Int(1,-1), //right-down
            new Vector2Int(0,-1), //down
            new Vector2Int(-1,-1), //down-left
            new Vector2Int(-1,0), //left
            new Vector2Int(-1,1) //left-up
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }
}