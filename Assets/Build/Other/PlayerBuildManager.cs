using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildManager : MonoBehaviour
{
    public Transform buildPoint;

    public GameObject buildObj;

    public bool isBuilding;

    int rot = 0;

    public AudioSource buildSound;

    public float cellSize;

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
                    Vector3 pointScaled = (hit.point / cellSize);

                    int _x = Mathf.RoundToInt(pointScaled.x);
                    int _y = Mathf.RoundToInt(pointScaled.y);
                    int _z = Mathf.RoundToInt(pointScaled.z);


                    buildObj.transform.position = new Vector3(_x, _y, _z) * cellSize;
                }

                buildObj.transform.rotation = Quaternion.Euler(buildObj.transform.rotation.x, rot, buildObj.transform.rotation.z);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(buildObj != null)
            {
                CameraShake.Instance.ShakeCamera();
                buildSound.Play();
                Instantiate(buildObj.GetComponent<BlueprintObject>().prefab, buildObj.transform.position, buildObj.transform.rotation);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(buildObj);
            isBuilding = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rot += 90;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Destroy(buildObj);
            isBuilding = false;
        }
    }
}
