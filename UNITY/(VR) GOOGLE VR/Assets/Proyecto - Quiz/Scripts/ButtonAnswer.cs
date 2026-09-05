using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Playables;

public class ButtonAnswer : MonoBehaviour
{
    [Header("Progress Bar")]
    public Image imgProgress;
    //public Color colorProgress;
    public UnityEvent progressEvent;

    Image imgButton;
    Animator anim;

    int hash_Select = Animator.StringToHash("Select");
    //Hacemos un hash del String Select. Optimización de memoria y rendimiento.
    //En vez de buscar el String cada vez, buscamos el hash que es más rápido.

    float timeCurrent;
    bool isEnable;


    private void Awake()
    {
        imgButton = GetComponent<Image>();
        anim = GetComponentInParent<Animator>();
    }


    public void OnPointerEnter()
    {
        if (!GameManager2.Instance.isChanging)
        {
            anim.SetBool(hash_Select, true);

            imgButton.color = GameManager2.Instance.colorEnter;

            isEnable = true;
        }
    }


    public void OnPointerExit()
    {
        if (!GameManager2.Instance.isChanging)
        {
            imgButton.color = GameManager2.Instance.colorExit;

            isEnable = false;
        }

        anim.SetBool(hash_Select, false);
        imgProgress.fillAmount = 0;
        timeCurrent = 0;
    }


    private void Update()
    {
        ProgressTimer();
    }


    private void ProgressTimer()
    {
        if (isEnable)
        {
            timeCurrent += Time.deltaTime;

            imgProgress.fillAmount =
                timeCurrent / GameManager2.Instance.timeToSelect;

            if (timeCurrent >= GameManager2.Instance.timeToSelect)
            {
                isEnable = false;

                progressEvent.Invoke();

                Debug.Log("Event Invoked");
            }
        }
    }
}