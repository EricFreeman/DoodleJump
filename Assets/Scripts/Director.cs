using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Upgrades;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class Director : MonoBehaviour,
        IListener<PlatformHitMessage>,
        IListener<PlayerDiedMessage>
    {
        public GameObject Platform;
        public GameObject Player;
        public GameObject Camera;

        public float LevelMoney;
        public bool IsDead;
        public float ElapsedTime
        {
            get { return Time.fixedTime - _startTime; }
        }

        private float _startTime;

        void Start()
        {
            // Add a couple platforms in to start
            Platform.GetComponent<Platform>().IsOverride = true;
            AddPlatform(-8, PlatformType.HighBounce);
            AddPlatform(-4);
            AddPlatform(4);
            AddPlatform(8);

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
            IsDead = IsDead || Camera.transform.position.y > Player.transform.position.y + 10;
        }

        private void AddPlatform(float y, PlatformType? type = null)
        {
            var plat = (GameObject)Instantiate(Platform);
            plat.transform.Translate(Random.Range(-4, 4), y, 0);

            plat.GetComponent<Platform>().Setup(type);
        }

        public void Handle(PlatformHitMessage message)
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