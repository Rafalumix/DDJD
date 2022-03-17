using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    //The idea of this script is to generate automatically new copy of the backround every time the player advance in the world
    //Of course when we pass a background we have to eliminate it to avoid overloading the application
    public GameObject[] availableBackgrounds;
    public List<GameObject> currentBackgrounds;
    private float screenWidthInPoints;


    // Start is called before the first frame update
    void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
        StartCoroutine(GeneratorCheck());
    }

void AddRoom(float farthestBackgroundEndX)
{
    int randomWorldIndex = Random.Range(0, availableBackgrounds.Length);   //Pick random index of the array to generate one background (this version have only 1 back, quite useless)
    GameObject background = (GameObject)Instantiate(availableBackgrounds[randomWorldIndex]); //Creates a new background object using the random index
    float backgroundWidth = background.transform.Find("Floor").localScale.x; //Take the size of the floor inside the room, equal to the room's width
    float backgroundCenter = farthestBackgroundEndX + backgroundWidth * 0.5f; //Calculate where should be the center of the new background
    background.transform.position = new Vector3(backgroundCenter, 0, 0); //Set the position of the new background
    currentBackgrounds.Add(background); //Add the background to the world 
}

private void GenerateBackgroundIfRequired()
{
    List<GameObject> backgroundsToRemove = new List<GameObject>();
    bool addBackgrounds = true;
    float playerX = transform.position.x;
    float removeBackgroundX = playerX - screenWidthInPoints;
    float addBackgroundX = playerX + screenWidthInPoints;
    float farthestBackgroundEndX = 0;
    foreach (var background in currentBackgrounds)
    {
        float backgroundWidth = background.transform.Find("Floor").localScale.x;
        float backgroundStartX = background.transform.position.x - (backgroundWidth * 0.5f);
        float backgroundEndX = backgroundStartX + backgroundWidth;
        if (backgroundStartX > addBackgroundX)
        {
            addBackgrounds = false;
        }
        if (backgroundEndX < removeBackgroundX)
        {
            backgroundsToRemove.Add(background);
        }
        farthestBackgroundEndX = Mathf.Max(farthestBackgroundEndX, backgroundEndX);
    }
    foreach (var background in backgroundsToRemove)
    {
        currentBackgrounds.Remove(background);
        Destroy(background);
    }
    if (addBackgrounds)
    {
        AddRoom(farthestBackgroundEndX);
    }
}

private IEnumerator GeneratorCheck()
{
    while (true)
    {
        GenerateBackgroundIfRequired();
        yield return new WaitForSeconds(0.25f);
    }
}



    // Update is called once per frame
    void Update()
    {
        
    }
}
