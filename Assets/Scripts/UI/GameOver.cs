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
        public Text HeightText;
        public Text TotalLevelMoneyText;

        private float _totalLevelMoney;

        void Start()
        {
            // Save game
            _totalLevelMoney = CalculateTotalMoney();
            var player = PlayerManager.Load();
            player.Money += _totalLevelMoney;
            PlayerManager.Save(player);

            LevelMoneyText.text = "Level (Base): {0}".ToFormat(Director.LevelMoney.ToString("C"));
            TotalLevelMoneyText.text = "Level (Total): {0}".ToFormat(_totalLevelMoney.ToString("C"));
            TotalMoneyText.text = "Total: {0}".ToFormat(player.Money.ToString("C"));
            TimeText.text = "Time: {0} Seconds".ToFormat(Director.ElapsedTime.ToString("N2"));
            HeightText.text = "Height: {0} ft".ToFormat(Director.MaxHeight.ToString("N0"));
        }

        private float CalculateTotalMoney()
        {
            return Director.LevelMoney * Director.MaxHeight / 50 * Director.ElapsedTime / 60;
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