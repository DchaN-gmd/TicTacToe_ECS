using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CreateCellViewSystem : ReactiveSystem<GameEntity>
{
    private Contexts _context;

    public CreateCellViewSystem(Contexts context) : base(context.game)
    {
        _context = context;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Cell);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isCell && entity.hasResource && !entity.hasCellViewComponents;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var newCell = GameObject.Instantiate(e.resource.Value);
            newCell.transform.position = new Vector3(e.position.Value.x, e.position.Value.y, 0);

            e.AddCellViewComponents(newCell);
        }
    }
}