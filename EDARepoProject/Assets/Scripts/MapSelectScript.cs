using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectScript : MonoBehaviour
{

    public GameObject _mapSelectList;
    public List<GameObject> mapList;
    public int mapIndex = 0;

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
            mapList[mapIndex].SetActive(true); //set active the first gameObject in the list
        }
    }


    public void MapNext()
    {
        mapList[mapIndex].SetActive(false);
        if(mapIndex == mapList.Count - 1)
        {
            mapIndex = 0;
        }
        else
        {
            mapIndex++;
        }
        mapList[mapIndex].SetActive(true);
    }

    public void MapPrevious()
    {
        mapList[mapIndex].SetActive(false);
        if (mapIndex == 0)
        {
            mapIndex = mapList.Count - 1;
        }
        else
        {
            mapIndex--;
        }
        mapList[mapIndex].SetActive(true);
    }

    public void SelectMap()
    {
        PlayerPrefs.SetInt("Selected Map", mapIndex);
        Debug.Log(PlayerPrefs.GetInt("Selected Map"));
    }
}
