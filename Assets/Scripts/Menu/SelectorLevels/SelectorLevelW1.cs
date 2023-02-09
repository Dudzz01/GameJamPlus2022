using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectorLevelW1 : SelectorManageAbstract, ISelectorLevelManager
{
    public void SelectLevel1()
    {
        if(GameController.s.arrayFasesDesbloqueadas[0] == true)
        {
            SceneManager.LoadScene("Fase 1 W1");
        }
    }

    public void SelectLevel2()
    {
        if(GameController.s.arrayFasesDesbloqueadas[1] == true)
        {
            SceneManager.LoadScene("Fase 2 W1");
        } 
    }

    public void SelectLevel3()
    {
        if(GameController.s.arrayFasesDesbloqueadas[2] == true)
        {
            SceneManager.LoadScene("Fase 3 W1");
        }
    }

    public void SelectLevel4()
    {
        if(GameController.s.arrayFasesDesbloqueadas[3] == true)
        {
            SceneManager.LoadScene("Fase 4 W1");
        }
    }

    public void SelectLevel5()
    {
        if(GameController.s.arrayFasesDesbloqueadas[4] == true)
        {
            SceneManager.LoadScene("Fase 5 W1");
        }
    }
}
