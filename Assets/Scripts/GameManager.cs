using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int turtleCount;
    public int maxTurtles;
    public float turtleSpawnTime;
    private float turtleSpawnCounter = 0;
    public GameObject turtle;
    public Vector2 xBounds, yBounds;

    public GameObject[] fish;
    public Vector2 fishX, fishY;
    public float fishSpawnTime;
    private float fishSpawnCoutner;

    public int turtleSaved, trashSaved;

    public Text turtleCoutner;
    public Text turtleCoutner2;
    public Text trashCoutner;

    public Slider polutionSlider;
    public float polutionRate;
    private float polutionCounter;
    private float polutionLevel = 0;
    public float polutionDecrement;
    private float polutionLinearCounter;

    public static bool gameStarted = false;
    public static bool gameOver = false;

    public GameObject startPanel;
    public GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.turtleCount = FindObjectsOfType<TurtleController>().Length;
        polutionCounter = 1f/polutionRate;
        polutionLinearCounter = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") && !gameStarted)
        {
            if (gameOver)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            else
            {
                startPanel.SetActive(false);
                gameStarted = true;
            }
        }

        if(turtleSpawnCounter > 0)
        {
            turtleSpawnCounter -= Time.deltaTime;
        }
        if(turtleCount < maxTurtles && turtleSpawnCounter <= 0)
        {
            Vector3 pos = new Vector3(Random.Range(xBounds.x, xBounds.y), Random.Range(yBounds.x, yBounds.y), 0);
            GameObject newTurtle = Instantiate(turtle, pos, Quaternion.identity);
            turtleCount += 1;
            turtleSpawnCounter = turtleSpawnTime;
        }

        fishSpawnCoutner -= Time.deltaTime;
        if(fishSpawnCoutner < 0)
        {
            fishSpawnCoutner = fishSpawnTime;
            bool goingRight = true;
            float xPos = fishX.x;
            if(Random.Range(0, 2) == 1)
            {
                goingRight = false;
                xPos = fishX.y;
            }
            Vector3 position = new Vector3(xPos, Random.Range(fishY.x, fishY.y), 0);
            GameObject newFish = (GameObject)Instantiate(fish[Random.Range(0, fish.Length)], position, Quaternion.identity);
            newFish.GetComponent<FishController>().speed *= goingRight ? 1 : -1;
            newFish.transform.localScale = new Vector3(goingRight ? 1 : -1, 1, 1);
        }
        turtleCoutner.text = "x" + turtleSaved;
        turtleCoutner2.text = "x" + turtleSaved;
        trashCoutner.text = "x" + trashSaved;

        polutionCounter -= Time.deltaTime;
        if(polutionCounter <= 0)
        {
            if (gameStarted)
            {
                polutionLevel += 1;
            }
            polutionCounter = 1f/polutionRate;
        }
        polutionSlider.value = polutionLevel;

        if (gameStarted)
        {
            polutionLinearCounter -= Time.deltaTime;
            if(polutionLinearCounter <= 0)
            {
                polutionRate += 1;
                polutionLinearCounter = 10;
            }
        }

        if(polutionLevel >= 100)
        {
            gameStarted = false;
            gameOver = true;
            gameOverPanel.SetActive(true);
        }
    }

    public void RemovePolution()
    {
        polutionLevel -= polutionDecrement;
        if(polutionLevel < 0)
        {
            polutionLevel = 0;
        }
    }
}
