using System;
using UnityEngine;

namespace Util
{
    public class ResetTransform : MonoBehaviour
    {
        private Vector3 _pos;
        private Quaternion _rot;
        private Vector3 _scale;

        private void Start()
        {
            _pos = transform.localPosition;
            _rot = transform.localRotation;
            _scale = transform.localScale;
        }

        public void PerformReset()
        {
            transform.localPosition = _pos;
            transform.localRotation = _rot;
            transform.localScale = _scale;
        }
    }
}