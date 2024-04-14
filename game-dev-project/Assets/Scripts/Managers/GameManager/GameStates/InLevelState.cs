using System;

public class InLevelState : State
{
    public override void OnEnter()
    {
        base.OnEnter();
        SoundManager.Instance.PlayMusic(SoundManager.MusicType.Gameplay);
    }
}
