using UnityEngine;

namespace Code.MonoBehaviours
{
    class CameraSingleton : MonoBehaviour
    {
        public static Camera Instance;

        void Awake()
        {
            Instance = GetComponent<Camera>();
        }
    }
}
