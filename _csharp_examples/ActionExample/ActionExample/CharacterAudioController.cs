namespace ActionExample;

public class CharacterAudioController
{
    public string _characterTag;
    public CharacterAudioController(string characterTag)
    {
        _characterTag = characterTag;
    }
    public void Jump()
    {
        Console.WriteLine($"CharacterAudioController.Jump - characterTag: {_characterTag}, " +
                          $"*** PLAYING PLAYER JUMP SOUND ***.");
    }
}