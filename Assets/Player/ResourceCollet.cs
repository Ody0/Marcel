using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollet : MonoBehaviour
{
    public Animator anims;


    public bool isCutting;

    public void OnEnable()
    {
        anims.SetTrigger("cutTree");
        anims.SetBool("isCuttingTree", true);
        StartCoroutine("isHeCutting");
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCutting = true;
        }
    }


    public IEnumerator isHeCutting()
    {
        isCutting = false;
        yield return new WaitForSeconds(0.6f);
        if (isCutting)
        {
            StartCoroutine("isHeCutting");
        }
        else
        {
            anims.SetBool("isCuttingTree", false);
            AutomataManager.Instance.ChangeState("Base");
        }
    }
}
