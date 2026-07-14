using System.Data;

namespace ActionExample;
using System.Threading;

public class GameLoopMock
{
    private int _counter = 0;
    public bool IsRunning => _isRunning;
    private bool _isRunning;
    public GameLoopMock()
    {
        Console.WriteLine("GameLoopMock.constructor");
        StartLoop();
    }
    
    private void StartLoop()
    {
        _isRunning = true;
        Console.WriteLine("GameLoopMock.StartLoop");
        while (IsRunning)
        {
            Update();
            Thread.Sleep(1000);
        };
    }

    private void Update()
    {
        Console.WriteLine($"GameLoopMock.Update - Counter: {_counter}, calling Jump");
        // just for the sake of the example. 
        string exampleCharacterId = _counter % 2 == 0 ? "player" : "NPC_monster";
        // working with strings, but should enums
        GameEvents.Jump(exampleCharacterId);
        
        _counter++;
        if (_counter >= 10)
        {
            _isRunning = false;
            Console.WriteLine("GameLoopMock.Update - stopping loop, calling EndGame");  
            GameEvents.EndGame();
        }
    }
}