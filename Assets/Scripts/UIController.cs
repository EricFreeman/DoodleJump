using Assets.Scripts.UI;
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
                _displayedScreen = true;
                SaveGame();
                ShowPanel();
            }
        }

        private void ShowPanel()
        {
            var panel = (GameObject)Instantiate(GameOverPanel);
            panel.transform.SetParent(transform, false);
            panel.GetComponent<GameOver>().Director = Director;
        }

        private void SaveGame()
        {
            // TODO: Save the game here
        }
    }
}