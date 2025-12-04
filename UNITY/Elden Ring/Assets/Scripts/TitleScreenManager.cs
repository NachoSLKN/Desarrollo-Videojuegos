using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace NachoSLKN
{
    public class TitleScreenManager : MonoBehaviour
    {
        public void StartNetworkAsHost()
        {
            Unity.Netcode.NetworkManager.Singleton.StartHost(); //Comienza la conexión como host
        }

        public void StartNewGame()
        {
            StartCoroutine(WorldSaveGameManager.instance.LoadNewGame());
        }

    }
}
