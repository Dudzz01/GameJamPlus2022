using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SelectorManageAbstract : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    public void BackToChooseWorld()
    {
        SceneManager.LoadScene("Escolher Mundos");
    }
}
