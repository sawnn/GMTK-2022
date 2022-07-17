using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public Material BlindMaterial;
    public Material BlankMaterial;
    Material StartMaterial;
    public bool isBlank = false;

    private void Start()
    {
        StartMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        // si le dé touche une case grise, HideColor()
        // (si temps suffisant écoulé) + si nombre de dalles passées suffisant 
    }

    public void HideColor()
    {
        isBlank = false;
        gameObject.GetComponent<MeshRenderer>().material = BlindMaterial;
    }

    public void RevealColor()
    {
        isBlank = false;
        gameObject.GetComponent<MeshRenderer>().material = StartMaterial;
    }

    public void ChangeGreyInWhite()
    {
        isBlank = true;
        gameObject.GetComponent<MeshRenderer>().material = BlankMaterial;
    }

}
