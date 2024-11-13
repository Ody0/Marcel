using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform[] slots;      
    public Transform[] UI_Prefabs;    
    public int[] ID_Prefabs;             


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                InventoryItem item = hit.transform.GetComponent<InventoryItem>();
                if (item != null)
                {
                    AddItemToInventory(item.ID);
                    hit.transform.GetComponent<DestroyableObject>().PickupMe();
                }
            }
        }
    }


    public void AddItemToInventory(int itemID)
    {
        for (int i = 0; i < ID_Prefabs.Length; i++)
        {
            if (ID_Prefabs[i] == 0)
            {
                ID_Prefabs[i] = itemID;
                RefreshInventory();
                return;
            }
        }

        Debug.Log("Inventory is full!");
    }

    public void RefreshInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            foreach (Transform child in slots[i])
            {
                Destroy(child.gameObject);
            }

            if (ID_Prefabs[i] != 0 && ID_Prefabs[i] < UI_Prefabs.Length)
            {
                Transform item = Instantiate(UI_Prefabs[ID_Prefabs[i]], slots[i]);
                item.localPosition = Vector3.zero;
            }
        }
    }
}
