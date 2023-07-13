using System;
using Lean.Touch;
using UnityEngine;

namespace DefaultNamespace
{
    public class ScaleParameters : MonoBehaviour
    {
        [SerializeField] private LeanPinchScale _leanPinchScale;
        [SerializeField, Range(0.5f, 1.2f)] private float _minScale;
        [SerializeField, Range(1.8f, 3f)] private float _maxScale;

        private void Start()
        {
            _leanPinchScale.MinScale = _minScale;
            _leanPinchScale.MaxScale = _maxScale;
        }
    }
}