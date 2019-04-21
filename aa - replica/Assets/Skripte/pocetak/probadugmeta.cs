using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class probadugmeta : MonoBehaviour {

    //skripta za glavni meni


    public Text single;
    public Text multi;
    public Text About;
    public Text Exit;
    public int Kontrola;


    private void OnMouseDown()   //ako predjemo pritisnemo levi klik ucitava se odredjena scena
    {
        

        switch (Kontrola)
        {
            case 0:
                SceneManager.LoadScene(1);
                break;
            case 1:
                SceneManager.LoadScene(2);
                break;
            case 2:
                SceneManager.LoadScene(4);
                break;
            case 3:
                Application.Quit();
                break;
        }

        FindObjectOfType<AudioManager>().Play("ButtonClick"); //paljenje odredjenog zvuka
    }



    private void OnMouseEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;  //omoguci tu sliku
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2; //prikazi je
        promenaboje();  //promeni boju teksta
        FindObjectOfType<AudioManager>().Play("ButtonEnter");   //pusti zvuk
    }

    private void OnMouseExit()   //vrati na stara podesavanja
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;  
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        promenaboje1();
    }


    private void promenaboje()
    {
        switch(Kontrola)  //menjamo boju teksta u zavsinosti od promenljive kontrola
        {
            case 0:
                single.color = Color.black;
                break;
            case 1:
                multi.color = Color.black;
                break;
            case 2:
                About.color = Color.black;
                break;
            case 3:
                Exit.color = Color.black;
                break;
        }
    }

    private void promenaboje1()
    {
        switch (Kontrola)
        {
            case 0:
                single.color = Color.white;
                break;
            case 1:
                multi.color = Color.white;
                break;
            case 2:
                About.color = Color.white;
                break;
            case 3:
                Exit.color = Color.white;
                break;
        }
    }


}
