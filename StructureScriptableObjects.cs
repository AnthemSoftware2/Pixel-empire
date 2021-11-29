using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BuildingMaterialType
{
    Wood,
    Stone
}

[CreateAssetMenu(fileName = "New Structure", menuName = "New Structure")]
public class StructureScriptableObjects : ScriptableObject
{
    public LayerMask RuleLayer;
    public Sprite sprite;
    public float JobTime;
    public BuildingMaterial[] buildingMaterial;
}

[Serializable]
public class BuildingMaterial
{
    public BuildingMaterialType materialType;
    public int amount;

    public BuildingMaterial(int amount, BuildingMaterialType materialType)
    {
        this.amount = amount;
        this.materialType = materialType;
    }
}