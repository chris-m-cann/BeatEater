using System;
using UnityEngine;

namespace Util
{
    [RequireComponent(typeof(Rigidbody))]
    public class SetInitialVelocity : MonoBehaviour
    {
        [SerializeField] private bool forwardOnly;
        [SerializeField] private float forwardSpeed;
        [SerializeField] private Vector3 velocity;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _rigidbody.velocity = forwardOnly ? transform.forward * forwardSpeed : velocity;
        }
    }
}