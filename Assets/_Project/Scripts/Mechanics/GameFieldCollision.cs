using System;
using UnityEngine;

namespace _Project.Scripts.Mechanics
{
    public class GameFieldCollision : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;

        public Collider2D Collider => _collider;
    }
}