using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Director : MonoBehaviour
    {
        public GameObject Platform;
        public GameObject Player;

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
    }
}