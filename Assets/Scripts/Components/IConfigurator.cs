using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Config, Unique, ComponentName("Configurator")]
public interface IConfigurator
{
    public GameObject CellPrefab { get; }
    public GameObject CrossPrefab { get; }
    public GameObject CirclePrefab { get; }
    public int BorderHeight { get; }
    public int BorderWidth { get; }
    public int CountToWin { get; }
}