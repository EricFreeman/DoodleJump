using System;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Models.Upgrades;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class StoreManager : MonoBehaviour
    {
        public GameObject UpgradePanelPrefab;
        public GameObject UpgradeItemList;

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
        }
    }
}
