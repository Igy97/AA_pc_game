using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;

public class slajder_skripta : MonoBehaviour {

    private float speed = 0.7f;
    private bool rotiraj = true;

    private GameObject objekat1;
    private GameObject objekat2;
    private GameObject objekat3;
    private GameObject objekat4;

    private GameObject meni;
    private GameObject pocetni_tekst;

    private float ugao_rotacije;


    private AudioManager s;
    private Sound pesma;

    private void Start()
    {
        //dodeljivanje odredjenih komponenti preko GameObject.Find naredbe
        objekat1 = GameObject.Find("Cela_scena").transform.Find("meni_efekat").transform.Find("1").gameObject;
        objekat2 = GameObject.Find("Cela_scena").transform.Find("meni_efekat").transform.Find("2").gameObject;
        objekat3 = GameObject.Find("Cela_scena").transform.Find("meni_efekat").transform.Find("3").gameObject;
        objekat4 = GameObject.Find("Cela_scena").transform.Find("meni_efekat").transform.Find("4").gameObject;
        meni = GameObject.Find("Cela_scena").transform.Find("meni_efekat").transform.Find("Canvas").gameObject;
        pocetni_tekst = GameObject.Find("Cela_scena").transform.Find("meni_efekat").transform.Find("pocetak_tekst").gameObject;

        meni.SetActive(false);
        objekat1.SetActive(false);
        objekat2.SetActive(false);
        objekat3.SetActive(false);
        objekat4.SetActive(false);


        s = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        pesma = Array.Find(s.sounds, z=> z.name == "Background");
        GameObject.Find("Slider (2)").GetComponent<Slider>().value = pesma.volume;

    }

    private void FixedUpdate()
    {
        if(rotiraj == true) gameObject.transform.Rotate(new Vector3(0, 0, speed)); //rotiranje kruga

    }


    private void Update()
    {
        GameObject.Find("Slider (2)").GetComponent<Slider>().value = pesma.volume;
        if (gameObject.GetComponent<RectTransform>().rotation.eulerAngles.y > 90)  //prikazati objekte ukoliko je ugao veci od 90
        {
            meni.SetActive(true);
            objekat1.SetActive(true);
            objekat2.SetActive(true);
            objekat3.SetActive(true);
            objekat4.SetActive(true);
            pocetni_tekst.SetActive(false);
        }
        else 
        {
            meni.SetActive(false);
            objekat1.SetActive(false);
            objekat2.SetActive(false);
            objekat3.SetActive(false);
            objekat4.SetActive(false);
            pocetni_tekst.SetActive(true);
        }
    }


    public void Rotacija(float promenljiva) //rotiraj sve ove komponete za odredjenu vrednost promenljive
    {
        ugao_rotacije = promenljiva;
        gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, promenljiva, gameObject.GetComponent<RectTransform>().rotation.eulerAngles.z);
        meni.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, promenljiva+180, 0);
        pocetni_tekst.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, promenljiva, 0);
        objekat1.GetComponent<Transform>().rotation = Quaternion.Euler(0, promenljiva, 0);
        objekat2.GetComponent<Transform>().rotation = Quaternion.Euler(0, promenljiva, 0);
        objekat3.GetComponent<Transform>().rotation = Quaternion.Euler(0, promenljiva, 0);
        objekat4.GetComponent<Transform>().rotation = Quaternion.Euler(0, promenljiva, 0);
        rotiraj = false;
    }

    public void otpustio_klik()
    {
        rotiraj = true;
    }

    public void zvuk1()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
	
    public void brzina_rotacije(float brzina)
    {  
        speed = brzina;
    }

    public void zvuk_u_igri(float sound)  //podesavanje treceg slajdera
    {   
        if (sound == 0)  //ako je zvuk jednak 0, zaustavi pesmu.
        {
            pesma.source.Stop();
            s.kontrola_pustanja = false;
        }
        else  //inace promeni na drugu
        {
            if (!s.kontrola_pustanja) s.kontrola_pustanja = true;
            pesma.source.volume = sound;
        }
        pesma.volume = sound;
    }

}
