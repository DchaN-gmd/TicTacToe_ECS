using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CheckWinSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public CheckWinSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Taken));
    }

    protected override bool Filter(GameEntity entity)
    {
        return !entity.isWinner;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var cells = _contexts.game.GetEntities(GameMatcher.AllOf(GameMatcher.Cell, GameMatcher.Position));

        var cellsDictionary = new Dictionary<Vector2Int, GameEntity>();

        foreach (var cell in cells)
        {
            cellsDictionary.Add(cell.position.Value, cell);
        }

        foreach (var entity in entities)
        {
            var horizontal = GetDirectionCount(cellsDictionary, new Vector2Int(1, 0), entity.position.Value,
                entity.taken.SignType);
            var vertical = GetDirectionCount(cellsDictionary, new Vector2Int(0, 1), entity.position.Value,
                entity.taken.SignType);
            var diagonalFirst = GetDirectionCount(cellsDictionary, new Vector2Int(1, 1), entity.position.Value,
                entity.taken.SignType);
            var diagonalSecond = GetDirectionCount(cellsDictionary, new Vector2Int(-1, 1), entity.position.Value,
                entity.taken.SignType);

            var maxCount = Mathf.Max(horizontal, vertical, diagonalFirst, diagonalSecond);

            if (maxCount >= _contexts.config.configurator.value.CountToWin)
            {
                entity.isWinner = true;
                Debug.Log("win");
                break;
            }
        }
    }

    private int GetDirectionCount(Dictionary<Vector2Int, GameEntity> cells, Vector2Int direction, Vector2Int position, SignType signType)
    {
        var currentPosition = position + direction;

        int directionCount = 1;

        while (cells.TryGetValue(currentPosition, out var cell))
        {
            if (!cell.hasTaken) break;

            if (cell.taken.SignType != signType) break;

            directionCount++;
            currentPosition += direction;
        }

        currentPosition = position - direction;

        while (cells.TryGetValue(currentPosition, out var cell))
        {
            if (!cell.hasTaken) break;

            if (cell.taken.SignType != signType) break;

            directionCount++;
            currentPosition -= direction;
        }

        return directionCount;
    }
}