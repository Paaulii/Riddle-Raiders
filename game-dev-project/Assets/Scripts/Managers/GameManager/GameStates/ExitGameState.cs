
public class ExitGameState : State {
    public override void OnEnter()
    {
        ExitGame();
    }

    private void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else 
            Application.Quit();
        #endif
    }
}