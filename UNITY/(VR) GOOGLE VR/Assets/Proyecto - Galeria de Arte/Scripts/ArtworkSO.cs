using UnityEngine;

//SCRIPTABLE OBJECT QUE NOS PERMITE MANTENER INFORMACIÓN DE LAS OBRAS DE ARTE, EN ESTE CASO UN ARRAY DE TEXTURAS. 

[CreateAssetMenu (fileName = "New Artwork", menuName = "Scriptable Object/New Artwork", order = 0)]
public class ArtworkSO : ScriptableObject
{
	public Texture[] artworks; //ARRAY DE TEXTURAS
}

