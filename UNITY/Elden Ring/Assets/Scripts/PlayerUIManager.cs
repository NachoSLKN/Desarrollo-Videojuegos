using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace NachoSLKN
{
    public class PlayerUIManager : MonoBehaviour
    {

        public static PlayerUIManager instance;
        [Header("NETWORK JOIN")]
        [SerializeField] private bool startGameAsClient;

        private void Awake()
        {
            if(instance == null)
            {
             instance = this;
            }

            else
            {
             Destroy(gameObject);

            }
        }

        private void Start()
        {
            DontDestroyOnLoad(this);

        }


        private void Update()
        {
            if (startGameAsClient)
            {
                startGameAsClient = false;
                //Tenemos que primero cerrar, porque gemos empezado como hosts en el menu principal
                Unity.Netcode.NetworkManager.Singleton.Shutdown();
                //Empezamos la conexion como clientes
                Unity.Netcode.NetworkManager.Singleton.StartClient();
            }
        }
    }
}
