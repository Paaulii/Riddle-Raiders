
public class LaserReciever : PlatformActivator
{
    public bool isActive;
    private bool wasActivationEventSent;
    private bool wasInactivationEventSent;

    void Update() {
        if (isActive)
        {
            if (!wasActivationEventSent) 
            {
                ParticleEffectsSystem.instance.SpawnEffect(EffectType.LaserEffect, transform.position);
                SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Laser, transform.position);
                wasActivationEventSent = true;
                wasInactivationEventSent = false;
                ChangeState(State.On);
            }
        }
        else 
        {
            if (!wasInactivationEventSent)
            {
                wasInactivationEventSent = true;
                wasActivationEventSent = false;
                ChangeState(State.Off);
            }
        }
        
        isActive = false;
    }
}
