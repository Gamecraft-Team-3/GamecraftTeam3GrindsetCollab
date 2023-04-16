using System;
using UnityEngine;

namespace Misc
{
    public class LaserFalloff : MonoBehaviour
    {
        private float _length, _lengthTimer;
        private TrailRenderer _line;

        private void Start()
        {
            _line = GetComponent<TrailRenderer>();
            _length = _line.time;
        }

        private void Update()
        {
            if (_lengthTimer < 1.5f)
                _lengthTimer += Time.deltaTime / _length;
            else
                _lengthTimer = 1.5f;
            
            Color newAlpha = new Color(_line.startColor.r, _line.startColor.g, _line.startColor.b, (1.5f - _lengthTimer / 1.5f));
            _line.startColor = newAlpha;
            _line.endColor = newAlpha;
        }
    }
}
