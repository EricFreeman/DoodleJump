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

        void Start()
        {
            var upgrades = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.BaseType == typeof (Upgrade));

            foreach (var u in upgrades)
            {
                var prefab = (GameObject)Instantiate(UpgradePanelPrefab);
                prefab.transform.SetParent(transform, false);
                prefab.GetComponent<StoreUpgrade>().Setup((Upgrade)Activator.CreateInstance(u));
            }
        }
    }
}
