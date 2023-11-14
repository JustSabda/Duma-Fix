using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProdukMenu : MonoBehaviour
{
    public GameObject[] productsOn;
    public GameObject theProductOn;

    public GameObject[] productsOff;
    public GameObject theProductOff;
    private void Start()
    {
        ArrayOnPanel();
        ArrayOffPanel();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 0);
        for (int i = 0; i < productsOn.Length; i++)
        {
            productsOn[i].SetActive(false);
            productsOff[i].SetActive(true);
        }
        for (int i = 0; i < unlockedLevel - 1; i++)
        {
            productsOn[i].SetActive(true);
            productsOff[i].SetActive(false);
        }
    }

    void ArrayOnPanel()
    {
        int childCount = theProductOn.transform.childCount;
        productsOn = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            productsOn[i] = theProductOn.transform.GetChild(i).gameObject;
        }
    }
    void ArrayOffPanel()
    {
        int childCount = theProductOff.transform.childCount;
        productsOff = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            productsOff[i] = theProductOff.transform.GetChild(i).gameObject;
        }
    }
}
