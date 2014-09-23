using Assets.Scripts.Models;
using Assets.Scripts.Models.Upgrades;
using UnityEngine;

namespace Assets.Scripts
{
    public class Rocket : MonoBehaviour
    {
        public Player Player;
        public int RemainingRockets;

        private float _rocketStart;

        void Start()
        {
            RemainingRockets = PlayerContext.Get(UpgradeType.Rocket);
        }

        void Update()
        {
            if (Player.IsParachuteLaunched) return;
            if (!Player.IsRocketFiring)
            {
                if (RemainingRockets <= 0) return;
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Player.IsRocketFiring = true;
                    _rocketStart = Time.fixedTime;
                    RemainingRockets--;
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