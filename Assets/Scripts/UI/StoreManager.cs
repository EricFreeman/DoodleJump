using Assets.Scripts.Models.Upgrades;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class StoreManager : MonoBehaviour
    {
        public GameObject UpgradePanelPrefab;

        void Start()
        {
            var prefab = (GameObject) Instantiate(UpgradePanelPrefab);
            prefab.transform.SetParent(transform, false);
            prefab.GetComponent<StoreUpgrade>().Setup(new JumpUpgrade());
        }
    }
}
