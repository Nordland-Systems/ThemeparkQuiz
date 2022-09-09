using System;
using UnityEngine;

namespace ThemeparkQuiz
{
    public class GyroManager : MonoBehaviour
    {
        #region Instance
        private static GyroManager instance;
        public static GyroManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GyroManager>();
                    if (instance == null)
                    {
                        instance =
                            new GameObject("Spawned GyroManager", typeof(GyroManager)).GetComponent<GyroManager>();
                    }
                }

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        #endregion

        [Header("Logic")] private Gyroscope gyro;
        [SerializeField] private Quaternion baseRotation = new Quaternion(0, 0, 1, 0);
        private float rotation;
        private bool gyroActive;

        public float Rotation => rotation;

        public void EnableGyro()
        {
            //Already activated
            if(gyroActive)
                return;

            if (SystemInfo.supportsGyroscope)
            {
                gyro = Input.gyro;
                gyro.enabled = true;
                gyroActive = gyro.enabled;
            }
        }

        private void Update()
        {
            if (gyroActive)
            {
                rotation = GetAngleByDeviceAxis();
                //GetAngleByDeviceAxis(Vector3.up);
                //float roll = rotationZ.eulerAngles.z;

                //rotation = gyro.attitude;
            }
        }
        
        /// <summary>
        /// Returns the rotation angle of given device axis. Use Vector3.right to obtain pitch, Vector3.up for yaw and Vector3.forward for roll.
        /// This is for landscape mode. Up vector is the wide side of the phone and forward vector is where the back camera points to.
        /// </summary>
        /// <returns>A scalar value, representing the rotation amount around specified axis.</returns>
        /// <param name="axis">Should be either Vector3.right, Vector3.up or Vector3.forward. Won't work for anything else.</param>
        float GetAngleByDeviceAxis()
        {
            Quaternion currentRotation = gyro.attitude;
            // Eliminate the XZ planes whilst maintaining Y
            Quaternion eliminationOfXZ = Quaternion.Inverse(Quaternion.FromToRotation(baseRotation * Vector3.up, currentRotation * Vector3.up));

            Quaternion yDifference = eliminationOfXZ * currentRotation;
            return yDifference.eulerAngles.y;
            
            /*Quaternion deviceRotation = gyro.attitude;
            Quaternion eliminationOfOthers = Quaternion.Inverse(
                Quaternion.FromToRotation(axis, deviceRotation * axis)
            );
            Vector3 filteredEuler = (eliminationOfOthers * deviceRotation).eulerAngles;

            float result = filteredEuler.z;
            if (axis == Vector3.up) {
                result = filteredEuler.y;
            }
            if (axis == Vector3.right) {
                // incorporate different euler representations.
                result = (filteredEuler.y > 90 && filteredEuler.y < 270) ? 180 - filteredEuler.x : filteredEuler.x;
            }
            return result;*/
        }
    }
}