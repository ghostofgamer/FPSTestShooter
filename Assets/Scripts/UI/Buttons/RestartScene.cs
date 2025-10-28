using UnityEngine.SceneManagement;

namespace UI.Buttons
{
    public class RestartScene : AbstractButton
    {
        public override void OnClick()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}