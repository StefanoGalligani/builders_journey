using UnityEngine;

namespace BuilderGame.Cam
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField]private Transform _camTransform;
        [SerializeField] private float _magnitude;
        [SerializeField] private float _width;

        private void Update() {
            Vector3 newPosition = _camTransform.position - _camTransform.position/_magnitude;
            float shifts = _camTransform.position.x / (_magnitude*_width);
            newPosition.x += _width*Mathf.Floor(shifts);
            newPosition.y += _camTransform.position.y/_magnitude/2;
            newPosition.z = 0;
            transform.position = newPosition;
        }
    }
}
