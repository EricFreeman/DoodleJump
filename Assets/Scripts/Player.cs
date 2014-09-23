using Assets.Scripts.Models;
using Assets.Scripts.Models.Upgrades;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public float Speed = .1f;
        public float RocketLength = 1;

        public int RemainingRockets;
        public int RemainingParachutes;

        private float _rocketStart;
        private bool _isRocketFiring;
        private float _parachuteStart;
        private bool _isParachuteLaunched;

        private void Start()
        {
            RemainingRockets = PlayerContext.Get(UpgradeType.Rocket);
            RemainingParachutes = PlayerContext.Get(UpgradeType.Parachute);
        }

        private void Update()
        {
            ApplyMovementSpeed();
            VerifyAndUpdatePlayerX();
            ManageRocket();
            ManageParachute();
        }

        private void ManageRocket()
        {
            if (_isParachuteLaunched) return;
            if (!_isRocketFiring)
            {
                if (RemainingRockets <= 0) return;
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _isRocketFiring = true;
                    _rocketStart = Time.fixedTime;
                    RemainingRockets--;
                }
            }
            else
            {
                var fire = (GameObject) Instantiate(Resources.Load("Prefabs/Fire"));
                fire.transform.position = transform.position;

                rigidbody.AddForce(0, 25, 0);
                if (Time.fixedTime > _rocketStart + RocketLength) _isRocketFiring = false;
            }
        }

        private void ManageParachute()
        {
            if (_isRocketFiring) return;
            if (!_isParachuteLaunched)
            {
                if (RemainingParachutes <= 0 || rigidbody.velocity.y > 0) return;
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _isParachuteLaunched = true;
                    _parachuteStart = Time.fixedTime;
                    RemainingParachutes--;

                    var parachute = (GameObject) Instantiate(Resources.Load("Prefabs/Parachute"));
                    parachute.transform.position = transform.position + new Vector3(0, 2, 0);
                    parachute.transform.SetParent(transform);
                }
            }
            else
            {
                if (rigidbody.velocity.y > 0)
                {
                    _isParachuteLaunched = false;
                    foreach (Transform t in transform)
                        if (t.name.Contains("Parachute")) Destroy(t.gameObject);
                }
                else
                    rigidbody.velocity = new Vector3(rigidbody.velocity.x, -.5f, 0);
            }
        }

        private void ApplyMovementSpeed()
        {
            var speed = Input.GetAxisRaw("Horizontal")*Speed;
            rigidbody.AddForce(speed, 0, 0);

            var tiltSpeed = GetTiltSpeed();
            rigidbody.AddForce(tiltSpeed * Speed);
        }

        private void VerifyAndUpdatePlayerX()
        {
            if (transform.position.x < -6) transform.Translate(-transform.position.x + 6, 0, 0);
            if (transform.position.x > 6) transform.Translate(-transform.position.x - 6, 0, 0);
        }

        private Vector3 GetTiltSpeed()
        {
            var dir = Vector3.zero;
            dir.x = -Input.acceleration.y;
            dir.z = Input.acceleration.x;
            if (dir.sqrMagnitude > 1)
                dir.Normalize();

            dir *= Time.deltaTime;

            return dir;
        }
    }
}