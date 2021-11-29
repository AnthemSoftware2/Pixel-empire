using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land
{
    Tile[,] tiles;
    public List<StockPIleTest> stockpiles;

    public Dictionary<Structure, GameObject> structuresObj;

    public Tile[,] Tiles
    {
        get
        {
            return tiles;
        }
    }

    int width;
    int height;

    public Land(int width, int height, tileType[] type)
    {
        this.width = width;
        this.height = height;
        tiles = new Tile[width, height];
        structuresObj = new Dictionary<Structure, GameObject>();
        stockpiles = new List<StockPIleTest>();

        int xOrg = Random.Range(-100, 100);
        int yOrg = Random.Range(-100, 100);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = xOrg + x;
                float yCoord = yOrg + y;
                float sample = Mathf.PerlinNoise(xCoord * 0.15f, yCoord * 0.15f);
                sample = Mathf.Clamp01(sample);

                for (int i = 0; i < type.Length; i++)
                {
                    if(sample >= type[i].MinNoiseAmount && sample < type[i].MaxNoiseAmount)
                    {
                        tiles[x, y] = new Tile(x, y, type[i].Layer, i);
                    }
                }
            }
        }
    }
    

    public void SpawnStructure(StructureScriptableObjects structure, int x, int y)
    {
        Structure strut = new Structure(structure);
        GameObject obj = new GameObject();
        obj.transform.position = new Vector3(x, y, -1.5f);
        obj.AddComponent<SpriteRenderer>().sprite = structure.sprite;
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);

        structuresObj.Add(strut, obj);
        
        tiles[x, y].PlaceStructure(strut);

        JobManager.instance.BuildingJob(tiles[x, y], structure);
    }

    public StockPIleTest GetStockpile (BuildingMaterialType type)
    {
        for (int i = 0; i<stockpiles.Count; i++)
        {
           if (stockpiles[i].inventory.AsType(type))
           {
               if (stockpiles[i].inventory.GetMaterialOfType(type).amount != 0)
               {
                   return stockpiles[i];
               }
           }
        }

        return null;
    }


    public void StructureIsDone(Structure strut)
    {
        structuresObj[strut].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

}
