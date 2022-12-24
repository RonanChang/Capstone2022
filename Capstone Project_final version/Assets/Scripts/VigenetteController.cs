using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;




public class VigenetteController : MonoBehaviour
{
    public Volume volume;
    Vignette vignette;
    PlayerLife playerlife;
    public GameObject player;



    void Start()
    {
        
       playerlife = player.GetComponent<PlayerLife>();
        //vignette = volume.GetComponent<Vignette>();
        if(volume.profile.TryGet<Vignette>(out vignette))
        {
            //vignette.intensity.value = 1f;
        }
       
    }

    
    void FixedUpdate()
    {

        float percent = playerlife.currentHealth / 150;
        vignette.intensity.value  = (1f - percent)* 0.9f;
        //Debug.Log(vignette.intensity.value);

    }

}
