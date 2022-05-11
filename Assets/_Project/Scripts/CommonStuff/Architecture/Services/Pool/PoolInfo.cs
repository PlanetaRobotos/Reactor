using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Architecture.Services.Pool
{
    [System.Serializable]
    public class PoolInfo
    {
        public string nameType;
        public int amount;
        public GameObject prefab;
        public GameObject container;

        [HideInInspector] public List<GameObject> pool = new List<GameObject>();

        public PoolInfo(string nameType, int amount, GameObject prefab, string containerName)
        {
            this.nameType = nameType;
            this.amount = amount;
            this.prefab = prefab;
            GameObject findingContainer = GameObject.Find(containerName);
            if (!findingContainer)
                container = new GameObject(containerName);
            else
                container = findingContainer;
        }

        public static PoolInfo Create(string nameType, int amount, GameObject prefab, string containerName) => 
            new PoolInfo(nameType, amount, prefab, containerName);
    }
}