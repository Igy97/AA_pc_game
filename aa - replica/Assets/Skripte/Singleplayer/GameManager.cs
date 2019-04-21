using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour {

    public int gasi = 0;  //aktivira se if u pin skripti u dogadjiajima za coliision enter i stay
    private int provera= 0; //samo jednom moze da se korisit if
    GameObject[] pinovi; //koristimo da skupimo sve pinove u jedan niz
    GameObject Spawner; //promenljiva za Spawner
    
    public  float brzina_pina = 12f;  //koristimo za brzinu pina, takodje uzeli smo static da bi ova promenljiva vec pila definisana pre svih
    public static int poeni; //poeni koji se vide na krugu, oni su static i uzimaju vrednost od promenljive ispod, jer ako igrac izgubi, ne korsitimo scene manager za reload scene
    //nego moramo sve manuelno da vratimo, i uvek moramo imati pocetnu vrenstost koliko pinova sve da se zakaci na krug

    public int poeni_na_pocetku_nivoa;  //sve objasnjeno gore

    private int kontrola_animacije;

    //prikaz rezultata i kontrola teksta
    private GameObject rezultat;
    private GameObject kraj;
    private GameObject presao;
    private GameObject informacije_o_nivou;

    private RectTransform retry_transform;

    private float default_speed; //promenljiva za pocetnu brzinu rotacije kruga

    private void Start()
    {
        poeni = poeni_na_pocetku_nivoa; //da bi kasnije stavili na krug
        kontrola_animacije = Camera.main.gameObject.GetComponent<pracenje_igraca>().kontrola_animacije;
        Spawner = GameObject.Find("Spawner").gameObject;
        retry_transform = GameObject.Find("Kraj_canvas").transform.FindChild("Retry").transform.FindChild("Text (2)").GetComponent<RectTransform>();
        rezultat = GameObject.Find("Rezultat_canvas").gameObject;
        kraj = GameObject.Find("Kraj_canvas").gameObject;
        presao = GameObject.Find("Presao_canvas").gameObject;
        informacije_o_nivou = GameObject.Find("Nivo").gameObject;
        informacije_o_nivou.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = SceneManager.GetActiveScene().name;
        kraj.SetActive(false);
        //kraj.GetComponent<Canvas>().enabled = false;
        presao.SetActive(false);
        //presao.GetComponent<Canvas>().enabled = false;
        default_speed = GameObject.Find("Krug").GetComponent<Rotate>().speed;
    }


    private void Update()
    {
        if (gasi != 0 && provera == 0) Kraj();
        if (poeni == 0 && Camera.main.GetComponent<pracenje_igraca>().kontrola_animacije == 0)  Presao_Nivo();
        if (Camera.main.GetComponent<pracenje_igraca>().kontrola_animacije == 2 && Input.GetKeyDown(KeyCode.Space) && Camera.main.orthographicSize <=2.8) Ucitaj_novi_nivo();
        if (Camera.main.GetComponent<pracenje_igraca>().kontrola_animacije == 1 && Input.GetKeyDown(KeyCode.Space) && Camera.main.orthographicSize <= 2.8) Vrati_na_Pocetak();
    }



    public void Vrati_na_Pocetak()
    {
        Brisi_pinove_kruga();
        brisi_nedefinisane_pinove();
        //Debug.Log("Vrati na pocetak");
        vrati_default_pinovima_pocetnu_boju();
        Camera.main.GetComponent<pracenje_igraca>().set_start_time = false;
        Camera.main.GetComponent<pracenje_igraca>().kontrola_animacije = 3;  
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //nece ostati ovako 
        // poeni = 0;
        poeni = poeni_na_pocetku_nivoa;
        //brzina_pina = 12f;
        Spawner.SetActive(true);
        Spawner.GetComponent<Spawner>().postavljen_pin = 0;
        Spawner.GetComponent<Spawner>().broj_pinova = poeni_na_pocetku_nivoa;
        GameObject.Find("Krug").GetComponent<Rotate>().enabled = true;  
        gasi = 0;
        provera = 0;  
        retry_transform.localScale = new Vector3(2.141553f, 0.8067297f, 1);
        rezultat.SetActive(true);   //pali poene
        kraj.SetActive(false); //gasi tekst za kraj i dodaje dva dugmeta
        //kraj.GetComponent<Canvas>().enabled = false;
        informacije_o_nivou.GetComponent<Canvas>().enabled = true;
        gameObject.transform.Find("Krug").transform.rotation = Quaternion.identity;
        gameObject.transform.Find("Krug").GetComponent<Rotate>().start_time = Time.time;
        GameObject.Find("Krug").GetComponent<Rotate>().speed = default_speed;
    }


    private void Presao_Nivo()
    {
        informacije_o_nivou.GetComponent<Canvas>().enabled = false;
        rezultat.SetActive(false);
        presao.SetActive(true);
        //presao.GetComponent<Canvas>().enabled = true;
        //Debug.Log("Presao si nivo");
        Spawner.SetActive(false);
        brisi_nedefinisane_pinove();
        Camera.main.GetComponent<pracenje_igraca>().kontrola_animacije = 2;
        int Level_reached = PlayerPrefs.GetInt("prelazak_nivoa");
        if (Level_reached < SceneManager.GetActiveScene().buildIndex-1)
        PlayerPrefs.SetInt("prelazak_nivoa", SceneManager.GetActiveScene().buildIndex-4);  //podesavaj ovo stalno!!!  -3 zato sto imamo , pocetnu scenu, multiplayer i about, izbor_za_multiplayer
        FindObjectOfType<AudioManager>().Play("WinGame");
    }

    public void Ucitaj_novi_nivo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
    }

    public void Izadji_iz_igre()
    {
        SceneManager.LoadScene(1); //ucitaj izbor nivoa
    }


    private void Kraj()
    {
        //GameObject.Find("Krug").GetComponent<RectTransform>().localPosition = new Vector3(0.20513f, 2.39f, 0f);
        //Camera.main.GetComponent<pracenje_igraca>().enabled = true;
        informacije_o_nivou.GetComponent<Canvas>().enabled = false;
        Spawner.SetActive(false); //iskljuci spawner 
        brisi_nedefinisane_pinove(); //brise sve pinove 
        provera = 1;
        rezultat.SetActive(false);   //gasi poene
        kraj.SetActive(true); //pali tekst za kraj i dodaje dva dugmeta
        //kraj.GetComponent<Canvas>().enabled = true;
        //Debug.Log("Spremno za gasenje");
        GameObject.Find("Krug").GetComponent<Rotate>().enabled = false; //uzimamo pristup skripti i gasimo je!       
        //Invoke("Kraj", 2); //ova funkicja posle dve sekunde poziva funkciju Kraj()
        //GameObject.FindWithTag("prvi_kanvas").transform.FindChild("Panel").gameObject.SetActive(true);
        Camera.main.GetComponent<pracenje_igraca>().kontrola_animacije = 1; //menjamo animaciju za kraj    
        FindObjectOfType<AudioManager>().Play("EndGame");
        //gameObject.transform.Find("Krug").GetComponent<Animator>().SetInteger("state", 0); //za varijacije kruga
        //gameObject.transform.Find("Krug").GetComponent<Rotate>().speed = default_speed;
    }

    private void Brisi_pinove_kruga()  
    {
        pinovi = GameObject.FindGameObjectsWithTag("Pin_Kruga");

        foreach (GameObject go in pinovi)
        {
            Destroy(go);
        }
    }

    private void vrati_default_pinovima_pocetnu_boju()
    {
        pinovi = GameObject.FindGameObjectsWithTag("Default_pinovi");

        foreach (GameObject go in pinovi)
        {
            go.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }


    private void brisi_nedefinisane_pinove() //brise pinove sa tagom Pin
    {
        pinovi = GameObject.FindGameObjectsWithTag("Pin");  //uzima sve te objekte, ne moze samo jedan

        foreach (GameObject go in pinovi)  
        {
             go.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; //napraviti da budu static, ne mogu da se pomeraju
             go.GetComponent<Pin>().enabled = false; //gasimo skriptu pin na svim pinovima, da ne opterecujemo procesor
             Destroy(go);
        }
    }
}
