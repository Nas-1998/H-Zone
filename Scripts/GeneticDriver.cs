using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneticDriver : MonoBehaviour
{
    public GameObject characterObject;
    public GameObject parentObject;
    public GameObject camera;
    // Update is called once per frame
    private GameObject[] objectArr;
    private Population popObject;

    public int populationSize = 50;
    public int geneSize = 500;
    public float mutationRate = 0.01f;
    public int maxGenerations = 50;
    private float startTime = 0;

    public Text generationLabel;
    public Text bestDistanceLabel;
    public Text timeLimitLabel;

    void Start()
    {
        popObject = new Population(populationSize, geneSize, mutationRate);
        objectArr = new GameObject[populationSize];
        GenerateCharacters();
    }
    void GenerateCharacters()
    {
        startTime = Time.time;
        for(int i=0;i<populationSize;i++)
        {
            objectArr[i] = Instantiate(characterObject);
            objectArr[i].transform.position = new Vector2(0, 3);
            objectArr[i].transform.SetParent(parentObject.transform);
            objectArr[i].GetComponent<CharacterMovement>().agent = popObject.PopulationPool[i];
        }
    }

    void DestroyCharacters()
    {
        for(int i=0;i<populationSize;i++)
        {
            Destroy(objectArr[i]);
        }
    }

    void Update()
    {
        int bestInd = 0;
        float bestFitness = 0;
        bool checkForOver = true;
        for(int i=0;i<populationSize;i++)
        {
            if(popObject.PopulationPool[i].FitnessValue>bestFitness)
            {
                bestFitness = popObject.PopulationPool[i].FitnessValue;
                bestInd = i;
            }
            if(objectArr[i].GetComponent<CharacterMovement>().isOver==false)
            {
                checkForOver = false;
            }
        }
        generationLabel.text = popObject.Generation.ToString();
        bestDistanceLabel.text = bestFitness.ToString();
        timeLimitLabel.text = ((int)(Time.time-startTime)).ToString() + "/30";
        if(checkForOver || (Time.time-startTime)>30.0f)
        {
            float avg = 0;
            for(int i=0;i<populationSize;i++)
            {
                avg += popObject.PopulationPool[i].FitnessValue;
            }
            // Debug.Log(avg/populationSize);
            popObject.GenerateNewPopulation();
            DestroyCharacters();
            GenerateCharacters();
        }
        camera.transform.position = new Vector3(objectArr[bestInd].transform.position.x, objectArr[bestInd].transform.position.y, camera.transform.position.z);
    }
}
