using System;
using System.Linq;
using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Upgrades;
using Assets.Scripts.Util;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class Platform : MonoBehaviour
    {
        public float Boost;
        public float Money;
        public GameObject Coin;
        public PlatformType Type;

        private bool _moveDir;

        void Update()
        {
            if (Type == PlatformType.MovingHorizontal)
            {
                if (transform.position.x < -4) _moveDir = true;
                if (transform.position.x > 4) _moveDir = false;

                transform.Translate(_moveDir ? .05f : -.05f, 0, 0);
            }
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

                // spawn particle effects
                Enumerable.Range(0, 10).Each(x =>
                {
                    var c = (GameObject)Instantiate(Coin);
                    c.transform.position = transform.position - new Vector3(UnityEngine.Random.Range(-1, 1), 0, 0);
                    c.rigidbody.AddForce(UnityEngine.Random.Range(-150, 150), UnityEngine.Random.Range(200, 350), 0);
                });

                // destroy the platform
                Destroy(gameObject);
            }
        }

        public void Setup(PlatformType? type = null)
        {
            Type = type == null ? GetRandomType() : type.Value;

            switch (Type)
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
                case PlatformType.MovingHorizontal:
                    gameObject.renderer.material.color = Color.green;
                    _moveDir = UnityEngine.Random.Range(0, 1) == 1;
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
        QuadrupleMoney,
        MovingHorizontal
    }
}