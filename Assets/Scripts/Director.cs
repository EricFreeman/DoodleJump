using System.Collections.Generic;
using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Upgrades;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class Director : MonoBehaviour,
        IListener<PlatformHitMessage>
    {
        public GameObject Platform;
        public GameObject Player;
        public GameObject Camera;

        public float LevelMoney;

        public float ElapsedTime
        {
            get { return Time.fixedTime - _startTime; }
        }
        public bool IsDead;

        public int MaxNextPlatform = 8;
        public int MinNextPlatform = 4;

        private float _nextSpawn;
        private float _startTime;

        public static List<UpgradeLevel> UpgradeLevels { get; set; } 

        void Start()
        {
            // Add a couple platforms in to start
            AddPlatform(-8, PlatformType.HighBounce);
            AddPlatform(-4);
            AddPlatform(4);
            AddPlatform(8);

            _nextSpawn = Random.Range(MinNextPlatform, MaxNextPlatform);
            _startTime = Time.fixedTime;

            this.Register<PlatformHitMessage>();

            var p = PlayerManager.Load();
            PlayerContext.Setup(p.UpgradeLevels);

            Player = (GameObject) Instantiate(Resources.Load("Prefabs/Player"));
        }

        void OnDestroy()
        {
            this.UnRegister<PlatformHitMessage>();
        }

        void Update()
        {
            if (Player.transform.position.y > _nextSpawn)
            {
                AddPlatform();
                _nextSpawn += Random.Range(MinNextPlatform, MaxNextPlatform);
            }

            IsDead = Camera.transform.position.y > Player.transform.position.y + 10;
        }

        private void AddPlatform(float? y = null, PlatformType? type = null)
        {
            if (y == null) y = _nextSpawn + 10;

            var plat = (GameObject)Instantiate(Platform);
            plat.transform.Translate(Random.Range(-4, 4), y.Value, 0);

            plat.GetComponent<Platform>().Setup(type);
        }

        public void Handle(PlatformHitMessage message)
        {
            var extraMoney = message.Money * PlayerContext.Get(UpgradeType.Money);
            LevelMoney += message.Money + extraMoney;
        }
    }
}