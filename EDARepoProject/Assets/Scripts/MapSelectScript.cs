using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectScript : MonoBehaviour
{

    public GameObject _mapSelectList;
    public List<GameObject> mapList;
    public int index = 0;
    // Use this for initialization
    void Start()
    {
        GameObject[] maps = Resources.LoadAll<GameObject>("MapUI");

        foreach (GameObject m in maps)
        {
            GameObject _map = Instantiate(m) as GameObject;
            //_map.transform.SetParent(GameObject.Find("mapSelectList").transform);
            _map.transform.parent = _mapSelectList.transform;

            mapList.Add(_map);
            _map.SetActive(false); //set the current gameObject as false
            mapList[index].SetActive(true); //set active the first gameObject in the list
        }
    }


    public void MapNext()
    {
        mapList[index].SetActive(false);
        if(index == mapList.Count - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        mapList[index].SetActive(true);
    }

    public void MapPrevious()
    {
        mapList[index].SetActive(false);
        if (index == 0)
        {
            index = mapList.Count - 1;
        }
        else
        {
            index--;
        }
        mapList[index].SetActive(true);
    }

    public void SelectMap()
    {

    }
}
