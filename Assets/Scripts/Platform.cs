using System;
using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Upgrades;
using UnityEngine;
using Random = System.Random;

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
                var boostLevel = PlayerContext.Get(UpgradeType.Jump);
                col.collider.rigidbody.velocity = new Vector3(col.collider.rigidbody.velocity.x, Boost + boostLevel, 0);

                // send message to give player money
                EventAggregator.SendMessage(new PlatformHitMessage { Money = Money });

                // destroy the platform
                Destroy(gameObject);
            }
        }

        public void Setup(PlatformType? type = null)
        {
            var realType = type == null ? GetRandomType() : type.Value;

            switch (realType)
            {
                case PlatformType.Deafult:
                    gameObject.renderer.material.color = Color.white;
                    break;
                case PlatformType.HighBounce:
                    Boost *= 1.33f;
                    gameObject.renderer.material.color = Color.blue;
                    break;
                case PlatformType.LowBounce:
                    Boost /= 2;
                    gameObject.renderer.material.color = Color.red;
                    break;
                case PlatformType.QuadrupleMoney:
                    Money *= 4;
                    gameObject.renderer.material.color = Color.yellow;
                    break;
            }
        }

        private PlatformType GetRandomType()
        {
            var values = Enum.GetValues(typeof(PlatformType));
            var random = new Random();
            return (PlatformType)values.GetValue(random.Next(values.Length));
        }
    }

    public enum PlatformType
    {
        Deafult,
        HighBounce,
        LowBounce,
        QuadrupleMoney
    }
}