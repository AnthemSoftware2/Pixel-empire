using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BuildingInterface
{
    List<GameObject> UpdateGhost(Vector3 MousePosition, List<GameObject> Ghost, StructureScriptableObjects StructureObject);
    List<GameObject> ClearAllGhosts(List<GameObject> Ghost);

    void ButtonDown(Vector3 MousePosition);
    void ButtonUp(Vector3 MousePosition, StructureScriptableObjects structure);
}


public class SingleBuilding : BuildingInterface
{
    public List<GameObject> UpdateGhost(Vector3 MousePosition, List<GameObject> Ghost, StructureScriptableObjects StructureObject)
    {
        bool canBuild = ToolsToUseForBuilding.CanBuildBool((int)MousePosition.x, (int)MousePosition.y, StructureObject.RuleLayer);

        if (Ghost.Count == 0)
        {
            GameObject obj = new GameObject();
            obj.AddComponent<SpriteRenderer>().sprite = StructureObject.sprite;

            ToolsToUseForBuilding.ChangeObjectColor(obj, canBuild);

            obj.transform.position = MousePosition;
            Ghost.Add(obj);
        }
        else if(Ghost[0].GetComponent<SpriteRenderer>().sprite != StructureObject.sprite)
        {
            Ghost[0].GetComponent<SpriteRenderer>().sprite = StructureObject.sprite;

            ToolsToUseForBuilding.ChangeObjectColor(Ghost[0], canBuild);

            Ghost[0].transform.position = MousePosition;
        }
        else
        {
            Ghost[0].transform.position = MousePosition;

            ToolsToUseForBuilding.ChangeObjectColor(Ghost[0], canBuild);
        }

        return Ghost;
    }

    public List<GameObject> ClearAllGhosts(List<GameObject> Ghost)
    {
        
        return Ghost;
    }

    public void ButtonDown(Vector3 MousePosition)
    {
        
    }

    public void ButtonUp(Vector3 MousePosition, StructureScriptableObjects structure)
    {
        Tile CurrentTile = LandManager.instance.land.Tiles[(int)MousePosition.x, (int)MousePosition.y];

        if (ToolsToUseForBuilding.CanBuildBool((int)MousePosition.x, (int)MousePosition.y, structure.RuleLayer))
        {
            LandManager.instance.land.SpawnStructure(structure, (int)MousePosition.x, (int)MousePosition.y);
        }
    }
}

public class DragAllDirectionsBuilding : BuildingInterface
{
    bool Hold;
    Vector2 oldMousePosition;
    bool OriginalGhost;

    public List<GameObject> UpdateGhost(Vector3 MousePosition, List<GameObject> Ghost, StructureScriptableObjects StructureObject)
    {
        if(Hold == false)
        {
            bool canBuild = ToolsToUseForBuilding.CanBuildBool((int)MousePosition.x, (int)MousePosition.y, StructureObject.RuleLayer);

            OriginalGhost = true;
            if (Ghost.Count == 0)
            {
                GameObject obj = new GameObject();
                obj.AddComponent<SpriteRenderer>().sprite = StructureObject.sprite;
                obj.transform.position = MousePosition;

                ToolsToUseForBuilding.ChangeObjectColor(obj, canBuild);

                Ghost.Add(obj);
            }
            else if (Ghost[0].GetComponent<SpriteRenderer>().sprite != StructureObject.sprite)
            {
                Ghost[0].GetComponent<SpriteRenderer>().sprite = StructureObject.sprite;

                ToolsToUseForBuilding.ChangeObjectColor(Ghost[0], canBuild);

                Ghost[0].transform.position = MousePosition;
            }
            else
            {
                ToolsToUseForBuilding.ChangeObjectColor(Ghost[0], canBuild);

                Ghost[0].transform.position = MousePosition;
            }
        }
        else
        {
            if(OriginalGhost == true)
            {
                MonoBehaviour.Destroy(Ghost[0]);
                Ghost.RemoveAt(0);
                OriginalGhost = false;
            }

            int startX = (int)oldMousePosition.x;
            int startY = (int)oldMousePosition.y;
            int endX = (int)MousePosition.x;
            int endY = (int)MousePosition.y;

            if (endX < startX)
            {
                int tmp = endX;
                endX = startX;
                startX = tmp;
            }

            if (endY < startY)
            {
                int tmp = endY;
                endY = startY;
                startY = tmp;
            }

            for (int i = 0; i < Ghost.Count; i++)
            {
                MonoBehaviour.Destroy(Ghost[i]);
            }
            Ghost.Clear();

            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {

                    bool canBuild = ToolsToUseForBuilding.CanBuildBool(x, y, StructureObject.RuleLayer);

                    GameObject obj = new GameObject();
                    obj.AddComponent<SpriteRenderer>().sprite = StructureObject.sprite;

                    ToolsToUseForBuilding.ChangeObjectColor(obj, canBuild);

                    obj.transform.position = new Vector3(x, y, MousePosition.z);
                    Ghost.Add(obj);
                }
            }
        }
        
