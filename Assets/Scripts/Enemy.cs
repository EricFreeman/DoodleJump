using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Player")
            {
                EventAggregator.SendMessage(new HitPlayerMessage());
                Destroy(gameObject);
            }
        }
    }
}