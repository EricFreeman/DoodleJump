using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameOver : MonoBehaviour
    {
        public Director Director;
        public Text LevelMoneyText;
        public Text TotalMoneyText;
        public Text TimeText;

        void Start()
        {
            // Save game
            var player = PlayerManager.Load();
            player.Money += Director.LevelMoney;
            PlayerManager.Save(player);

            LevelMoneyText.text = "Level: {0}".ToFormat(Director.LevelMoney.ToString("C"));
            TotalMoneyText.text = "Total: {0}".ToFormat(player.Money.ToString("C"));
            TimeText.text = "Time: {0} Seconds".ToFormat(Director.ElapsedTime.ToString("N2"));
        }

        public void Replay()
        {
            Application.LoadLevel("Game");
        }

        public void Store()
        {
            Application.LoadLevel("Store");
        }
    }
}