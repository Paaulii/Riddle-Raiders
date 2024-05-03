using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private Platform[] platforms;
    
    private void Start() 
    {
        platforms = GetComponentsInChildren<Platform>(true);
    }

    public void MovePlatforms(PlatformActivator.State state, Platform.PlatformColor group)
    {
        IEnumerable<Platform> sameGroupPlatforms = platforms.Where(x => x.Color == group);

        bool inShiftState = state == PlatformActivator.State.On;
        
        foreach (var platform in sameGroupPlatforms)
        {
            if (inShiftState)
            {
                platform.Shift();
            }
            else 
            {
                platform.Return();
            }
        }
    }
}
