using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public float Speed = .1f;

        void Update()
        {
            var speed = Input.GetAxisRaw("Horizontal") * Speed;
//            transform.Translate(speed, 0, 0);
            rigidbody.AddForce(speed, 0, 0);

            if(transform.position.x < -6) transform.Translate(-transform.position.x + 6, 0, 0);
            if(transform.position.x > 6) transform.Translate(-transform.position.x - 6, 0, 0);
        }
    }
}