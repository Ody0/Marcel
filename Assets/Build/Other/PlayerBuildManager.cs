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
            if (buildObj != null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // Only update position if the raycast hits something
                if (Physics.Raycast(ray, out hit))
                {
                    // Calculate the target position snapped to the grid
                    Vector3 pointScaled = hit.point / cellSize;

                    // Round to nearest grid point
                    int _x = Mathf.RoundToInt(pointScaled.x);
                    int _y = Mathf.RoundToInt(pointScaled.y);
                    int _z = Mathf.RoundToInt(pointScaled.z);

                    // Smooth the movement by using Lerp or directly set it to the snapped position
                    Vector3 targetPosition = new Vector3(_x, _y, _z) * cellSize;
                    buildObj.transform.position = Vector3.Lerp(buildObj.transform.position, targetPosition, Time.deltaTime * 20f); // 10f is a smoothing factor
                }

                // Update rotation directly using Quaternion
                buildObj.transform.rotation = Quaternion.Euler(0, rot, 0);
            }
        }

        // Build on left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            if (buildObj != null)
            {
                CameraShake.Instance.ShakeCamera();
                buildSound.Play();
                Instantiate(buildObj.GetComponent<BlueprintObject>().prefab, buildObj.transform.position, buildObj.transform.rotation);
            }
        }

        // Cancel building on right mouse click
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(buildObj);
            isBuilding = false;
        }

        // Rotate the object on 'R' key press
        if (Input.GetKeyDown(KeyCode.R))
        {
            rot += 90;
            rot %= 360; // Ensure rotation stays within 0-360 degrees
        }

        // Cancel building on 'Tab' key press
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Destroy(buildObj);
            isBuilding = false;
        }
    }

}
