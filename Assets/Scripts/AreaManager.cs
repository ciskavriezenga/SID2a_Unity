using UnityEngine;

public class AreaManager : MonoBehaviour
{
    
    // add Audiomanager
    public AreaType currentArea = AreaType.Forest;

    public void UpdateArea(AreaType newArea)
    {
        // TODO - check if we indeed entered a new area
        Debug.Log("Area currently is " + currentArea);
        Debug.Log("Area changed to " + newArea);
    }
    

}
