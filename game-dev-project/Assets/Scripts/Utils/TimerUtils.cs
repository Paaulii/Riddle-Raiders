using System.Text;
using UnityEngine;

public static class TimerUtils
{
    public static string GetFormatedTime(float time)
    {
        if (time == -1) 
        {
            return "--:--";
        }
		
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
		
        StringBuilder builder = new StringBuilder();
        builder.Append(minutes < 10 ? "0" : "");
        builder.Append(minutes);
        builder.Append(":");
        builder.Append(seconds < 10 ? "0" : "");
        builder.Append(seconds);
        return builder.ToString();
    }
}
