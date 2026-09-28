





## enum to keep track of different areas in readable manner
public enum AreaType
{
    Forrest,
    City,
    Ocean
}


## AudioManager
Serializable class die werkt met Serializable struct:

[System.Serializable]
   public struct AreaMusicMapping
   {
       public AreaType area;
       public AudioClip musicClip;
   }
 [SerializeField] private List<AreaMusicMapping> areaMusicList;


// IEnumerator --> in order to spread crossfade over multiple frames

 private IEnumerator Crossfade(AudioSource source, AudioClip clip)
 {

    // use `Time.deltaTime` to sum elapsed time
    // use `yield return null`
    // use Mathf.Lerp for a linear crossfade, with start volume, target volume and progress
 }

// StartCoroutine(Crossfade(newSource, newClip));



For now, we can simply iterate the list to retrieve the mapped audio. However, with many more areas, this would consume too much cpu --> in runtime create dictionary to store the mapping: new Dictionary<AreaType, AudioClip>();

To search the list, use
`foreach (var mapping in areaMusicList)`
or use LINQ --> todo, read about
mapping = areaMusicList.FirstOrDefault(m => m.area == newArea);





## Trigger zones
TODO - Use trigger zones instead of planes
--> create 3d plane above visible walkable surface
--> isTrigger - turn on, mesh renderer uit
--> set area
