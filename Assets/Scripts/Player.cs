using Assets.Scripts.Events;
using Assets.Scripts.Events.Messages;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Upgrades;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour,
        IListener<HitPlayerMessage>
    {
        public float Speed = .1f;
        public float RocketLength = 1;

        public bool IsRocketFiring;
        public bool IsParachuteLaunched;
        public int RemainingRockets;
        public int RemainingParachutes;
        public int Health = 1;

        void Start()
        {
            Health += PlayerContext.Get(UpgradeType.Armor);
        }

        private void Update()
        {
            ApplyMovementSpeed();
            VerifyAndUpdatePlayerX();
        }

        private void ApplyMovementSpeed()
        {
            var speed = Input.GetAxisRaw("Horizontal")*Speed;
            rigidbody.AddForce(speed, 0, 0);

            var tiltSpeed = GetTiltSpeed();
            rigidbody.AddForce(tiltSpeed * Speed);
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

        private void VerifyAndUpdatePlayerX()
        {
            if (transform.position.x < -6) transform.Translate(-transform.position.x + 6, 0, 0);
            if (transform.position.x > 6) transform.Translate(-transform.position.x - 6, 0, 0);
        }

        public void Handle(HitPlayerMessage message)
        {
            Health--;
            if (Health <= 0)
            {
                EventAggregator.SendMessage(new PlayerDiedMessage());
                var feet = GameObject.FindGameObjectWithTag("PlayerFeet");
                if(feet != null) Destroy(feet);
            }
        }
    }
}