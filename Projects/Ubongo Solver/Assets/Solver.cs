using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solver : MonoBehaviour
{
    #region Singelton
    public static Solver instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

    }
    #endregion

    public GameObject cube;

    private void Start()
    {
        var level = new Piece3D(new List<Vector3Int> { Vector3Int.zero, Vector3Int.up, Vector3Int.right, new Vector3Int(1, 1, 0), Vector3Int.right * 2, new Vector3Int(2, 1, 0),
                                                       new Vector3Int(0, 0, 1), Vector3Int.up + new Vector3Int(0, 0, 1), Vector3Int.right + new Vector3Int(0, 0, 1),
                                                       new Vector3Int(1, 1, 0) + new Vector3Int(0, 0, 1), Vector3Int.right * 2 + new Vector3Int(0, 0, 1),
                                                       new Vector3Int(2, 1, 0) + new Vector3Int(0, 0, 1)}, Color.red);

        var p0 = new Piece3D(new List<Vector3Int> { Vector3Int.zero, Vector3Int.up, Vector3Int.right, new Vector3Int(1, 1, 0), Vector3Int.right * 2 }, Color.blue);
        var p1 = new Piece3D(new List<Vector3Int> { Vector3Int.zero, Vector3Int.up, Vector3Int.right }, Color.green);
        var p2 = new Piece3D(new List<Vector3Int> { Vector3Int.zero, Vector3Int.up, Vector3Int.right, new Vector3Int(0, 1, -1) }, Color.yellow);

        //for (int i = 0; i < 24; i++)
        //{
        //    p0.CreatePiece(Vector3Int.right * i * 6, i);

        //    p1.CreatePiece(Vector3Int.right * i * 6 + new Vector3Int(0, 0, 1) * 10, i);

        //    p2.CreatePiece(Vector3Int.right * i * 6 + new Vector3Int(0, 0, 1) * 20, i);

        //    level.CreatePiece(Vector3Int.right * i * 6 + new Vector3Int(0, 0, 1) * 30, i);

        //}

        List<Solution> solutions = Solve(new List<Piece3D> { p0, p1, p2 }, level);

        for (int i = 0; i < solutions.Count; i++)
        {
            Debug.Log("Den har en løsning jo - Antall brikker som er brukt er: " + solutions[0].moves.Count);
            solutions[i].Solve().position = Vector3.right * i * 6;
        }

    }

    public Piece3D level;

    public int debugInt = 0; // Remove afterwards

    public List<Solution> Solve(List<Piece3D> allPieces, Piece3D wholeLevel)
    {
        int levelSize = wholeLevel.Cubes(0).Count;

        int sumPieces = 0;
        foreach (Piece3D piece in allPieces)
        {
            sumPieces += piece.Cubes(0).Count;
        }

        List<Solution> allSolutions = new List<Solution>();


        if (levelSize == sumPieces)
        {
            bool test = Deep(allPieces, wholeLevel, new List<Move>());
            Debug.Log(test);
        }

        return allSolutions;

        bool Deep(List<Piece3D> pieces, Piece3D level, List<Move> doneMoves)
        {
            #region In case if it loops to much
            debugInt++;
            if(debugInt > 250000)
            {
                Debug.Log("Broke it because debugint is to big!");
                return false;
            }
            #endregion

            // Tester om alle brikkene passer et sted
            foreach (Piece3D piece in pieces)
            {
                List<Piece3D> newPieces = new List<Piece3D>(pieces);
                newPieces.Remove(piece);

                List<Move> moves = piece.PossibleMoves(level);
                if (moves.Count == 0)
                {
                    // Dette betyr at den forige brikken plassert ikke kan være der
                    return true;
                }
                else if (moves.Count == 1)
                {
                    return NextDepth(moves[0]);
                }
            }

            foreach (Piece3D piece in pieces)
            {
                foreach (Move move in piece.PossibleMoves(level))
                {
                    NextDepth(move);
                }
            }

            return true;

            bool NextDepth(Move move)
            {
                Piece3D newLevel = move.Place(level);
                Debug.Log("Cubes in newLevel: " + newLevel.Cubes(0).Count);

                List<Move> newDoneMoves = new List<Move>(doneMoves)
                {
                    move
                };

                if (newLevel.Cubes(0).Count == 0)
                {
                    var debug = 0;
                    foreach (var dm in newDoneMoves)
                    {
                        debug += dm.piece.Cubes(0).Count;
                    }
                    Debug.Log("Returning solution. Sum of newDoneMoves: " + debug + ", newLevel: " + newLevel.Cubes(0).Count);

                    allSolutions.Add(new Solution(newDoneMoves));

                    return false;
                }

                List<Piece3D> newPieces = new List<Piece3D>(pieces);
                newPieces.Remove(move.piece);

                return Deep(newPieces, newLevel, newDoneMoves);
                
            }

        }

    }
}

public class Solution
{
    public readonly List<Move> moves;

    public Solution(List<Move> moves)
    {
        this.moves = moves;
    }

    public Transform Solve()
    {
        Transform solved = new GameObject("Solved").transform;
        foreach (Move move in moves)
        {
            move.CreateMove().SetParent(solved);
        }
        return solved;
    }
}

