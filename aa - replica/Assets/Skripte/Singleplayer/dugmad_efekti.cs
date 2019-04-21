using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class dugmad_efekti : MonoBehaviour {

    [HideInInspector]
    public bool kontrola_zakljucavanja_levela = false;


    public void prebacivanje_nivoa()
    {
        if (!kontrola_zakljucavanja_levela) SceneManager.LoadScene(gameObject.name.ToString());  //ako nije level zakljucan, igrac ima pravo da ga izabere
    }

    public void enter()
    {
        if (!kontrola_zakljucavanja_levela)
        {
            gameObject.GetComponent<Image>().color = Color.red;
            FindObjectOfType<AudioManager>().Play("ButtonEnter");
        }
        //else gameObject.GetComponent<Image>().color = new Color32(120, 100, 100, 255);

    }


    public void exit()
    {
        if (!kontrola_zakljucavanja_levela) gameObject.GetComponent<Image>().color = Color.white;
    }
}
