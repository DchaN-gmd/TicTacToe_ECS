using System.Linq;
using Entitas;
using UnityEngine;

public class InputSystem : IExecuteSystem, IInitializeSystem
{
    private Camera _camera;
    private Contexts _context;

    public InputSystem(Contexts context)
    {
        _context = context;
    }

    public void Initialize()
    {
        _camera = Camera.main;
    }

    public void Execute()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo))
            {
                var cellView = hitInfo.collider;

                var cells = _context.game.GetGroup(GameMatcher.AllOf(GameMatcher.CellViewComponents, GameMatcher.Collider));

                var hitCell = cells.AsEnumerable().FirstOrDefault((x) => x.collider.Value.Equals(cellView));
                if (hitCell != null)
                {
                    hitCell.isClicked = true;
                }
            }
        }
    }
}