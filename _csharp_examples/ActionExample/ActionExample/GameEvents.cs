namespace ActionExample;
using System;
public class GameEvents
{
    public static event Action<string> OnJump;
    public static event Action OnEndGame;
    
    public static void Jump(string characterTag)
    {
        OnJump?.Invoke(characterTag);
    }
    public static void EndGame()
    {
        OnEndGame?.Invoke();
    }
}