using System;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    
    // add Audiomanager
    public AreaType currentArea = AreaType.Forest;

    public static AreaManager Instance { get; private set; }

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void UpdateArea(AreaType newArea)
    {
        if (newArea != currentArea)
        {
            Debug.Log("Area changed to " + newArea);
            AudioManager.Instance.SwitchMusic(newArea);
        }
        
        
    }
    

}
