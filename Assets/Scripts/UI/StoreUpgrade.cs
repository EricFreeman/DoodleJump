using System.Linq;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Upgrades;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class StoreUpgrade : MonoBehaviour
    {
        public Text UpgradeNameText;
        public Text PriceText;
        public Upgrade Upgrade;

        public Button BuyButton;

        public Transform UpgradeAmountPanel;
        public GameObject LevelPill;
        public Sprite Bought;
        public Sprite Unbought;

        private PlayerModel _player;
        private int _upgradeLevel;

        public void Setup(Upgrade u)
        {
            Upgrade = u;
            UpdateItem();
        }

        private void UpdateItem()
        {
            _player = PlayerManager.Load();
            _upgradeLevel = _player.GetLevel(Upgrade);
            UpgradeNameText.text = Upgrade.Name;
            PriceText.text = Upgrade.LevelPrice(_upgradeLevel).ToString("C");
            BuyButton.gameObject.SetActive(_player.Money >= Upgrade.LevelPrice(_upgradeLevel));

            // destroy existing pill level markers
            foreach (Transform t in UpgradeAmountPanel)
                Destroy(t.gameObject);

            // add new pill level markers
            Enumerable.Range(0, Upgrade.MaxLevel).Each(x =>
            {
                var pill = (GameObject)Instantiate(LevelPill);
                pill.GetComponent<Image>().sprite = x < _upgradeLevel ? Bought : Unbought;
                pill.transform.SetParent(UpgradeAmountPanel, false);
            });
        }

        public void Buy()
        {
            var price = Upgrade.LevelPrice(_player.GetLevel(Upgrade));

            // if you can afford upgrade
            if (_player.Money >= price && Upgrade.MaxLevel > _upgradeLevel)
            {
                _player.Money -= price;
                _upgradeLevel++;
                _player.SetLevel(Upgrade, _upgradeLevel);
                PlayerManager.Save(_player);
                UpdateItem();
            }
        }
    }

    public static class PlayerExtensions
    {
        public static int GetLevel(this PlayerModel p, Upgrade u)
        {
            var uplevel = p.UpgradeLevels.FirstOrDefault(x => x.Upgrade == u);
            return uplevel == null ? 0 : uplevel.Level;
        }

        public static void SetLevel(this PlayerModel p, Upgrade u, int l)
        {
            var uplevel = p.UpgradeLevels.FirstOrDefault(x => x.Upgrade == u);
            if (uplevel != null) uplevel.Level = l;
            else p.UpgradeLevels.Add(new UpgradeLevel(u, l));
        }
    }
}