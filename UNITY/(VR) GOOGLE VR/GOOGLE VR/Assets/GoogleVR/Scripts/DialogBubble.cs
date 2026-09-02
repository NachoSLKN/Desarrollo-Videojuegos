using UnityEngine;
using TMPro;

public class DialogBubble : MonoBehaviour
{
	public string dialogText; // Texto que colcamos dentro de la burbuja
	public TextMeshProUGUI txtDialogBubble; // Componente del TextMeshPRo
	public GameObject dialogBubble; // Gameobject que es la burbuja
	
	
    void Start()
    {
		txtDialogBubble.text = dialogText;
        Pointer_Exit();
    }
	

	//----------------Funciones públicas-----------------------
	public void Pointer_Enter ()
	{
		dialogBubble.SetActive(true);
	}
	
	public void Pointer_Exit ()
	{
		dialogBubble.SetActive(false);
	}

   
}
