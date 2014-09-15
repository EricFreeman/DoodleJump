using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
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

        public int MaxNextPlatform = 8;
        public int MinNextPlatform = 4;

        private float _nextSpawn;

        void Start()
        {
            // Add a couple platforms in to start

            AddPlatform(-12);
            AddPlatform(-8);
            AddPlatform(-4);
            AddPlatform(4);
            AddPlatform(8);

            _nextSpawn = Random.Range(MinNextPlatform, MaxNextPlatform) + 8;

            this.Register<PlatformHitMessage>();
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
        }

        private void AddPlatform(float? y = null)
        {
            if (y == null) y = Player.transform.position.y + 10;

            var plat = (GameObject)Instantiate(Platform);
            plat.transform.Translate(Random.Range(-4, 4), y.Value, 0);
        }

        public void Handle(PlatformHitMessage message)
        {
            LevelMoney += message.Money;
            Debug.Log(LevelMoney);
        }
    }
}