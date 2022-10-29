using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScriptTrilhaSonora : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(this.transform.root.gameObject);
        DestroyTrilhaSonora();
        
    }

    public void DestroyTrilhaSonora()
    {
        if(SceneManager.GetActiveScene().name == "FinalFeliz" || SceneManager.GetActiveScene().name == "FinalTriste")
        {
            Destroy(this.gameObject);
            
        }
    }


}
