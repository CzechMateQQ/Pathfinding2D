using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public GameObject winCanvas;

    public GameObject flameOne;
    public GameObject flameTwo;
    public GameObject flameThree;

    private SpriteRenderer flameOneSR;
    private SpriteRenderer flameTwoSR;
    private SpriteRenderer flameThreeSR;

    public Sprite litFlame;

    public GameObject doorOne;
    public GameObject doorTwo;
    public GameObject doorThree;
    public GameObject doorFour;

    private SpriteRenderer doorOneSR;
    private SpriteRenderer doorTwoSR;
    private SpriteRenderer doorThreeSR;
    private SpriteRenderer doorFourSR;

    public Sprite openTL;
    public Sprite openTR;
    public Sprite openBL;
    public Sprite openBR;

    public NavMeshAgent player;
    private bool doorOpen = false;

    private void Start()
    {
        flameOneSR = flameOne.GetComponent<SpriteRenderer>();
        flameTwoSR = flameTwo.GetComponent<SpriteRenderer>();
        flameThreeSR = flameThree.GetComponent<SpriteRenderer>();

        doorOneSR = doorOne.GetComponent<SpriteRenderer>();
        doorTwoSR = doorTwo.GetComponent<SpriteRenderer>();
        doorThreeSR = doorThree.GetComponent<SpriteRenderer>();
        doorFourSR = doorFour.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (flameOneSR.sprite == litFlame && flameTwoSR == litFlame && flameThreeSR == litFlame)
        {
            OpenDoor();
        }

        if(doorOpen && player.transform.position.x < 4.5f && player.transform.position.y > 12.5f)
        {
            LevelComplete();
            Debug.Log("You Win!");
        }
    }

    void OpenDoor()
    {
        doorOneSR.sprite = openTL;
        doorTwoSR.sprite = openTR;
        doorThreeSR.sprite = openBL;
        doorFourSR.sprite = openBR;

        doorOpen = true;
    }
    
    void LevelComplete()
    {
        winCanvas.SetActive(true);
    }
}
