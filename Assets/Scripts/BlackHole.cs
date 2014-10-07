using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class BlackHole : MonoBehaviour
    {
        private Player _player;
        public float Speed;
        public float Distance;

        void Update()
        {
            if (_player == null) _player = FindObjectOfType<Player>();
            else
            {
                if(Math.Abs(Vector3.Distance(_player.transform.position, transform.position)) < Distance)
                    _player.transform.position = Vector3.MoveTowards(
                        _player.transform.position,
                        transform.position,
                        Speed*Time.deltaTime);
            }
        }
    }
}