﻿using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using UnityEngine;

namespace Assets.Scripts
{
    public class Platform : MonoBehaviour
    {
        public GameObject Camera;
        public float Boost;
        public float Money;

        void Update()
        {
            //remove platform if it gets too far below the camera
            if(transform.position.y < Camera.transform.position.y - 10)
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider col)
        {
            // platforms only work if you "fall" into them
            if (col.collider.rigidbody.velocity.y < 0)
            {
                // push player up
                col.collider.rigidbody.velocity = new Vector3(col.collider.rigidbody.velocity.x, Boost, 0);

                // send message to give player money
                EventAggregator.SendMessage(new PlatformHitMessage { Money = Money });

                // destroy the platform
                Destroy(gameObject);
            }
        }
    }
}