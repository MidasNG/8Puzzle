using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField] private int oldrow, oldcol;
    public int row, col;
    public PuzzleController Puzzle;
    [SerializeField] private AnimationCurve curve;
    private bool move;
    private float t;
    void Start()
    {
        transform.position = new Vector3(col, 0, row);
        oldrow = row; oldcol = col;
        if (!Puzzle.CheckPos(row, col)) gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0 && col < 2 && !Puzzle.CheckPos(row, col + 1) && !move)
        {
            col++;
            Puzzle.InvertOccupation(row, col);
            move = true;
        }

        if (Input.GetAxis("Horizontal") < 0 && col > 0 && !Puzzle.CheckPos(row, col - 1) && !move)
        {
            col--;
            Puzzle.InvertOccupation(row, col);
            move = true;
        }

        if (Input.GetAxis("Vertical") < 0 && row > 0  && !Puzzle.CheckPos(row -1, col) && !move)
        {
            row--;
            Puzzle.InvertOccupation(row, col);
            move = true;
        }

        if (Input.GetAxis("Vertical") > 0 && row < 2 && !Puzzle.CheckPos(row + 1, col) && !move)
        {
            row++;
            Puzzle.InvertOccupation(row, col);
            move = true;
        }

        if (move)
        {
            t = Mathf.Clamp(t + 2 * Time.deltaTime, 0, 1);
            transform.position = Vector3.Lerp(transform.position, new Vector3(col, 0, row), curve.Evaluate(t));
            if (t == 1)
            {
                move = false;
                Puzzle.InvertOccupation(oldrow, oldcol);
                oldrow = row;
                oldcol = col;
                t = 0;
            }
        }
    }
}
