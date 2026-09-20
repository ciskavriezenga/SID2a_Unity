namespace ActionExample;

public class AudioSystem
{
    private CharacterAudioController _playerAudioController;
    public AudioSystem()
    {
        _playerAudioController = new CharacterAudioController("player");
        GameEvents.OnJump += Jump;
        GameEvents.OnEndGame += EndGame;
    }
    public void Jump(string characterId)
    {
        Console.WriteLine($"AudioSystem.Jump - characterId: {characterId}" +
                          $", if {characterId} == player - play player jump sound.");
        if (characterId == "player")
        {
            _playerAudioController.Jump();    
        }
    }
    public void EndGame()
    {
        Console.WriteLine("AudioSystem.EndGame - *** PLAYING END GAME SOUND ***.");
    }
}