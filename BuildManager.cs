using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    public StructureScriptableObjects Structure;
    public BuildingInterface buildinginterface;
    Vector3 oldPos;
    public List<GameObject> ghosts = new List<GameObject>();

    bool deleteGhosts;

    // Start is called before the first frame update
    void Start()
    {
        buildinginterface = new SingleBuilding();
    }

    // Update is called once per frame
    void Update()
    {
        if(Structure == null)
        {
            if (deleteGhosts == true)
            {
                for (int i = 0; i < ghosts.Count; i++)
                {
                    Destroy(ghosts[i]);
                }
                ghosts.Clear();
                deleteGhosts = false;
            }
        }
        else
        {
            deleteGhosts = true;
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 MousePosition = new Vector3(Mathf.RoundToInt(mouse.x), Mathf.RoundToInt(mouse.y), -1);

            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                buildinginterface.ButtonDown(MousePosition);
            }

            if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
            {
                buildinginterface.ButtonUp(MousePosition, Structure);
                ghosts = buildinginterface.ClearAllGhosts(ghosts);
            }

            if (oldPos != MousePosition)
            {
                ghosts = buildinginterface.UpdateGhost(MousePosition, ghosts, Structure);
                oldPos = MousePosition;
            }
        }
    }

    public void GetStructure(StructureScriptableObjects Structure)
    {
        this.Structure = Structure;
    }

    public void ChangeInterfaceToHouse()
    {
        buildinginterface = new SingleBuilding();
    }
    public void ChangeInterfaceToRoad()
    {
        buildinginterface = new DragAllDirectionsBuilding();
    }
    public void ChangeInterfaceToWall()
    {
        buildinginterface = new DragStraightDirectionBuilding();
    }
}