public class Move
{
    public Piece3D piece;
    public Vector3Int atPosition;
    public int direction;

    public Move(Piece3D piece, Vector3Int atPosition, int direction)
    {
        this.piece = piece;
        this.atPosition = atPosition;
        this.direction = direction;
    }

    public Piece3D Place(Piece3D inLevel)
    {
        return piece.Place(atPosition, direction, inLevel);
    }

    public Transform CreateMove()
    {
        return piece.CreatePiece(atPosition, direction);
    }
}

public class Piece3D
{
    public Color color;
    List<Vector3Int> cubes;

    public Piece3D(List<Vector3Int> cubes, Color color)
    {
        this.cubes = cubes;
        this.color = color;
    }

    public List<Vector3Int> Cubes(int direction)
    {
        var turnedShape = new List<Vector3Int>();

        switch (direction)
        {
            case 0:
                return cubes;
            case 1:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(-cube.x, cube.z, cube.y));
                }
                return turnedShape;
            case 2:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(cube.z, cube.x, cube.y));
                }
                return turnedShape;
            case 3:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(-cube.z, cube.y, cube.x));
                }
                return turnedShape;
            case 4:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(cube.y, cube.z, cube.x));
                }
                return turnedShape;
            case 5:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(-cube.y, cube.x, cube.z));
                }
                return turnedShape;
            case 6:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(-cube.x, -cube.y, cube.z));
                }
                return turnedShape;
            case 7:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(cube.x, -cube.z, cube.y));
                }
                return turnedShape;
            case 8:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(-cube.z, -cube.x, cube.y));
                }
                return turnedShape;
            case 9:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(cube.z, -cube.y, cube.x));
                }
                return turnedShape;
            case 10:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(-cube.y, -cube.z, cube.x));
                }
                return turnedShape;
            case 11:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(cube.y, -cube.x, cube.z));
                }
                return turnedShape;

            case 12:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(-cube.x, cube.y, -cube.z));
                }
                return turnedShape;
            case 13:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(cube.x, cube.z, -cube.y));
                }
                return turnedShape;
            case 14:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(-cube.z, cube.x, -cube.y));
                }
                return turnedShape;
            case 15:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(cube.z, cube.y, -cube.x));
                }
                return turnedShape;

            case 16:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(-cube.y, cube.z, -cube.x));
                }
                return turnedShape;
            case 17:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(cube.y, cube.x, -cube.z));
                }
                return turnedShape;
            case 18:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(cube.x, -cube.y, -cube.z));
                }
                return turnedShape;
            case 19:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(-cube.x, -cube.z, -cube.y));
                }
                return turnedShape;
            case 20:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(cube.z, -cube.x, -cube.y));
                }
                return turnedShape;
            case 21:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(-cube.z, -cube.y, -cube.x));
                }
                return turnedShape;
            case 22:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(cube.y, -cube.z, -cube.x));
                }
                return turnedShape;
            case 23:
                foreach (var cube in cubes)
                {
                    turnedShape.Add(new Vector3Int(-cube.y, -cube.x, -cube.z));
                }
                return turnedShape;
        }

        return cubes;
    }

    public Transform CreatePiece(Vector3Int atPosition, int direction)
    {
        Transform t = new GameObject("Piece").transform;
        t.transform.position = atPosition;
        foreach (var cube in Cubes(direction))
        {
            Object.Instantiate(Solver.instance.cube, atPosition + cube, Quaternion.identity, t);
        }
        return t;
    }

    public Piece3D Place(Vector3Int atPosition, int direction, Piece3D inLevel)
    {
        Debug.Log("En brikke er plassert: " + inLevel.Cubes(0).Count);
        // Sjekk hver cube i brikken
        foreach (Vector3Int cube in Cubes(direction))
        {
            Debug.Log(Cubes(direction).Count + " Gå inn nå");
            foreach (Vector3Int levelCube in inLevel.cubes)
            {
                if (atPosition + cube == levelCube)
                {
                    inLevel.cubes.Remove(levelCube);
                    Debug.Log(Cubes(direction).Count + " Fjerner en kube altså! :) Antall kuber er: " + inLevel.Cubes(0).Count);
                    break;
                }
            }
        }
        Debug.Log("En brikke er plassert: " + inLevel.Cubes(0).Count);

        return inLevel;
    }

    public List<Move> PossibleMoves(Piece3D inLevel)
    {
        List<Move> allMoves = new List<Move>();

        // Sjekker hver posisjon i levelet   
        foreach (var tryAt in inLevel.cubes)
        {
            // Sjekker hver rotasjon av brikken
            for (int d = 0; d < 24; d++)
            {
                bool shapeFits = true;

                // Sjeker hver cube i brikken
                foreach (var cube in Cubes(d))
                {
                    bool cubeFits = false;
                    // Sjekker om cuben passer i levelet
                    foreach (var levelCube in inLevel.cubes)
                    {
                        if (cube + tryAt == levelCube)
                        {
                            cubeFits = true;
                            break;
                        }
                    }
                    if (!cubeFits)
                    {
                        shapeFits = false;
                        break;  
                    }
                }

                if (shapeFits)
                {
                    allMoves.Add(new Move(this, tryAt, d));
                }
            }
        }

        return allMoves;
    }
}
