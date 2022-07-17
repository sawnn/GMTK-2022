using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonSound;

    public void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioSource.PlayClipAtPoint(buttonSound, transform.position);
        }
    }
}
