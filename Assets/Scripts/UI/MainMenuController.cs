using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    {
        public void PlayButtonClicked()
        {
            audio.Play();
            Application.LoadLevel("Store");
        }

        public void OptionsButtonClicked()
        {
            audio.Play();
        }

        public void QuitButtonClicked()
        {
            audio.Play();
            Application.Quit();
        }
    }
}