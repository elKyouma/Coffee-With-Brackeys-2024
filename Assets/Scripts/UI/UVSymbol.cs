using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVSymbol : MonoBehaviour
{
    [SerializeField] Texture2D texture;
    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material material = meshRenderer.material;
        material._Base = texture;
    }
}
