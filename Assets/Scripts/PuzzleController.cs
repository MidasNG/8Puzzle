
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    private bool[,] posCheck = new bool [3, 3];
    public TileController[] tiles = new TileController[9];
    public bool shuffle;
    void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                posCheck[i, j] = true;
            }
        }

        if (shuffle)
        {
            int currentTile;
            int row = 2, col = 2;
            TileController temp;
            for (int i = 8; i >= 0; i--)
            {
                currentTile = Random.Range(0, i + 1);
                temp = tiles[currentTile];
                tiles[currentTile] = tiles[i];
                tiles[i] = temp;
                tiles[i].row = row; tiles[i].col = col;
                if (col > 0)
                {
                    col--;
                }
                else
                {
                    row--; col = 2;
                }
            }
        }
        posCheck[2, 2] = false;
    }
    public void InvertOccupation(int row, int col)
    {
        posCheck[row, col] = !posCheck[row, col];
    }
    public bool CheckPos(int row, int col)
    {
        return posCheck[row, col];
    }
}
