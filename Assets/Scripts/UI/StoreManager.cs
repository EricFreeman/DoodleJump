using System;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using Assets.Scripts.Models.Upgrades;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class StoreManager : MonoBehaviour, IListener<BuyItemMessage>
    {
        public GameObject UpgradePanelPrefab;
        public GameObject UpgradeItemList;
        public Text MoneyText;

        void Start()
        {
            // find upgrades
            var upgrades = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.BaseType == typeof (Upgrade));
            var index = 0;

            // add upgrades to store
            foreach (var u in upgrades)
            {
                var prefab = (GameObject) Instantiate(UpgradePanelPrefab);
                prefab.transform.SetParent(UpgradeItemList.transform, false);
                prefab.GetComponent<StoreUpgrade>().Setup((Upgrade) Activator.CreateInstance(u));
                prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -index*100);
                index++;
            }

            // resize upgrade content panel so scorlling works correctly
            var r = UpgradeItemList.GetComponent<RectTransform>();
            r.sizeDelta = new Vector2(0, index * 100);
            r.rect.Set(0, 0, r.rect.width, r.rect.height);

            // Load initial player money text
            var player = PlayerManager.Load();
            MoneyText.text = "Money: {0:C}".ToFormat(player.Money);

            this.Register<BuyItemMessage>();
        }

        void OnDestroy()
        {
            this.UnRegister<BuyItemMessage>();
        }

        public void Play()
        {
            Application.LoadLevel("Game");
        }

        public void Back()
        {
            Application.LoadLevel("MainMenu");
        }

        public void Reset()
        {
            PlayerManager.Reset();
            var newPlayer = PlayerManager.Load();
            EventAggregator.SendMessage(new BuyItemMessage() { Player = newPlayer });
        }

        public void Handle(BuyItemMessage message)
        {
            MoneyText.text = "Money: {0:C}".ToFormat(message.Player.Money);
        }
    }
}
