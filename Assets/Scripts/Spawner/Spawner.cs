using System;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    // сделать в ресурс эксплоужен создание спаунера на сцене и передать туда
    // в качестве префаба предмет который должен спауниться
    
    [SerializeField] protected MonoBehaviour _prefab;
    protected ObjectPool<MonoBehaviour> _pool;

    protected override void  Awake()
    {
        base.Awake();
        CreatePool(_prefab);
    }
    public void CreatePool(MonoBehaviour prefab)
    {
        _pool = new ObjectPool<MonoBehaviour>(_prefab);
    }
    

    public MonoBehaviour GetObject()
    {
        return _pool.GetObject();
    }
}
