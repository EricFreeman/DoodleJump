using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIController : MonoBehaviour
    {
        public Director Director;
        public Text Money;

        void Update()
        {
            Money.text = "Money: {0}".ToFormat(Director.LevelMoney.ToString("C"));
        }
    }
}