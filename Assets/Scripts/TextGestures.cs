using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGestures : MonoBehaviour
{
    public AudioClip whooSound;
    private AudioSource audioSource;

    public GameObject[] gesturesObject;
    public int flipCounter;
    const int AMAIZING = 7;
    const int GREAT = 5;
    const int NICE = 3;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Fliper.OnFlips += OnFlip;
    }

    private void OnDisable()
    {
        Fliper.OnFlips -= OnFlip;
    }

    private void OnFlip(bool isFlip)
    {
        if (isFlip)
        {
            flipCounter++;

            CheckIfToAnim(AMAIZING, 0);
            CheckIfToAnim(NICE, 1);
            CheckIfToAnim(GREAT, 2);
        }
    }

    private void TextAnimation(int index, bool toAnimate)
    {
        gesturesObject[index].SetActive(toAnimate);
        gesturesObject[index].GetComponent<Animator>().SetBool("isGesture", toAnimate);
    }

    private void CheckIfToAnim(int gestureToCheck, int index)
    {
        bool toAnimate;

        if (flipCounter % gestureToCheck == 0)
        {
            toAnimate = true;
            audioSource.PlayOneShot(whooSound);
        }
        else
        {
            toAnimate = false;
        }

        TextAnimation(index, toAnimate);
    }


}
