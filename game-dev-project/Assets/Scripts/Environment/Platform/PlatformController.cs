using System.Collections;
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

    public void MovePlatforms(Lever.LeverState leverState, Platform.PlatformColor color)
    {
        IEnumerable<Platform> sameColorPlatform = platforms.Where(x => x.Color == color);

        bool inShiftState = InShiftingState(leverState);
        
        foreach (var platform in sameColorPlatform)
        {
            if (inShiftState)
            {
                platform.StartShifting();
            }
            else 
            {
                platform.StopShifting();
            }
        }
    }

    private bool InShiftingState(Lever.LeverState leverState)
    {
        return leverState == Lever.LeverState.On;
    }
}
