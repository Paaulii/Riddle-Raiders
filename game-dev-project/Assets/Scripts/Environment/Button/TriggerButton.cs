using System.Linq;
using UnityEngine;

public class TriggerButton : PlatformActivator
{
    [SerializeField] protected Character.CharacterType[] allowedPlayersToActivate;

    [Header("Appearance")] 
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Sprite pressedButton;
    [SerializeField] private Sprite releasedButton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryChangeState(collision, true);
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        TryChangeState(collision, false);
    }

    private void TryChangeState(Collider2D collision, bool onTriggerEnter)
    {
        if (!collision.TryGetComponent(out Character player) || 
            !allowedPlayersToActivate.Contains(player.Type))
        {
            return;
        }
        
        renderer.sprite = onTriggerEnter? pressedButton : releasedButton;
        ChangeState(onTriggerEnter? State.On : State.Off);
    }
}
