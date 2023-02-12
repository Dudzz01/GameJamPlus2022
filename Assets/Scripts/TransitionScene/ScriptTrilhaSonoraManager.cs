using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public  class ScriptTrilhaSonoraManager : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame

    public static GameObject somObject;
    protected void Awake()
    {
        DontDestroyOnLoad(this.transform.root.gameObject);

        if(somObject == null)
        {
            somObject = this.transform.root.gameObject;
        }
        else
        {
            Destroy(this.transform.root.gameObject);
        }
    }

    public virtual void DestroySoundObject()
    {
        //Destruir sound Object
    }


}
