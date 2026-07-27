using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [SerializeField, Min(0)]
    private int startingScrap = 0;

    public int Scrap { get; private set; }

    public event Action<int> OnScrapChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Scrap = startingScrap;
    }

    public void AddScrap(int amount)
    {
        if (amount <= 0)
            return;

        Scrap += amount;
        OnScrapChanged?.Invoke(Scrap);
    }

    public bool TrySpendScrap(int amount)
    {
        if (amount <= 0 || Scrap < amount)
            return false;

        Scrap -= amount;
        OnScrapChanged?.Invoke(Scrap);
        return true;
    }


    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        AddScrap(10);
    //    }
    //}
}