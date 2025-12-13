using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditosScript : MonoBehaviour
{

    public TextMeshProUGUI CreditosText;

    public string[] lines;

    public float textSpeed = 0.1f;

    int index;

    void Start()
    {
        CreditosText.text = string.Empty;
        StartDialogue();
    }

    void Update()

    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CreditosText.text == lines[index])
            {
                NextLine();
            }

            else
            {
                StopAllCoroutines();
                CreditosText.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {

        index = 0;
        StartCoroutine(WriteLine());
    }


    IEnumerator WriteLine()

    {

        foreach (char letter in lines[index].ToCharArray())
        {
            CreditosText.text += letter;

            yield return new WaitForSeconds(textSpeed);
        }

    }


    public void NextLine()

    {

        if (index < lines.Length - 1)
        {
            index++;
            CreditosText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


}
