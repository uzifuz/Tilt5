using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseOneOfList : MonoBehaviour
{
    [SerializeField]
    GameObject[] possibleObj;
    [SerializeField]
    bool selectOnlyOne = true;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in possibleObj)
        {
            obj.SetActive(false);
        }
        SelectObjects();
    }

    void SelectObjects()
    {
        for (int i = 0; i < possibleObj.Length; i++)
        {
            if(selectOnlyOne)
            {
                possibleObj[Random.Range(0, possibleObj.Length)].SetActive(true);
                break;
            }
        }
    }
}
