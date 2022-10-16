using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSpawnPoints : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spawnPointCurrent;
    
    private void Update() {
        SpawnPointAtual();
    }
    public GameObject SpawnPointAtual()
    {
        return spawnPointCurrent;
    }
}
