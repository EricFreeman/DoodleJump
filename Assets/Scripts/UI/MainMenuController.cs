using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    {
        public void PlayButtonClicked()
        {
            Application.LoadLevel("Store");
        }

        public void OptionsButtonClicked()
        {
            Application.LoadLevel("Options");
        }

        public void QuitButtonClicked()
        {
            Application.Quit();
        }
    }
}