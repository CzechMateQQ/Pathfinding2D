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
    public NavMeshAgent enemyOne;
    public NavMeshAgent enemyTwo;

    private Vector3 playerStart;
    private Vector3 enemyOneStart;
    private Vector3 enemyTwoStart;

    private bool doorOpen = false;

    private void Start()
    {
        winCanvas.SetActive(false);

        playerStart = player.transform.position;
        enemyOneStart = enemyOne.transform.position;
        enemyTwoStart = enemyTwo.transform.position;

        flameOneSR = flameOne.GetComponent<SpriteRenderer>();
        flameTwoSR = flameTwo.GetComponent<SpriteRenderer>();
        flameThreeSR = flameThree.GetComponent<SpriteRenderer>();

        doorOneSR = doorOne.GetComponent<SpriteRenderer>();
        doorTwoSR = doorTwo.GetComponent<SpriteRenderer>();
        doorThreeSR = doorThree.GetComponent<SpriteRenderer>();
        doorFourSR = doorFour.GetComponent<SpriteRenderer>();
    }

    // Checks win conditions
    void Update()
    {
        if (flameOneSR.sprite == litFlame && flameTwoSR.sprite == litFlame && flameThreeSR.sprite == litFlame)
        {
            OpenDoor();
        }

        if(doorOpen && player.transform.position.x < 4.5f && player.transform.position.z > 12.5f)
        {
            LevelComplete();
            Debug.Log("You Win!");
        }    
    }

    // Swaps closed door sprites for open door sprites upon completeing objective
    void OpenDoor()
    {
        doorOneSR.sprite = openTL;
        doorTwoSR.sprite = openTR;
        doorThreeSR.sprite = openBL;
        doorFourSR.sprite = openBR;

        doorOpen = true;
    }
    
    // Displays win canvas and resets character positions
    void LevelComplete()
    {
        winCanvas.SetActive(true);

        player.Warp(playerStart);
        player.destination = playerStart;

        enemyOne.Warp(enemyOneStart);
        player.destination = enemyOneStart;

        enemyTwo.Warp(enemyTwoStart);
        enemyTwo.destination = enemyTwoStart;
    }
}
