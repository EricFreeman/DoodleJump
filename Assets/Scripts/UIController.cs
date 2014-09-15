using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIController : MonoBehaviour
    {
        public Director Director;
        public Text Money;
        public GameObject GameOverPanel;

        private bool _displayedScreen;

        void Update()
        {
            Money.text = "Money: {0}".ToFormat(Director.LevelMoney.ToString("C"));

            if (Director.IsDead && !_displayedScreen)
            {
                SaveGame();
                var panel = (GameObject) Instantiate(GameOverPanel);
                panel.transform.SetParent(transform, false);
                _displayedScreen = true;
            }
        }

        private void SaveGame()
        {
            // TODO: Save the game here
        }
    }
}