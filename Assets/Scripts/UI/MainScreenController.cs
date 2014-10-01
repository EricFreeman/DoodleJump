using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MainScreenController : MonoBehaviour,
        IListener<ShowPanelMessage>
    {
        public GameObject MainMenu;
        public GameObject OptionsMenu;

        void Start()
        {
            this.Register<ShowPanelMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<ShowPanelMessage>();
        }

        public void Handle(ShowPanelMessage message)
        {
            MainMenu.SetActive(message.Type == PanelType.MainMenu);
            OptionsMenu.SetActive(message.Type == PanelType.Options);
        }
    }
}