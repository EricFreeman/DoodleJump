using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        private bool _isDead;

        void Update()
        {
            if(_isDead) DestroyImmediate(gameObject);
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Player" && !_isDead)
            {
                _isDead = true;
                EventAggregator.SendMessage(new HitPlayerMessage());
            }
        }
    }
}