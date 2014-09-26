using System;
using System.IO.IsolatedStorage;
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

        public bool IsOverride;

        void Start()
        {
            if(!IsOverride)
                Setup();
        }

        private void OnTriggerEnter(Collider col)
        {
            // don't interact with a dead player
            if (col.tag != "PlayerFeet") return;

            var player = col.transform.parent;

            // platforms only work if you "fall" into them
            if (player.collider.rigidbody.velocity.y < 0)
            {
                // push player up
                var boostLevel = PlayerContext.Get(UpgradeType.Jump);
                player.collider.rigidbody.velocity = new Vector3(player.collider.rigidbody.velocity.x, Boost + boostLevel, 0);

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
                    gameObject.AddComponent<MoveBackAndForth>();
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