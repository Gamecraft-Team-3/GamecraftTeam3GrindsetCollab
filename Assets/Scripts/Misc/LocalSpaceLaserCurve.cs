using UnityEngine;

namespace Misc
{
    public class LocalSpaceLaserCurve : MonoBehaviour
    {
        private TrailRenderer _line;
        private int _linePositions;
        private int _keyCount;
        private int _keyIndex;
    
        /**
         * This does not work the way it is supposed to, but it looks okay and i am wasting time.
         */
        
        void Start()
        {
            _line = GetComponent<TrailRenderer>();
            _linePositions = _line.positionCount;
            _keyCount = _line.widthCurve.length;
        }

        void Update()
        {
            if (_line.positionCount > _linePositions)
            {
                AddCurveVertex();
            }
        }

        private void AddCurveVertex()
        {
            if (_keyIndex >= _keyCount)
                _keyIndex = 0;

            AnimationCurve curve = new AnimationCurve();

            Keyframe currentKey = _line.widthCurve.keys[_keyIndex];

            foreach (var keyframe in _line.widthCurve.keys)
            {
                Keyframe keyCopy = keyframe;
                keyCopy.time -= currentKey.time / _line.widthCurve.length;

                curve.AddKey(keyCopy);
            }

            Keyframe copyKey = currentKey;
            copyKey.time = curve.keys[^1].time + currentKey.time / _line.widthCurve.length;

            curve.AddKey(copyKey);

            _line.widthCurve = curve;
            
            _keyIndex++;

            _linePositions = _line.positionCount;
        }
    }
}
