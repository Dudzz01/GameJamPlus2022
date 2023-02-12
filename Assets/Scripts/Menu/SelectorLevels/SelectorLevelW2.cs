using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SelectorLevelW2 : SelectorManageAbstract, ISelectorLevelManager
{

    [SerializeField]private Image[] spriteLevel = new Image[6];
    [SerializeField]private Sprite[] spriteBotoes = new Sprite[2];

    private void Update() {
        ConfigArtWorld1();
    }

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

    public void ConfigArtWorld1()
    {
        if(GameController.s.arrayFasesDesbloqueadas[5] == true)
        {
            spriteLevel[0].sprite = spriteBotoes[0];
        }
        else
        {
            spriteLevel[0].sprite = spriteBotoes[1];
        }

        if(GameController.s.arrayFasesDesbloqueadas[6] == true)
        {
            spriteLevel[1].sprite = spriteBotoes[0];
        }
        else
        {
            spriteLevel[1].sprite = spriteBotoes[1];
        }   

        if(GameController.s.arrayFasesDesbloqueadas[7] == true)
        {
            spriteLevel[2].sprite = spriteBotoes[0];
        }
        else
        {
            spriteLevel[2].sprite = spriteBotoes[1];
        }   

        if(GameController.s.arrayFasesDesbloqueadas[8] == true)
        {
            spriteLevel[3].sprite = spriteBotoes[0];
        }
        else
        {
            spriteLevel[3].sprite = spriteBotoes[1];
        }   

        if(GameController.s.arrayFasesDesbloqueadas[9] == true)
        {
            spriteLevel[4].sprite = spriteBotoes[0];
        }
        else
        {
            spriteLevel[4].sprite = spriteBotoes[1];
        }

    }
}
