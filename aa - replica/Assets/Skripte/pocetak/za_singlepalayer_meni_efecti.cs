using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class za_singlepalayer_meni_efecti : MonoBehaviour {

    public Scrollbar scrool_bar;

    public Sprite crveno_dugme;
    public Sprite crno_dugme;

    private GameObject Dugme_desno_black;
    private GameObject Dugme_levo_black;
    int stanje = 0;
    int pomeri = 0;

    private static float  dugme_count = 0; //koliko puta smo pritisnuli dugme, ukoliko pritisnemo levo dugme, smanjujemo za 1 a ako pritsnemo desno povecavamo za 1
    private float promenljiva_do_koje_ide_slajder; //sama rec kaze, pritskamo levo ili desno dugme, slajder se pomera i dolaze do te vrednosti, kada dodje on staje


    

    private float start_time;

    private void Start()
    {
        dugme_count = 0;
        if (SceneManager.GetActiveScene().name != "About")  //koriscenja je ista skripta, pa zbog toga da bi se koristilo desno dugme mora biti razlicito od te scene
        {
            Dugme_desno_black = GameObject.Find("desno_dugme_black").gameObject;     
        }      
 
        Dugme_levo_black = GameObject.Find("levo_dugme_black").gameObject;
    }

    private void Update()
    {


        if (pomeri != 0)
        {
            pomeranje_scoolbara_desno_smooth();  //pomeraj ikone sve dok se vrednost dugme_count == scrool_bar.value
            if (dugme_count == scrool_bar.value) pomeri = 0;
        }
        
    }

    public void promena_dugmeta()  //menjamo ikonice
    {
        if (stanje == 0)
        {
            gameObject.GetComponent<Image>().sprite = crveno_dugme;
            stanje = 1;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = crno_dugme;
            stanje = 0;
        }
        
        
        FindObjectOfType<AudioManager>().Play("ButtonEnter");
    }

    public void ukljuci_dugme()
    {     
         gameObject.GetComponent<Image>().enabled = true;
    }

  


    public void pritisnuto_dugme_levo()  //ako je prisnuto dugme levo
    {
        if (dugme_count != 0f)
        {
            start_time = Time.time;  //uzima se vreme od kad je pritisnuto to dugme
            dugme_count -= 0.25f;  //smanjujemo za 0.25
            pomeri = 1;  //pomeranje setujemo na 1, da bi mogli da promenimo polozaj ikonica
        }
        else SceneManager.LoadScene(0);  //ako je scrool bar na pocetku tad mozes da se vratis u glavni meni

        
        
       if (Dugme_desno_black.GetComponent<Image>().enabled == false) Dugme_desno_black.GetComponent<Image>().enabled = true;
        
       
       
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

  
    public void pritisnuto_dugme_desno()
    {
        
        if(dugme_count!=1)
        {
            start_time = Time.time;
            dugme_count += 0.25f;
            pomeri = 1;
            
        }

        if (dugme_count == 1)
        {
            gameObject.GetComponent<Image>().enabled = false;
        }
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }


    private void pomeranje_scoolbara_desno_smooth()  //funkcija za pomeranje
    {
    
        scrool_bar.value  = Mathf.SmoothStep(scrool_bar.value, dugme_count, (Time.time - start_time) / 0.6666666f); 
    }

}
