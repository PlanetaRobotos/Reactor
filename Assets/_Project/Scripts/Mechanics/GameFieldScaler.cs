using UnityEngine;

namespace _Project.Scripts.Mechanics
{
    public class GameFieldScaler : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float _widthPercent;

        public void Scale()
        {
            transform.localScale = Vector3.one * _widthPercent;
        }
    }
}