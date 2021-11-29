using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RequiredInventory
{
    public BuildingMaterial[] RequiredBuildingMaterial;

    public void AddToInventory(BuildingMaterial material, int AmountToAdd)
    {
        if (AmountToAdd > material.amount)
        {
            AmountToAdd = material.amount;
        }


        for (int i = 0; i < RequiredBuildingMaterial.Length; i++)
        {
            if (RequiredBuildingMaterial[i].materialType == material.materialType)
            {
                int final = AmountToAdd;
                if(RequiredBuildingMaterial[i].amount - AmountToAdd < 0)
                {
                    int remaider = AmountToAdd - RequiredBuildingMaterial[i].amount;
                    final -= remaider;
                }

                RequiredBuildingMaterial[i].amount -= final;
                material.amount -= final;
                return;
            }
        }
    }

    public BuildingMaterial GetRequiredMaterial()
    {
        for (int i = 0; i < RequiredBuildingMaterial.Length; i++)
        {
            if(RequiredBuildingMaterial[i].amount != 0)
            {
                return RequiredBuildingMaterial[i];
            }
        }

        return null;
    }

    public bool RequirementDone()
    {
        for (int i = 0; i < RequiredBuildingMaterial.Length; i++)
        {
            if(RequiredBuildingMaterial[i].amount != 0)
            {
                return false;
            }
        }
        return true;
    }
}

[Serializable]
public class CarryInventory
{
    public int MaxAmount;
    public List<BuildingMaterial> materials = new List<BuildingMaterial>();

    public BuildingMaterial GetMaterialOfType(BuildingMaterialType type)
    {
        for (int i = 0; i < materials.Count; i++)
        {
            if (materials[i].materialType == type)
            {
                return materials[i];
            }
        }

        return null;
    }

    public void AddToInventory(BuildingMaterial material, int AmountToAdd)
    {
        if (AmountToAdd > material.amount)
        {
            AmountToAdd = material.amount;
        }

        AmountToAdd = Mathf.Clamp(AmountToAdd, 0, MaxAmount);

        for (int i = 0; i < materials.Count; i++)
        {
            if(materials[i].materialType == material.materialType)
            {
                materials[i].amount += AmountToAdd;
                material.amount -= AmountToAdd;
                return;
            }
        }

        materials.Add(new BuildingMaterial(AmountToAdd, material.materialType));
        material.amount -= AmountToAdd;
    }

    public bool AsType(BuildingMaterialType type)
    {
        for (int i = 0; i < materials.Count; i++)
        {
            if (materials[i].materialType == type && materials[i].amount != 0)
            {
                return true;
            }
        }

        return false;
    }
}
