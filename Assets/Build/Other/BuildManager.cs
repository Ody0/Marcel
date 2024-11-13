using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public PlayerBuildManager pbm;


    public GameObject buildCanvas;
    public GameObject invCanvas;


    public void OpenBuild()
    {
        buildCanvas.SetActive(true);
        invCanvas.SetActive(false);
    }

    public void OpenInv()
    {
        buildCanvas.SetActive(false);
        invCanvas.SetActive(true);
    }
    
    public void Build(GameObject obj)
    {
        pbm.buildObj = Instantiate(obj);
        pbm.isBuilding = true;
        pbm.GetComponent<PlayerManager>().CloseShop();
    }

}
