using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public AudioClip PlayerHitSound;
        public float Money = 5;
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
                EventAggregator.SendMessage(new EarnMoneyMessage { Money = Money });
                EventAggregator.SendMessage(new PlaySoundMessage { Clip = PlayerHitSound });
            }
        }
    }
}