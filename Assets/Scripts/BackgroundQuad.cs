using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeatEater
{
    public class BackgroundQuad : MonoBehaviour
    {
        [SerializeField] private Camera camera;

        private Vector3 _camPos;

        private void LateUpdate()
        {
            var depth = camera.farClipPlane - float.Epsilon;
            // Screens coordinate corner location
            var zero = new Vector3(0, 0, depth);
            var max = new Vector3(Screen.width, Screen.height, depth);

            //Corner locations in world coordinates
            var lowerLeft = camera.ScreenToWorldPoint(zero);
            var upperRight = camera.ScreenToWorldPoint(max);


            var scale = new Vector3(
                (upperRight.x - lowerLeft.x),
                (upperRight.y - lowerLeft.y),
                1f
            );


            var pos = new Vector3(
                lowerLeft.x + ((upperRight.x - lowerLeft.x) / 2),
                lowerLeft.y + ((upperRight.y - lowerLeft.y) / 2),
                depth
            );

            transform.localPosition = pos;
            transform.localScale = scale;
        }
    }
}