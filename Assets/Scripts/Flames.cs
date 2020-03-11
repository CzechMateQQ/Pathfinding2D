using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour
{
    public Sprite litFlame;
    public Sprite unlitFlame;

    private SpriteRenderer myFlameSR;

    void Start()
    {
        myFlameSR = GetComponent<SpriteRenderer>();
        myFlameSR.sprite = unlitFlame;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && myFlameSR.sprite == unlitFlame)
        {
            myFlameSR.sprite = litFlame;
        }
    }
}