        return Ghost;
    }

    public List<GameObject> ClearAllGhosts(List<GameObject> Ghost)
    {
        for (int i = 0; i < Ghost.Count; i++)
        {
            MonoBehaviour.Destroy(Ghost[i]);
        }
        Ghost.Clear();
        return Ghost;
    }

    public void ButtonDown(Vector3 MousePosition)
    {
        oldMousePosition = new Vector2(MousePosition.x, MousePosition.y);
        Hold = true;
    }

    public void ButtonUp(Vector3 MousePosition, StructureScriptableObjects structure)
    {
        int startX = (int)oldMousePosition.x;
        int startY = (int)oldMousePosition.y;
        int endX = (int)MousePosition.x;
        int endY = (int)MousePosition.y;

        if (endX < startX)
        {
            int tmp = endX;
            endX = startX;
            startX = tmp;
        }

        if (endY < startY)
        {
            int tmp = endY;
            endY = startY;
            startY = tmp;
        }

        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                Tile CurrentTile = LandManager.instance.land.Tiles[x, y];

                if (ToolsToUseForBuilding.CanBuildBool(x, y, structure.RuleLayer) == true)
                {
                    LandManager.instance.land.SpawnStructure(structure, x, y);
                }
            }
        }
        Hold = false;
    }
}

public class DragStraightDirectionBuilding : BuildingInterface
{
    bool Hold;
    Vector2 oldMousePosition;
    bool OriginalGhost;

