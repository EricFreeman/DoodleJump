using System;
using System.Linq;
using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Upgrades;
using Assets.Scripts.Util;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Director : MonoBehaviour,
        IListener<EarnMoneyMessage>,
        IListener<PlayerDiedMessage>
    {
        public GameObject Platform;
        public GameObject Player;

        public float LevelMoney;
        public float MaxHeight;
        public bool IsDead;
        public float ElapsedTime
        {
            get { return Time.fixedTime - _startTime; }
        }

        private float _startTime;

        void Start()
        {
            // Add a few platforms in to start
            Enumerable.Range(0, 5).Each(x => Enumerable.Range(0, 3).Each(n => AddPlatform(Random.Range(x * 6 - 15, x * 6 - 10))));

            _startTime = Time.fixedTime;

            this.Register<EarnMoneyMessage>();
            this.Register<PlayerDiedMessage>();

            var p = PlayerManager.Load();
            PlayerContext.Setup(p.UpgradeLevels);

            Player = (GameObject) Instantiate(Resources.Load("Prefabs/Player"));
        }

        void OnDestroy()
        {
            this.UnRegister<EarnMoneyMessage>();
            this.UnRegister<PlayerDiedMessage>();
        }

        void Update()
        {
            IsDead = IsDead || Camera.main.transform.position.y > Player.transform.position.y + WorldContext.OffScreenY;
            MaxHeight = MaxHeight > Player.transform.position.y ? MaxHeight : Player.transform.position.y;
        }

        private void AddPlatform(float y, PlatformType? type = null)
        {
            var plat = (GameObject)Instantiate(Platform);
            plat.transform.Translate(Random.Range(-WorldContext.OffScreenX, WorldContext.OffScreenX), y, 0);
        }

        public void Handle(EarnMoneyMessage message)
        {
            var extraMoney = message.Money * PlayerContext.Get(UpgradeType.Money);
            LevelMoney += message.Money + extraMoney;
        }

        public void Handle(PlayerDiedMessage message)
        {
            IsDead = true;
        }
    }
}