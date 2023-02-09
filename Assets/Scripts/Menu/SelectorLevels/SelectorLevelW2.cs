using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectorLevelW2 : SelectorManageAbstract, ISelectorLevelManager
{
    public void SelectLevel1()
    {
        if(GameController.s.arrayFasesDesbloqueadas[5] == true)
        {
            SceneManager.LoadScene("Fase 1 W2");
        }
    }

    public void SelectLevel2()
    {
        if(GameController.s.arrayFasesDesbloqueadas[6] == true)
        {
            SceneManager.LoadScene("Fase 2 W2");
        } 
    }

    public void SelectLevel3()
    {
        if(GameController.s.arrayFasesDesbloqueadas[7] == true)
        {
            SceneManager.LoadScene("Fase 3 W2");
        }
    }

    public void SelectLevel4()
    {
        if(GameController.s.arrayFasesDesbloqueadas[8] == true)
        {
            SceneManager.LoadScene("Fase 4 W2");
        }
    }

    public void SelectLevel5()
    {
        if(GameController.s.arrayFasesDesbloqueadas[9] == true)
        {
            SceneManager.LoadScene("Fase 5 W2");
        }
    }
}
