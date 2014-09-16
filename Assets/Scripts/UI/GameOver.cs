using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameOver : MonoBehaviour
    {
        public Director Director;
        public Text MoneyText;
        public Text TimeText;

        void Start()
        {
            MoneyText.text = "Money: {0}".ToFormat(Director.LevelMoney.ToString("C"));
            TimeText.text = "Time: {0} Seconds".ToFormat(Director.ElapsedTime.ToString("N2"));
        }

        public void Replay()
        {
            Application.LoadLevel("Game");
        }

        public void Store()
        {
            
        }
    }
}