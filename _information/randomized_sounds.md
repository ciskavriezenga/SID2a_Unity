# Randomized sounds

## Random container
TODO@C- write short 'how to'

## Single audio clip & randomization
### Randomized pitch shift
Simply randomize the pitch of the audio source that is used to
play the audio clip (either as an assigned clip or as a one shot) in your scipt as follow.

```csharp
audioSourceOneShotsRandomized.pitch = Random.Range(0.9f, 1.1f);
```
In this snippet, hard coded values are used. However, you can add and use two public float properties to your script that can be adapted within Unity in the script's component inspector view.


### Randomized Low pass filter cutoff
In the inspector view, with the audio source object selected, add an 'Audio Low Pass Filter' component.
In the script that handles the playback of the audio clip to which you want to apply a randomized LPF cutoff frequency, simply randomize the cuttoffFrequency as follow.

```csharp
randomizedLPF.cutoffFrequency = Random.Range(1000.0f,10000.0f)
```
Similarly to the code snippet demonstration the pitch randomisation, instead of hardcoded values for the range, you can add and use two public float properties to your script that can be adapted within Unity in the script's component inspector view.
