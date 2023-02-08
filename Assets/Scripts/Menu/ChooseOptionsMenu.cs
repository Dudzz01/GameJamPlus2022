using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseOptionsMenu : MonoBehaviour
{
    
    public void SelectPlay()
    {
        if(GetComponent<SaveGame>().LoadGameOfScene() == null)
         {
             GameController.s.arrayFasesDesbloqueadas[0] = true;
             GameController.s.numeroDeFases = 0;
             GetComponent<SaveGame>().SaveGameOfScene(GameController.s);
         }
         else
         {
            GameController.s =  GetComponent<SaveGame>().LoadGameOfScene();
         }
        
        
         SceneManager.LoadScene("Escolher Mundos");
    }

    
}

