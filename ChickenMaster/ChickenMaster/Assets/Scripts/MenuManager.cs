using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip hover;
    public AudioClip click;

    public void loadNextScene()
    {
        Application.LoadLevel("OtherScene");
    }
     
    public void OnHover()
    {
        source.PlayOneShot(hover);
    }
    
    public void OneClick()
    {
        source.PlayOneShot(click);

    }

   
}
