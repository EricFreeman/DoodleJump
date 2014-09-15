using UnityEngine;

namespace Assets.Scripts
{
    public class TrackObject : MonoBehaviour
    {
        public GameObject Object;
        public Vector3 Offset;

        void Update ()
        {
            if(Object.transform.position.y > transform.position.y)
                transform.position = new Vector3(0, Object.transform.position.y, 0) + Offset;
        }
    }
}