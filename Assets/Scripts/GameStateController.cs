using SimpleEventBus.Disposables;
using Plugins.SimpleEventBus;
using Events;

public class GameStateController
{
    private readonly CompositeDisposable _subscriptions;

    public GameStateController()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.UserInterface.Subscribe<ExitGameEvent>(ExitGameEventHandler)
        };      
    }

    private void ExitGameEventHandler(ExitGameEvent eventData)
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Dispose()
    {
        _subscriptions.Dispose();
    }
}