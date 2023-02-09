 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static Save s = new Save();

    
    private void Update()
    {
         DontDestroyOnLoad(this.transform.root.gameObject);   
    }
}
