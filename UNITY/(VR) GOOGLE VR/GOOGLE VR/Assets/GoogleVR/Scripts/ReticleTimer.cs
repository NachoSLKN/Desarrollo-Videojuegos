using UnityEngine;
using UnityEngine.UI; // Librería UI.
using UnityEngine.Events;

public class ReticleTimer : MonoBehaviour
{
	[Header ("Timer")]
	public Image imgTimer; // Variable para modificar el Timer.
	[Range (0f, 5f)] public float timeTotal = 1;
	
	[Header ("Events")]
	public UnityEvent[] timerEvents; // Array de Unity Events. 
	
	int idEvent;
	float timeCurrent;
	bool isEnable; // Variable para detectar si la reticula está habilitada para funcionar. 

	void Start ()
	{
		Timer_Exit ();
	}

	void Update ()
	{
		Timer ();
	}

	private void Timer ()
	{
		if (isEnable) // Si está habilitado...
		{
			timeCurrent += Time.deltaTime; // ..el tiempo actual se suma con el tiempo que ocurre en el momento.
			imgTimer.fillAmount = timeCurrent / timeTotal; // El timer toma el tiempo actual y lo divide por le tiempo total. 

			if (timeCurrent >= timeTotal)
			{
				isEnable = false;
				timerEvents[idEvent].Invoke();
			}
		}
	}

	public void Timer_Enter (int _ID) // Le pasamos un nº de identificación. 
	{
		isEnable = true;
		idEvent = _ID;
	}

	public void Timer_Exit ()
	{
		isEnable = false; // Se desactiva. 
		imgTimer.fillAmount = 0;
		timeCurrent = 0;
	}

}