using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandManager : MonoBehaviour
{
    public static LandManager instance;

    public int width;
    public int height;
    public Land land;
    public tileType[] TileTypes;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        land = new Land(width, height, TileTypes);
        CreateGraphics();
    }

    void CreateGraphics()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject obj = new GameObject();
                obj.transform.position = new Vector3(x, y, 0);
                obj.transform.SetParent(transform);
                obj.name = "GameObject " + x + "_" + y;
                obj.AddComponent<SpriteRenderer>().sprite = TileTypes[land.Tiles[x, y].TileType].sprite;
            }
        }
    }
}

[System.Serializable]
public struct tileType
{
    public Sprite sprite;
    public int Layer;
    public float MinNoiseAmount;
    public float MaxNoiseAmount;
}