    public List<GameObject> UpdateGhost(Vector3 MousePosition, List<GameObject> Ghost, StructureScriptableObjects StructureObject)
    {
        if (Hold == false)
        {
            bool canBuild = ToolsToUseForBuilding.CanBuildBool((int)MousePosition.x, (int)MousePosition.y, StructureObject.RuleLayer);

            OriginalGhost = true;
            if (Ghost.Count == 0)
            {
                GameObject obj = new GameObject();
                obj.AddComponent<SpriteRenderer>().sprite = StructureObject.sprite;
                obj.transform.position = MousePosition;

                ToolsToUseForBuilding.ChangeObjectColor(obj, canBuild);

                Ghost.Add(obj);
            }
            else if (Ghost[0].GetComponent<SpriteRenderer>().sprite != StructureObject.sprite)
            {
                Ghost[0].GetComponent<SpriteRenderer>().sprite = StructureObject.sprite;

                ToolsToUseForBuilding.ChangeObjectColor(Ghost[0], canBuild);

                Ghost[0].transform.position = MousePosition;
            }
            else
            {
                Ghost[0].transform.position = MousePosition;

                ToolsToUseForBuilding.ChangeObjectColor(Ghost[0], canBuild);
            }

        }
        else
        {
            if (OriginalGhost == true)
            {
                MonoBehaviour.Destroy(Ghost[0]);
                Ghost.RemoveAt(0);
                OriginalGhost = false;
            }

            bool yOnly = false;
            bool xOnly = false;

            if(MousePosition.y > oldMousePosition.y || MousePosition.y < oldMousePosition.y)
            {
                yOnly = true;
            }
            if (MousePosition.x > oldMousePosition.x || MousePosition.x < oldMousePosition.x)
            {
                xOnly = true;
            }

            int startX = (int)oldMousePosition.x;
            int startY = (int)oldMousePosition.y;

            int endX = startX;
            int endY = startY;

            if (yOnly == true)
            {
                endX = startX;
                endY = (int)MousePosition.y;
            }
            if(xOnly == true)
            {
                endY = startY;
                endX = (int)MousePosition.x;
            }


            if (endX < startX)
            {
                int tmp = endX;
                endX = startX;
                startX = tmp;
            }

            if (endY < startY)
            {
                int tmp = endY;
                endY = startY;
                startY = tmp;
            }

            for (int i = 0; i < Ghost.Count; i++)
            {
                MonoBehaviour.Destroy(Ghost[i]);
            }
            Ghost.Clear();

            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    bool canBuild = ToolsToUseForBuilding.CanBuildBool(x, y, StructureObject.RuleLayer);

                    GameObject obj = new GameObject();
                    obj.AddComponent<SpriteRenderer>().sprite = StructureObject.sprite;
                    obj.transform.position = new Vector3(x, y, MousePosition.z);

                    ToolsToUseForBuilding.ChangeObjectColor(obj, canBuild);

                    Ghost.Add(obj);
                }
            }
        }

        return Ghost;
    }

    public List<GameObject> ClearAllGhosts(List<GameObject> Ghost)
    {
        for (int i = 0; i < Ghost.Count; i++)
        {
            MonoBehaviour.Destroy(Ghost[i]);
        }
        Ghost.Clear();
        return Ghost;
    }

    public void ButtonDown(Vector3 MousePosition)
    {
        oldMousePosition = new Vector2(MousePosition.x, MousePosition.y);
        Hold = true;
    }

    public void ButtonUp(Vector3 MousePosition, StructureScriptableObjects structure)
    {

        bool yOnly = false;
        bool xOnly = false;

        if (MousePosition.y > oldMousePosition.y || MousePosition.y < oldMousePosition.y)
        {
            yOnly = true;
        }
        if (MousePosition.x > oldMousePosition.x || MousePosition.x < oldMousePosition.x)
        {
            xOnly = true;
        }

        int startX = (int)oldMousePosition.x;
        int startY = (int)oldMousePosition.y;

        int endX = startX;
        int endY = startY;

        if (yOnly == true)
        {
            endX = startX;
            endY = (int)MousePosition.y;
        }
        if (xOnly == true)
        {
            endY = startY;
            endX = (int)MousePosition.x;
        }

        if (endX < startX)
        {
            int tmp = endX;
            endX = startX;
            startX = tmp;
        }

        if (endY < startY)
        {
            int tmp = endY;
            endY = startY;
            startY = tmp;
        }

        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                Tile CurrentTile = LandManager.instance.land.Tiles[x, y];

                if (ToolsToUseForBuilding.CanBuildBool(x, y, structure.RuleLayer) == true)
                {
                    LandManager.instance.land.SpawnStructure(structure, x, y);
                }
            }
        }
        Hold = false;
    }

}


class ToolsToUseForBuilding
{
    public static bool CanBuildBool(int x, int y, LayerMask mask)
    {
        bool canBuild = false;
        Tile CurrentTile = LandManager.instance.land.Tiles[x, y];

        if (CurrentTile.Structure == null && IsInLayerMask(CurrentTile.Layer, mask) == true)
        {
            canBuild = true;
        }
        else
        {
            canBuild = false;
        }

        return canBuild;
    }

    public static void ChangeObjectColor(GameObject obj, bool BuildState)
    {
        if (BuildState == true)
        {
            obj.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 150);
        }
        else
        {
            obj.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 150);
        }
    }

    public static bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << layer)) > 0);
    }
}

