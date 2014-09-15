using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using UnityEngine;

namespace Assets.Scripts
{
    public class Platform : MonoBehaviour
    {
        private void OnTriggerEnter(Collider col)
        {
            if (col.collider.rigidbody.velocity.y < 0)
            {
                col.collider.rigidbody.velocity = new Vector3(0, 20, 0);
                EventAggregator.SendMessage(new PlatformHitMessage { Money = .25f });
            }
        }
    }
}