using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class LinkBoxColliderSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public LinkBoxColliderSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.CellViewComponents));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasCellViewComponents && !entity.hasCollider;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.AddCollider(entity.cellViewComponents.Value.GetComponent<BoxCollider>());
        }
    }
}