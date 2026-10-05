# Location based Music


1. Add cubes to define areas, for each cube:
  - turn of rendering, 'mesh renderer'
  - under collision component - set isTrigger to true


2. Create Area.cs script, add this to all area cubes. To this script, add:

```
  public AreaType areaType;
```
3. Create AreaType enum, either in seperate AreaType.cs script or within Area.cs script.

4. Create an AreaManager, which keeps track of the currentArea and contains the function `UpdateArea`:
```
private AreaType currentArea;
...
public void UpdateArea(AreaType areaType)
```

5. In PlayerController add the function `public void OnTriggerEnter(Collider other)` which updates the `currentArea` when relevant and then signals AudioManager, see next step.

6. Create an AudioManager, make it Serializable by adding the following code:
```
Serializable class die werkt met Serializable struct:

[System.Serializable]
   public struct AreaMusicMapping
   {
       public AreaType area;
       public AudioClip musicClip;
   }
 [SerializeField] private List<AreaMusicMapping> areaMusicList;

```

AudioClip: _"AudioClips are referenced and used by AudioSources to play sounds."_
https://docs.unity3d.com/6000.6/Documentation/ScriptReference/AudioClip.html

This allows us to easily assign the music files to audio areas

For now, we can simply iterate the list to retrieve the mapped audio. However, with many more areas, this would consume too much cpu --> in runtime create dictionary to store the mapping: new Dictionary<AreaType, AudioClip>();

To search the list, use
`foreach (var mapping in areaMusicList)`
or use LINQ --> todo, read about
mapping = areaMusicList.FirstOrDefault(m => m.area == newArea);


7. Add two AudioSources, load initial audio clip to first source and reference
```
private AudioSource audioSource1;
private AudioSource audioSource2;
private AudioSource activeSource;
...
// Check which targetSource is currently not active:
AudioSource inactiveSource = audioSource1;
if(activeSource == audioSource1) {
  inactiveSource == audioSource2;
}
```
Or 'shortified' if statement: `(if statement) TRUE ? FALSE;`
```
AudioSource inactiveSource = (activeSource == audioSource1) ? audioSource2 : audioSourceA
```

8. Implement crossfade, required snippets of code:
```
// IEnumerator --> in order to spread crossfade over multiple frames

 private IEnumerator Crossfade(AudioSource source, AudioClip clip)
 {
    // use `Time.deltaTime` to sum elapsed time
    // use `yield return null`
    // use Mathf.Lerp for a linear crossfade, with start volume, target volume and progress
 }

// StartCoroutine(Crossfade(newSource, newClip));

```
