using UnityEngine;

namespace ThemeparkQuiz
{
    public class FollowGyro : MonoBehaviour
    {
        [Header("Tweaks")] 
        [SerializeField] private Quaternion baseRotation = new Quaternion(0, 0, 1, 0);

        private void Start()
        {
            GyroManager.Instance.EnableGyro();
        }

        private void Update()
        {
            transform.localRotation = new Quaternion(0, 0, GyroManager.Instance.Rotation, 0);
            Debug.Log(GyroManager.Instance.Rotation);
        }
    }
}