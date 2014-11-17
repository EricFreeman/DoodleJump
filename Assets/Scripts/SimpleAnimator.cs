using UnityEngine;
using System.Linq;

namespace Assets.Scripts
{
    public class SimpleAnimator : MonoBehaviour
    {
        public Sprite[] Frames;
        private int _frame;
        public int SpriteSpeed;
        private int _currentSpriteSpeed;

        void Update()
        {
            if (_currentSpriteSpeed-- <= 0)
            {
                _currentSpriteSpeed = SpriteSpeed;
                if (_frame++ >= Frames.Count() - 1) _frame = 0;
                GetComponent<SpriteRenderer>().sprite = Frames[_frame];
            }
        }
    }
}