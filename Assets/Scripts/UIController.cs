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

        private Player _player;
        public Text ParachutesText;
        public Text RocketsText;
        public Text LivesText;

        private bool _displayedScreen;

        void Update()
        {
            Money.text = "Money: {0}".ToFormat(Director.LevelMoney.ToString("C"));

            if (Director.IsDead && !_displayedScreen)
            {
                _displayedScreen = true;
                ShowPanel();
            }

            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                ParachutesText.transform.parent.gameObject.SetActive(_player.RemainingParachutes > 0);
                RocketsText.transform.parent.gameObject.SetActive(_player.RemainingRockets > 0);
            }
            else
            {
                ParachutesText.text = "x {0}".ToFormat(_player.RemainingParachutes);
                RocketsText.text = "x {0}".ToFormat(_player.RemainingRockets);
                LivesText.text = "x {0}".ToFormat(_player.Health);
            }
        }

        private void ShowPanel()
        {
            var panel = (GameObject)Instantiate(GameOverPanel);
            panel.transform.SetParent(transform, false);
            panel.GetComponent<GameOver>().Director = Director;
        }
    }
}