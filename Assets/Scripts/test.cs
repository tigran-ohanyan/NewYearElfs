using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class test : MonoBehaviour
{
public int a;
    private ISave _save;
    [Inject]
    public void Construct(ISave _ISave){
        _save = _ISave;
        
    }
    public void Start()
    {
        testing();
    }
    public void testing()
    {
        _save.PlayerData.Health = a;
        Debug.Log(_save.PlayerData.Health);
    }
}
