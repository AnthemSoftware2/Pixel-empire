using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{

    int layer;
    public int Layer
    {
        get
        {
            return layer;
        }
    }

    int tiletype;
    public int TileType
    {
        get
        {
            return tiletype;
        }
    }

    Structure structure;
    public Structure Structure
    {
        get
        {
            return structure;
        }
    }

    int x;
    public int X
    {
        get
        {
            return x;
        }
    }

    int y;
    public int Y
    {
        get
        {
            return y;
        }
    }

    

    public Tile(int x, int y, int layer, int tiletype)
    {
        this.layer = layer;
        this.tiletype = tiletype;
        this.x = x;
        this.y = y;
    }

    public void PlaceStructure(Structure structure)
    {
        this.structure = structure;
    }
}
