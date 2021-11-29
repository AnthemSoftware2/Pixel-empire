using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure
{
    public RequiredInventory RequiredInventory;

    public Structure(StructureScriptableObjects structureScriptable)
    {
        if (structureScriptable.buildingMaterial.Length != 0) 
        { 
            RequiredInventory = new RequiredInventory();
            RequiredInventory.RequiredBuildingMaterial = new BuildingMaterial[structureScriptable.buildingMaterial.Length];
            for (int i = 0; i < structureScriptable.buildingMaterial.Length; i++)
            {
                RequiredInventory.RequiredBuildingMaterial[i] = new BuildingMaterial(structureScriptable.buildingMaterial[i].amount, structureScriptable.buildingMaterial[i].materialType);
            }
        }
    }
}
