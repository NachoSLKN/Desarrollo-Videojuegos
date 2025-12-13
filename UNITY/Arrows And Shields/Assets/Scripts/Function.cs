using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Function : MonoBehaviour
{
   
    public string text;
    public Text textBox;

    public void ActivateText()
    {
        StartCoroutine("ShowText");
    }

    public void DesactivateText()
    {
        StopCoroutine("ShowText");
        textBox.text = "";
    }
    IEnumerator ShowText()
    {
        for (int i = 0; i < text.Length; i++)
        {
            textBox.text += text[i];
            yield return new WaitForSeconds(0.02f);
        }
    }
}
