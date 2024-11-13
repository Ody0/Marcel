using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;




[System.Serializable]
public class SaveManager : MonoBehaviour
{

    public static SaveManager Instance;

    public string filePath = "data.txt";

    public void Awake()
    {
        Instance = this;
    }

    public void SaveProgression()
    {
        GameData data = new GameData();

        print("Saved!");



        //Storage
        data.playerPos = PlayerManager.Instance.transform.position;
        //Storage




        string toWrite = JsonUtility.ToJson(data);
        using (StreamWriter outputFile = new StreamWriter(filePath))
        {
            outputFile.Write(toWrite);
            print("Output");
        }
    }




    public void LoadProgression()
    {


        if (File.Exists(filePath))
        {
            using (StreamReader inputFile = new StreamReader(filePath))
            {
                string toParse = inputFile.ReadToEnd();
                GameData currentProgressionData = JsonUtility.FromJson<GameData>(toParse);



                //Re-Store
                PlayerManager.Instance.transform.position = currentProgressionData.playerPos;
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

}



