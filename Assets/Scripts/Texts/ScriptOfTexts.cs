using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScriptOfTexts : MonoBehaviour
{

  private void Update() {
    if(gameObject.GetComponent<Text>().enabled == true)
    {
      StartCoroutine(DesabiltyText());
      Debug.Log("ATIVOU");
    }
  }
    private IEnumerator DesabiltyText()
   {
     
     yield return new WaitForSeconds(6f);
     Debug.Log("TO VIVO");
     gameObject.SetActive(false);
     
     yield return null;
   }
}
