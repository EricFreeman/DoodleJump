using UnityEngine;

namespace Assets.Scripts
{
    public class Spawnable : MonoBehaviour
    {
        public GameObject ToSpawn;
        private GameObject _player;

        public float MaxNextSpawn;
        public float MinNextSpawn;
        public float SpawnAfter;

        private float _nextSpawn;

        void Start () 
        {
            _nextSpawn = Random.Range(MinNextSpawn, MaxNextSpawn) + SpawnAfter;
        }

        void Update ()
        {
            if (_player == null) _player = GameObject.FindGameObjectWithTag("Player");
            else
            {
                if (_player.transform.position.y > _nextSpawn)
                {
                    var spawn = (GameObject)Instantiate(ToSpawn);
                    spawn.transform.Translate(Random.Range(-4, 4), _nextSpawn + 10, 0);
                    _nextSpawn += Random.Range(MinNextSpawn, MaxNextSpawn);
                }
            }
        }
    }
}