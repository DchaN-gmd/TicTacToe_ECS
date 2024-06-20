using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game]
public class CellViewComponents : IComponent
{
    [EntityIndex] public GameObject Value;
}