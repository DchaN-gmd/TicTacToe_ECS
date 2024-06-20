using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Configurator _configurator;
    private Systems _systems;
    private Contexts _contexts;

    private void Start()
    {
        _contexts = new Contexts();

        _contexts.config.SetConfigurator(_configurator);

        InitSystems();

        _systems.Initialize();
    }

    private void Update()
    {
        _systems.Execute();
        _systems.ActivateReactiveSystems();
        _systems.Cleanup();
    }

    private void InitSystems()
    {
        _systems = new Feature("Game")
            .Add(new InitCellsSystem(_contexts))
            .Add(new InputSystem(_contexts))
            .Add(new CheckWinSystem(_contexts))
            .Add(new SetSignViewSystem(_contexts))
            .Add(new CreateCellViewSystem(_contexts))
            .Add(new LinkBoxColliderSystem(_contexts));
    }
}