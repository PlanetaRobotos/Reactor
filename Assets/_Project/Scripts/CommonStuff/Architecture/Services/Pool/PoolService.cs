using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Architecture.Services.Pool
{
    public class PoolService : IPoolService
    {
        public readonly List<PoolInfo> listOfPool = new List<PoolInfo>();

        private readonly DiContainer _diContainer;

        public PoolService(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void FillPool(PoolInfo info)
        {
            // var containerHolder = _diContainer.InstantiatePrefab(new GameObject());
            // info.container.transform.SetParent(containerHolder.transform);
            for (var i = 0; i < info.amount; i++)
            {
                var objInstance = _diContainer.InstantiatePrefab(info.prefab, info.container.transform);
                objInstance.SetActive(false);
                info.pool.Add(objInstance);
            }

            listOfPool.Add(info);
        }

        public bool IsReady()
        {
            if (listOfPool.Count > 0)
                return true;

            return false;
        }

        public void AddToPool(GameObject obj, string nameType, string containerName)
        {
            obj.SetActive(false);
            if (listOfPool.Count > 0)
                foreach (var info in listOfPool)
                    if (info.nameType == nameType)
                    {
                        info.pool.Add(obj);
                        if (info.container != null)
                            obj.transform.SetParent(info.container.transform);
                    }
                    else
                    {
                        CreateNewPoolInfo(obj, nameType, containerName);
                    }
            else
                CreateNewPoolInfo(obj, nameType, containerName);
        }

        public void CleanUp()
        {
            for (var i = 0; i < listOfPool.Count; i++)
            {
                for (var j = 0; j < listOfPool[i].pool.Count; j++)
                {
                    Object.Destroy(listOfPool[i].container);
                    Object.Destroy(listOfPool[i].prefab);
                    Object.Destroy(listOfPool[i].pool[j]);
                    listOfPool[i].pool.RemoveAt(j);
                }

                Object.Destroy(listOfPool[i].prefab);
                listOfPool.RemoveAt(i);
            }
        }

        public void ClearPool(string nameType)
        {
            for (var i = 0; i < listOfPool.Count; i++)
                if (listOfPool[i].nameType == nameType)
                {
                    Object.Destroy(listOfPool[i].prefab);
                    listOfPool.RemoveAt(i);
                }
        }

        public GameObject GetPoolObject(string nameType)
        {
            var selected = GetByType(nameType);
            var pool = selected.pool;
            GameObject objInstance;

            if (pool.Count > 0)
            {
                objInstance = pool[pool.Count - 1];
                pool.Remove(objInstance);
            }
            else
            {
                objInstance = _diContainer.InstantiatePrefab(selected.prefab, selected.container.transform);
                objInstance.SetActive(false);
            }
            return objInstance;
        }

        public void ReturnToPool(GameObject obj, string nameType)
        {
            obj.SetActive(false);
            var selected = GetByType(nameType);
            var pool = selected.pool;

            if (!pool.Contains(obj))
            {
                pool.Add(obj);
                // obj.transform.SetParent();
            }
        }

        private void CreateNewPoolInfo(GameObject obj, string nameType, string containerName)
        {
            var info = new PoolInfo(nameType, 1, obj, containerName);
            obj.transform.SetParent(info.container.transform);
            info.pool.Add(obj);
            listOfPool.Add(info);
        }

        private PoolInfo GetByType(string nameType)
        {
            for (var i = 0; i < listOfPool.Count; i++)
                if (nameType == listOfPool[i].nameType)
                    return listOfPool[i];

            return null;
        }
    }
}