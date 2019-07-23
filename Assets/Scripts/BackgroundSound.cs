using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSound : MonoBehaviour
{
    Image image;
    public Sprite soundOn;
    public Sprite soundOff;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Mute()
    {
        if(Mathf.Approximately(AudioListener.volume, 0f))
        {
            AudioListener.volume = 0.5f;
            image.sprite = soundOn;
        }
        else
        {
            AudioListener.volume = 0f;
            image.sprite = soundOff;
        }
    }
}
