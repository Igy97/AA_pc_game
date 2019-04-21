using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class izbor_za_multiplayer : MonoBehaviour {

    public GameObject podesavanje_igraca, podesavanje_igre;

    public TMP_InputField unos_imena;
    public GameObject uzvicnik1;
    public GameObject uzvicnik2;

    public Sprite crna;
    public Sprite crvena;

    private int igrac = 1;  //za kog igraca unosimo podatke

    private Color boja = Color.white;
    private string ime = "";
    private int poeni = 1;
    private float speed_za_krug = 0.7f;
    private bool random_smer = false, increase_speed = false, change_direction = false;

    public GameObject poeni_za_igru;
    public GameObject brzina_kruga_za_igru;

    private int objekat_boja = 0;

    private void Start()
    {
        uzvicnik1.GetComponent<TextMeshProUGUI>().enabled = false;
        uzvicnik2.GetComponent<TextMeshProUGUI>().enabled = false;
        podesavanje_igre.SetActive(false);
        poeni_za_igru.GetComponent<TextMeshProUGUI>().text = poeni.ToString();
        brzina_kruga_za_igru.GetComponent<TextMeshProUGUI>().text = speed_za_krug.ToString();
    }


    public void ime_za_igraca()  //unosimo ime iz textboxa
    {
        ime = unos_imena.text;  
    }

    public void boja_za_igraca(GameObject dugme)
    {
        objekat_boja = 1;
        dugme.GetComponent<Button>().interactable = false;  //kada izaberemo nego dugme, to dugme postaje neupotrebljivo
        boja = dugme.GetComponent<Image>().color;
        dugme.tag = "boja";  //menjamo tag
        if (GameObject.FindGameObjectWithTag("boja").GetComponent<Button>().interactable == false && GameObject.FindGameObjectWithTag("boja").name != dugme.name)  //ukoliko menjamo dugme
        {
            GameObject.FindGameObjectWithTag("boja").GetComponent<Button>().interactable = true;
            GameObject.FindGameObjectWithTag("boja").GetComponent<Button>().tag = "Untagged";
        }
        
          
    }

   public void options_for_player()
    {
        
        if(ime == "" || boja == Color.white)  //kada treba da se pojave uzvicnici
        {
            if (ime == "") uzvicnik1.GetComponent<TextMeshProUGUI>().enabled = true;
            if (boja == Color.white) uzvicnik2.GetComponent<TextMeshProUGUI>().enabled = true;
            
        }
        else
        {
            uzvicnik1.GetComponent<TextMeshProUGUI>().enabled = false;
            uzvicnik2.GetComponent<TextMeshProUGUI>().enabled = false;

            if (igrac == 1)
            {
                GameObject.Find("Podaci").GetComponent<podaci>().igrac1 = unos_imena.text;
                GameObject.Find("Podaci").GetComponent<podaci>().igrac1_boja = boja;
                GameObject.FindGameObjectWithTag("boja").gameObject.tag = "igrac1";
                igrac = 2;
                boja = Color.white;
                ime = "";
                unos_imena.text = "";
                GameObject.Find("naslov").GetComponent<TextMeshProUGUI>().text = "OPTIONS FOR PLAYER2";
                objekat_boja = 0;
            }
            else if (igrac == 2)  //unos za igraca 2
            {
                GameObject.FindGameObjectWithTag("boja").gameObject.tag = "igrac2";
                GameObject.Find("Podaci").GetComponent<podaci>().igrac2 = unos_imena.text;
                GameObject.Find("Podaci").GetComponent<podaci>().igrac2_boja = boja;
                podesavanje_igre.SetActive(true);
                podesavanje_igraca.SetActive(false);
                objekat_boja = 0;

                igrac = 3;
            }

            else if(igrac == 3)  //podesavanje igre
            {
                GameObject.Find("Podaci").GetComponent<podaci>().brzina_kruga = speed_za_krug;
                GameObject.Find("Podaci").GetComponent<podaci>().poeni = poeni;
                GameObject.Find("Podaci").GetComponent<podaci>().random_direction = random_smer;
                GameObject.Find("Podaci").GetComponent<podaci>().increase_speed = increase_speed;
                GameObject.Find("Podaci").GetComponent<podaci>().pin_change_direction = change_direction;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
        }

        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void options_for_player_back()  //ukoliko se vratimo korak nazad
    {
        if (igrac == 3)
        {
            podesavanje_igre.SetActive(false);
            podesavanje_igraca.SetActive(true);
            uzvicnik1.GetComponent<TextMeshProUGUI>().enabled = false;
            uzvicnik2.GetComponent<TextMeshProUGUI>().enabled = false;
            unos_imena.text = "";
            GameObject.FindGameObjectWithTag("igrac2").GetComponent<Button>().interactable = true;
            GameObject.FindGameObjectWithTag("igrac2").gameObject.tag = "Untagged";
            igrac = 2;
            boja = Color.white;
            ime = "";
            GameObject.Find("Podaci").GetComponent<podaci>().igrac2 = unos_imena.text;
            GameObject.Find("Podaci").GetComponent<podaci>().igrac2_boja = boja;
        }
        else if (igrac == 2)
        {
            uzvicnik1.GetComponent<TextMeshProUGUI>().enabled = false;
            uzvicnik2.GetComponent<TextMeshProUGUI>().enabled = false;
            if (objekat_boja != 0)
            {
                GameObject.FindGameObjectWithTag("boja").GetComponent<Button>().interactable = true;
                GameObject.FindGameObjectWithTag("boja").gameObject.tag = "Untagged";
            }
            unos_imena.text = "";
            GameObject.Find("naslov").GetComponent<TextMeshProUGUI>().text = "OPTIONS FOR PLAYER1";
            igrac = 1;
            GameObject.FindGameObjectWithTag("igrac1").GetComponent<Button>().interactable = true;
            GameObject.FindGameObjectWithTag("igrac1").gameObject.tag = "Untagged";
            boja = Color.white;
            ime = "";
            GameObject.Find("Podaci").GetComponent<podaci>().igrac1 = unos_imena.text;
            GameObject.Find("Podaci").GetComponent<podaci>().igrac1_boja = boja;

        }
        else if (igrac == 1) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);


        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void button_enter(GameObject dugme)  //za dugmad
    {
        FindObjectOfType<AudioManager>().Play("ButtonEnter");
        dugme.GetComponent<Image>().sprite = crvena;

    }

   public void button_exit(GameObject dugme)
   {
        FindObjectOfType<AudioManager>().Play("ButtonEnter");
        dugme.GetComponent<Image>().sprite = crna;

   }


    public void slider_poeni(Slider slajder)
    {
        poeni = (int)slajder.value;
        poeni_za_igru.GetComponent<TextMeshProUGUI>().text = poeni.ToString();
    }

    public void slider_zvuk()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void slider_brzina_kruga(Slider slajder)
    {
        
        speed_za_krug = slajder.value;
        speed_za_krug = Mathf.Round(speed_za_krug * 10f) / 10f;
        brzina_kruga_za_igru.GetComponent<TextMeshProUGUI>().text = speed_za_krug.ToString();
    }


    public void random_direction(Toggle tog)
    {
        random_smer = tog.isOn;
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }


    public void pojacavanje_brzine(Toggle tog)
    {
        increase_speed = tog.isOn;
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void menjaj_smer(Toggle tog)
    {
        change_direction = tog.isOn;
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }




}
