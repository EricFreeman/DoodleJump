using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Director : MonoBehaviour
    {
        public GameObject Platform;
        public GameObject Player;

        public int MaxNextPlatform = 4;
        public int MinNextPlatform = 0;

        private float _nextSpawn = 0;
        private float _lastSpawn = 8;

        void Start()
        {
            // Add a couple platforms in to start

            AddPlatform(-12);
            AddPlatform(-8);
            AddPlatform(-4);
            AddPlatform(4);
            AddPlatform(8);

            _nextSpawn = Random.Range(MinNextPlatform, MaxNextPlatform);
        }

        void Update()
        {
            if (Math.Abs(Player.transform.position.y - (_lastSpawn + _nextSpawn)) < .1)
            {
                AddPlatform();
                _nextSpawn = Random.Range(MinNextPlatform, MaxNextPlatform);
            }
        }

        private void AddPlatform(float? y = null)
        {
            if (y == null) y = Player.transform.position.y + 10;

            var plat = (GameObject)Instantiate(Platform);
            plat.transform.Translate(Random.Range(-4, 4), y.Value, 0);
        }
    }
}