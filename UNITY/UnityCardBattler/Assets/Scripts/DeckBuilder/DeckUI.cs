using UnityEngine;
using System.Collections.Generic;

public class DeckUI : MonoBehaviour
{

    [SerializeField] private GameObject cardTabPrefab;
    private const float VERTICAL_SPACING = 0.6f;

    private List<GameObject> cardTabGameObjects = new List<GameObject>();

    private void Start()
    {
        BuildUI();
    }

    private void OnDisable()
    {

        DeckEvents.OnDeckProcessed -= BuildUI;


    }

    private void OnEnable()
    {

            DeckEvents.OnDeckProcessed += BuildUI;

    }

   

    private void BuildUI()
    {
        foreach (GameObject cardTab in cardTabGameObjects)
        {
            Destroy(cardTab);
        }

        cardTabGameObjects.Clear();
        List<CardData>deck = DeckManager.Instance.GetDeck();
        for (int i = 0; i < deck.Count; i++)
        {
            GameObject cardTab = Instantiate(cardTabPrefab, transform);
            cardTab.GetComponent<CardTab>().LocadCardTabData(deck[i]);
            cardTab.transform.localPosition = new Vector3(0f, -i * VERTICAL_SPACING, 0f);
            cardTabGameObjects.Add(cardTab);

        }
    }

    
}
