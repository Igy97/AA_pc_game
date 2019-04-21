using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;  //pristup tekst komponenti i njenim funkcijama
using UnityEngine;
using TMPro;

public class Semafor : MonoBehaviour
{
    private TextMeshProUGUI tekst;
    
    
    private RectTransform krug;
    private Animator kontrola_animacije;


    public bool multiplayer;


    private void Start()
    {
        tekst = GameObject.Find("Rezultat_canvas").transform.FindChild("Text").GetComponent<TextMeshProUGUI>();
        
        krug = GameObject.FindGameObjectWithTag("Krug").GetComponent<RectTransform>();
        kontrola_animacije = Camera.main.gameObject.GetComponent<Animator>();

    }

    private void Update()  //malo izmenja skipta zbog multiplayera, ako je multiplayer true, ide funkcija vezana za multiplayer
    {
        if (multiplayer == true) prikaz_rezultata_multiplayer();
        else prikaz_rezultata();

        
        //      kanvas.localPosition = krug.localPosition;  //ovde ima vise ovih position, a za promenu canvase se koristi localposition


        //Camera.main.GetComponent<Transform>().position = new Vector3(25, 25, 25);
    }

    private void prikaz_rezultata()
    {
        tekst.text = GameManager.poeni.ToString();
        
        /*
        switch (kontrola_animacije.GetInteger("State"))
        {
            case 0: tekst.text =   break;
            case 1: tekst.text = "Try\nagain\n:("; tekst.fontSize = 17; break;
            case 2: tekst.text = "You\nwon\n:)"; tekst.fontSize = 17; break;
            default: Debug.Log("Nesto nece xD"); break;
        }
        */
    }


    private void prikaz_rezultata_multiplayer()
    {
        tekst.text = GameManger_multiplayer.poeni.ToString();
    }


}
