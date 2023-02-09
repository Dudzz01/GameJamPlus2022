using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectorLevelW3 : SelectorManageAbstract, ISelectorLevelManager
{
    public void SelectLevel1()
    {
        if(GameController.s.arrayFasesDesbloqueadas[10] == true)
        {
            SceneManager.LoadScene("Fase 1 W3");
        }
    }

    public void SelectLevel2()
    {
        if(GameController.s.arrayFasesDesbloqueadas[11] == true)
        {
            SceneManager.LoadScene("Fase 2 W3");
        } 
    }

    public void SelectLevel3()
    {
        if(GameController.s.arrayFasesDesbloqueadas[12] == true)
        {
            SceneManager.LoadScene("Fase 3 W3");
        }
    }

    public void SelectLevel4()
    {
        if(GameController.s.arrayFasesDesbloqueadas[13] == true)
        {
            SceneManager.LoadScene("Fase 4 W3");
        }
    }

    public void SelectLevel5()
    {
        if(GameController.s.arrayFasesDesbloqueadas[14] == true)
        {
            SceneManager.LoadScene("Fase 5 W3");
        }
    }
}
