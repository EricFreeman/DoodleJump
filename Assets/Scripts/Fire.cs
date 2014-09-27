using UnityEngine;

namespace Assets.Scripts
{
    public class Fire : MonoBehaviour
    {
        private float _scale = 1f;

        void Start()
        {
//            rigidbody.AddForce(0, -100, 0);
        }

        void Update()
        {
            _scale -= .05f;
            transform.localScale = new Vector3(_scale / 2, _scale / 2, _scale / 2);

            if(_scale <= 0) Destroy(gameObject);
        }
    }
}