using UnityEngine.SceneManagement;

public class ResetLevelState : State
{
    public override void OnEnter()
    {
        base.OnEnter();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
