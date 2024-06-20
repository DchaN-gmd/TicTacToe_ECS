using Entitas;
using UnityEngine;

public class InitCellsSystem : IInitializeSystem
{
    private Configurator _configurator;
    private Contexts _contexts;

    public InitCellsSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var cellPrefab = _contexts.config.configurator.value.CellPrefab;
        var height = _contexts.config.configurator.value.BorderHeight;
        var width = _contexts.config.configurator.value.BorderWidth;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cellEntity = _contexts.game.CreateEntity();
                cellEntity.AddComponent(0, new CellComponent());

                cellEntity.AddPosition(new Vector2Int(x, y));
                cellEntity.AddResource(_contexts.config.configurator.value.CellPrefab);
            }
        }
    }
}