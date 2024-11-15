using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomataManager : MonoBehaviour
{
    public GameObject[] states;


    public static AutomataManager Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void ChangeState(string state)
    {
        foreach (GameObject GO in states)
        {
            GO.SetActive(false);
        }

        foreach (GameObject GO in states)
        {
            if (GO.name == state)
            {
                GO.SetActive(true);
            }
        }
    }
}
