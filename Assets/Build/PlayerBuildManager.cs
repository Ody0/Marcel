using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildManager : MonoBehaviour
{
    public Transform buildPoint;

    public GameObject buildObj;

    public bool isBuilding;

    int rot = 0;

    public void Update()
    {
        if (isBuilding)
        {
            if(buildObj != null)
            {

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    buildObj.transform.position = hit.point;
                }

                buildObj.transform.rotation = Quaternion.Euler(buildObj.transform.rotation.x, rot, buildObj.transform.rotation.z);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(buildObj != null)
            {
                Instantiate(buildObj.GetComponent<BlueprintObject>().prefab, buildObj.transform.position, buildObj.transform.rotation);
                Destroy(buildObj);
                isBuilding = false;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(buildObj);
            isBuilding = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rot += 45;
        }
    }
}
