using System;
using UnityEngine;

public class EnterEndDoor : MonoBehaviour
{
    public Action onEnterEndDoor;

    [SerializeField] GameObject openDoorSprite;
    [SerializeField] GameObject closeDoorSprite;
    private bool smallPlayerAtDoor;
    private bool bigPlayerAtDoor;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Character character = col.GetComponent<Character>();
        if (character == null) 
        {
            return;
        }
        
        switch (character.Type)
        {
            case Character.CharacterType.Small:
                smallPlayerAtDoor = true;
                break;
            case Character.CharacterType.Big:
                bigPlayerAtDoor = true;
                break;
        }

        if (!smallPlayerAtDoor || !bigPlayerAtDoor) return;
        openDoorSprite.SetActive(true);
        closeDoorSprite.SetActive(false);
        onEnterEndDoor?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Character character = col.GetComponent<Character>();
        
        if (character == null) 
        {
            return;
        }

        switch (character.Type)
        {
            case Character.CharacterType.Small:
                smallPlayerAtDoor = false;
                break;
            case Character.CharacterType.Big:
                bigPlayerAtDoor = false;
                break;
        }
    }
}
