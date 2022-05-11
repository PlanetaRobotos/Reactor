using _Project.Scripts.Architecture.Services.Pool;
using UnityEngine;

public interface IPoolService
{
    void FillPool(PoolInfo info);
    bool IsReady();
    void ClearPool(string nameType);
    void AddToPool(GameObject obj, string nameType, string nameContainer);
    GameObject GetPoolObject(string nameType);
    void ReturnToPool(GameObject obj, string nameType);
    void CleanUp();
}