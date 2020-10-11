using System;
using UnityEngine;

namespace BeatEater
{
    [RequireComponent(typeof(Camera))]
    public class AlignViewWithArCamera : MonoBehaviour
    {
        [SerializeField] private Camera arCamera;

        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            _camera.projectionMatrix = arCamera.projectionMatrix;
        }
    }
}