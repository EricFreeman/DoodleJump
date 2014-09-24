using Assets.Scripts.Models;
using Assets.Scripts.Models.Upgrades;
using UnityEngine;

namespace Assets.Scripts
{
    public class Rocket : MonoBehaviour
    {
        public Player Player;

        private float _rocketStart;

        void Start()
        {
            Player.RemainingRockets = PlayerContext.Get(UpgradeType.Rocket);
        }

        void Update()
        {
            if (Player.IsParachuteLaunched) return;
            if (!Player.IsRocketFiring)
            {
                if (Player.RemainingRockets <= 0) return;
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Player.IsRocketFiring = true;
                    _rocketStart = Time.fixedTime;
                    Player.RemainingRockets--;
                }
            }
            else
            {
                var fire = (GameObject)Instantiate(Resources.Load("Prefabs/Fire"));
                fire.transform.position = Player.transform.position;

                Player.rigidbody.AddForce(0, 25, 0);
                if (Time.fixedTime > _rocketStart + Player.RocketLength) Player.IsRocketFiring = false;
            }
        }
    }
}