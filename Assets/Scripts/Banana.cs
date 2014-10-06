using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using UnityEngine;

namespace Assets.Scripts
{
    public class Banana : MonoBehaviour
    {
        public float Money;
        public AudioClip Sound;

        private bool _isDeleted;

        // Update is called once per frame
        private void Update()
        {
            transform.Rotate(0, 0, 3);
        }

        private void OnTriggerEnter(Collider col)
        {
            if (!_isDeleted)
            {
                EventAggregator.SendMessage(new EarnMoneyMessage {Money = Money});
                EventAggregator.SendMessage(new PlaySoundMessage { Clip = Sound });
                Destroy(gameObject);
                _isDeleted = true;
            }
        }
    }
}