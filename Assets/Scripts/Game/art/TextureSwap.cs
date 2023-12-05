using System.Collections.Generic;
using UnityEngine;

public class TextureSwap : MonoBehaviour
{

    private Material mat;
    public List<Texture2D> allTextures = new List<Texture2D>();


    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        ApplyRandomTexture();

    }
    void ApplyRandomTexture()
    {
        if (allTextures.Count > 0)
        {
            // Get a random index within the range of the list
            int randomIndex = Random.Range(0, allTextures.Count);

            // Apply the randomly selected texture to the material
            mat.mainTexture = allTextures[randomIndex];
        }
        else
        {
            Debug.LogError("No textures available in the list.");
        }
    }
}
