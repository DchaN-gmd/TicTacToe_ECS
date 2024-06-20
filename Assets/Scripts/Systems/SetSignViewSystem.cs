using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SetSignViewSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public SetSignViewSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Clicked);
    }

    protected override bool Filter(GameEntity entity)
    {
        return !entity.hasTaken && entity.isClicked;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.AddTaken(GameState.CurrentType);

            switch (GameState.CurrentType)
            {
                case SignType.Cross:
                    GameObject.Instantiate(_contexts.config.configurator.value.CrossPrefab,
                        entity.cellViewComponents.Value.transform);
                    break;
                case SignType.Circle:
                    GameObject.Instantiate(_contexts.config.configurator.value.CirclePrefab,
                        entity.cellViewComponents.Value.transform);
                    break;
                default: throw new ArgumentOutOfRangeException();
            }

            GameState.CurrentType = GameState.CurrentType == SignType.Circle ? SignType.Cross : SignType.Circle;
        }
    }
}