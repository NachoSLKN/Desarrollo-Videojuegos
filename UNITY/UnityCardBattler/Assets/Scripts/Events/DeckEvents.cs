using UnityEngine;
using System;

public class DeckEvents : MonoBehaviour
{
  
  public static event Action OnDeckProcessed;

  public static event Action<CardData> OnRemoveCardFromDeck;

    public static event Action<CardData> OnAddCardToDeck;

  public static void RemoveCardFromDeck(CardData card)
  {
    OnRemoveCardFromDeck?.Invoke(card);
  }


    public static void AddCardToDeck(CardData card)
    {
        OnAddCardToDeck?.Invoke(card);
    }

    public static void DeckProcessed()
    {
        OnDeckProcessed?.Invoke();
    }

    
}
