using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public PlayerBuildManager pbm;
    
    public void Build(GameObject obj)
    {
        pbm.buildObj = Instantiate(obj);
        pbm.isBuilding = true;
        pbm.GetComponent<PlayerManager>().CloseShop();
    }

}
