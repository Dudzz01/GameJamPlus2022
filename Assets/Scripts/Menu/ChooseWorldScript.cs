using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseWorldScript : MonoBehaviour
{
    private void Start() {
        Debug.Log("0: " +GameController.s.arrayFasesDesbloqueadas[0] +
                  "1: " +GameController.s.arrayFasesDesbloqueadas[1] +
                  "2: " +GameController.s.arrayFasesDesbloqueadas[2] +
                  "3: " +GameController.s.arrayFasesDesbloqueadas[3] +
                  "4: " +GameController.s.arrayFasesDesbloqueadas[4] );
    }
    public void SelectWorld1()
    {
         
        SceneManager.LoadScene("Levels of W1");
    }

    public void SelectWorld2()
    {
        SceneManager.LoadScene("Levels of W2");
    }

    public void SelectWorld3()
    {
        SceneManager.LoadScene("Levels of W3");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu Original");
    }
}
