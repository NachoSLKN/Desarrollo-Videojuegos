
using UnityEngine;

public class Artwork : MonoBehaviour
{

    [Header("Artwork")]
    public ArtworkSO artworkSO; //Referencia al Scriptable Object ArtworkSO 
    private int materialId = 1; //ID del material que queremos cambiar, en este caso el 1, que es el material de la obra de arte.
    MeshRenderer[] artworks;

    // Start is called before the first frame update
    private void Start()
    {
        

artworks = GetComponentsInChildren<MeshRenderer>(); //Obtenemos todos los MeshRenderer de los hijos del GameObject Artwork.
SetArtworks();


    }

        private void SetArtworks()
    {

        for(int i = 0; i < artworks.Length; i++)
        {
            artworks[i].material.mainTexture = artworkSO.artworks[i]; //Asignamos las texturas del array de texturas del Scriptable Object a los materiales de los MeshRenderer.
        }

    }


   
}
