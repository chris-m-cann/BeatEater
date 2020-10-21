using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Util;
using Util.Events;

namespace BeatEater
{
    public class MouthCollider: MonoBehaviour
    {
        [SerializeField] private float colliderWidthFactor = 1f;
        [SerializeField] private float colliderHeightFactor = 1f;
        [SerializeField] private float colliderDepth = .1f;
        [SerializeField] private float minOpenHeight = 0.015f;
        [SerializeField] private float biteTime = 1f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private GameObject visualiser;
        [SerializeField] private ColliderUnityEvent onBiteCollision;

        private ARFaceManager _faceManager;
        private bool bitting = false;
        private bool showBitting = false;
        private bool mouthOpen = false;
        private Vector3 biteCenter = Vector3.zero;
        private Vector3 biteSize = Vector3.zero;
        private Quaternion biteRotation;

        private const int TOP_LIP_VERT = 11;
        private const int BOTTOM_LIP_VERT = 16;
        private const int LEFT_OF_LIP_VERT = 78;
        private const int RIGHT_OF_LIP_VERT = 308;

        private void Awake()
        {
            _faceManager = GetComponent<ARFaceManager>();
        }

        private void Update()
        {
            var facemesh = new XRFaceMesh();
            var e = _faceManager.trackables.GetEnumerator();
            e.MoveNext();
            if (e.Current == null) return;

            _faceManager.subsystem.GetFaceMesh(
                e.Current.trackableId, Allocator.TempJob, ref facemesh
            );
            var top = e.Current.transform.TransformPoint(facemesh.vertices[TOP_LIP_VERT]);
            var bottom = e.Current.transform.TransformPoint(facemesh.vertices[BOTTOM_LIP_VERT]);

            var left = e.Current.transform.TransformPoint(facemesh.vertices[LEFT_OF_LIP_VERT]);
            var right = e.Current.transform.TransformPoint(facemesh.vertices[RIGHT_OF_LIP_VERT]);

            var rtl = (right - left);
            var ttb = (top - bottom);

            var width = rtl.magnitude;
            var height = ttb.magnitude;

            var mid = left + (rtl.normalized * (width / 2));


            var mouthWasOpen = mouthOpen;
            mouthOpen = (height > minOpenHeight);

            if (mouthWasOpen && !mouthOpen)
            {
                Bite(mid, e.Current.transform.rotation, new Vector3(width * colliderWidthFactor, -height * colliderHeightFactor, colliderDepth));
            }

            if (visualiser != null)
            {
                var vt = visualiser.transform;
                vt.position = biteCenter;
                vt.rotation = biteRotation;
                vt.localScale = biteSize;
                visualiser.SetActive(showBitting);
            }
        }

        private void FixedUpdate()
        {
            if (bitting)
            {
                var hits = Physics.OverlapBox(biteCenter, biteSize / 2, biteRotation, layerMask);
                foreach (var hit in hits)
                {
                    onBiteCollision.Invoke(hit);
                }

                bitting = false;
            }
        }

        private void Bite(Vector3 center, Quaternion rotation, Vector3 size)
        {
            biteCenter = center;
            biteRotation = rotation;
            biteSize = size;
            bitting = true;
            showBitting = true;
            StopAllCoroutines();
            this.ExecuteAfter(biteTime, () => showBitting = false);
        }
    }
}

/*
logic of a face collisions:
    - if it gets bitten we score, else go through the face??
    - if it hits the face then break
    - if it hits the mouth and open go through
    - if it hits the mouth and closed then break
    - if it it gets bitten then

*/