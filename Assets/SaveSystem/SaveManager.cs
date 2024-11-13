using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;
using System.Runtime.Versioning;

[System.Serializable]
public class SaveManager : MonoBehaviour
{

    public static SaveManager Instance;

    public string filePath = "data.txt";

    public GameObject[] ID_Objects;


    [Header("Visuals")]
    public Animator loadingCanvas;
    public PauseMenu pauseAnim;


    public void Awake()
    {
        Instance = this;
    }

    public void SaveProgression()
    {
        GameData data = new GameData();

        //Storage

        data.playerPos = PlayerManager.Instance.transform.position;


        List<SaveableObject> objList = new List<SaveableObject>();
        foreach (SaveableObject saveable in GameObject.FindObjectsOfType<SaveableObject>())
        {
            objList.Add(saveable);
        }



        foreach (SaveableObject saveable in objList)
        {
            data.InstantiatedObjects.Add(saveable.ID);
        }

        foreach(SaveableObject saveable in objList)
        {
            data.ObjectsPos.Add(saveable.transform.position);
        }

        foreach (SaveableObject saveable in objList)
        {
            data.ObjectRot.Add(saveable.transform.rotation);
        }

        //Storage




        string toWrite = JsonUtility.ToJson(data);
        using (StreamWriter outputFile = new StreamWriter(filePath))
        {
            outputFile.Write(toWrite);
            print("Output");
        }
    }



    int load_pos;

    public void LoadProgression()
    {

        pauseAnim.ResumeGame();
        loadingCanvas.Play("Idle");


        load_pos = 0;

        if (File.Exists(filePath))
        {
            using (StreamReader inputFile = new StreamReader(filePath))
            {
                string toParse = inputFile.ReadToEnd();
                GameData currentProgressionData = JsonUtility.FromJson<GameData>(toParse);



                //Re-Store

                PlayerManager.Instance.transform.position = currentProgressionData.playerPos;

                    
                foreach (int I in currentProgressionData.InstantiatedObjects)
                {
                    Instantiate(ID_Objects[I], currentProgressionData.ObjectsPos[load_pos], currentProgressionData.ObjectRot[load_pos]);
                    load_pos++;
                }

                //Re-Store



            }

        }
        else
        {
            //Rien Faire
        }



    }


}


[System.Serializable]
public class GameData
{

    public Vector3 playerPos;
    public List<int> InstantiatedObjects = new List<int>();

    public List<Vector3> ObjectsPos = new List<Vector3>();
    public List<Quaternion> ObjectRot = new List<Quaternion>();

}


