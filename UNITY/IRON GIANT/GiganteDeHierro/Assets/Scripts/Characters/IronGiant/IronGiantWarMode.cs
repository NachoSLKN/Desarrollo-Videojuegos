using UnityEngine;

public class IronGiantWarMode : MonoBehaviour
{
    [Header("Brazo normal")]
    public GameObject upperArmNormal;
    public GameObject forearmNormal;
    public GameObject handNormal;

    [Header("Brazo War")]
    public GameObject rightArmWar;

    private bool warMode = false;

    void Start()
    {
        rightArmWar.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            warMode = !warMode;

            upperArmNormal.SetActive(!warMode);
            forearmNormal.SetActive(!warMode);
            handNormal.SetActive(!warMode);


            rightArmWar.SetActive(warMode);
        }
    }
}