using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellAndRelativePosition 
{
    public Cell cell;           // Casilla.
    public Direction direction; // Direction a tomar despues de borrar el muro
   
    public enum Direction
    {
        Norte,
        Sur,
        Este,
        Oeste
    }

    public CellAndRelativePosition (Cell cell, Direction direction)
    {
        this.cell = cell;
        this.direction = direction;
    }

}
