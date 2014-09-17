using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using Assets.Scripts.Models;
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

        private List<UpgradeLevel> _upgradeLevels { get; set; } 

        void Start()
        {
            // Add a couple platforms in to start

            AddPlatform(-12);
            AddPlatform(-8);
            AddPlatform(-4);
            AddPlatform(4);
            AddPlatform(8);

            _nextSpawn = Random.Range(MinNextPlatform, MaxNextPlatform);
            _startTime = Time.fixedTime;

            this.Register<PlatformHitMessage>();

            var p = PlayerManager.Load();
            _upgradeLevels = p.UpgradeLevels;
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

        private void AddPlatform(float? y = null)
        {
            if (y == null) y = _nextSpawn + 10;

            var plat = (GameObject)Instantiate(Platform);
            plat.transform.Translate(Random.Range(-4, 4), y.Value, 0);
            plat.GetComponent<Platform>().Camera = Camera;
        }

        public void Handle(PlatformHitMessage message)
        {
            var investments = _upgradeLevels.FirstOrDefault(x => x.Upgrade.Name == "Investments");
            var extraMoney = investments != null ? message.Money*investments.Level : 0;

            LevelMoney += message.Money + extraMoney;
        }
    }
}