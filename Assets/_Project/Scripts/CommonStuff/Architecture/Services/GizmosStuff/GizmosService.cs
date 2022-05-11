using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Architecture.Services
{
    public class GizmosService : MonoBehaviour
    {
        private readonly List<IGizmos> _gizmos = new List<IGizmos>();
        
        public void OnDrawGizmos()
        {
            foreach (var gizmo in _gizmos) 
                gizmo.DrawGizmo();
        }

        public void AddGizmo(IGizmos gizmo) => _gizmos.Add(gizmo);
        public void RemoveGizmo(IGizmos gizmo) => _gizmos.Remove(gizmo);
    }

    public interface IGizmos
    {
        void DrawGizmo();
    }
}