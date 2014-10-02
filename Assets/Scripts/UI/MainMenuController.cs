using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    {
        public AudioClip Click;

        public void PlayButtonClicked()
        {
            EventAggregator.SendMessage(new PlaySoundMessage { Clip = Click });
            Application.LoadLevel("Store");
        }

        public void OptionsButtonClicked()
        {
            EventAggregator.SendMessage(new PlaySoundMessage { Clip = Click });
            EventAggregator.SendMessage(new ShowPanelMessage { Type = PanelType.Options });
        }

        public void ControlsButtonClicked()
        {
            EventAggregator.SendMessage(new PlaySoundMessage { Clip = Click });
            EventAggregator.SendMessage(new ShowPanelMessage { Type = PanelType.Controls });
        }

        public void CreditsButtonClicked()
        {
            EventAggregator.SendMessage(new PlaySoundMessage { Clip = Click });
            EventAggregator.SendMessage(new ShowPanelMessage { Type = PanelType.Credits });
        }

        public void QuitButtonClicked()
        {
            EventAggregator.SendMessage(new PlaySoundMessage { Clip = Click });
            Application.Quit();
        }
    }
}