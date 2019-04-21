using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class ucitiavanje_levela : MonoBehaviour
{
    public GameObject[] Slike;
    public Sprite katanac;
    

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        int Level_reached = PlayerPrefs.GetInt("prelazak_nivoa", 0);
        //Debug.Log(Level_reached);

        

        for(int i = 0; i < Slike.Length; i++)
        {
            if (i  > Level_reached)  //u zavisnosti do kog levela smo stigli, toliko imamo otkljucanih ikonica
            {
                Slike[i].GetComponent<Image>().sprite = katanac;
                Slike[i].GetComponent<Image>().color = new Color32(120,100,100,255);
                Slike[i].GetComponent<dugmad_efekti>().kontrola_zakljucavanja_levela = true;
                Slike[i].transform.FindChild("TextMeshPro Text").GetComponent<TextMeshProUGUI>().enabled = false;
            }
            


        }
        
    }


}
