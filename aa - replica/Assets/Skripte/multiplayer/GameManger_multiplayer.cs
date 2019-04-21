using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManger_multiplayer : MonoBehaviour
{
    private GameObject semafor;
    private GameObject prikaz_kraja; //koji igrac je pobedio
    private Text prikaz_kraja_text; //text koji se prikazuje na Canvasu

    public int kontrola_levela;
    public static int poeni;

    private GameObject[] pin1;
    private GameObject[] pin2;

    private GameObject spawner1;
    private GameObject spawner2;

    private Spawner_multiplayer pravila;

    

    public bool izmena = false;

    private int rezultat_igrac1, rezultat_igrac2;
    public TextMeshProUGUI rezultat_za_igraca1, rezultat_za_igraca2, ime_igrac1, ime_igrac2;

    private Color boja;

    private void Start()
    {
        GameObject.Find("sep").GetComponent<TextMeshProUGUI>().fontSize = rezultat_za_igraca1.fontSize;
        rezultat_igrac1 = GameObject.FindObjectOfType<podaci>().poeni;
        rezultat_igrac2 = GameObject.FindObjectOfType<podaci>().poeni;
        rezultat_za_igraca1.text = rezultat_igrac1.ToString();
        rezultat_za_igraca2.text = rezultat_igrac2.ToString();
        GameObject.Find("Krug").GetComponent<Rotate>().speed = GameObject.FindObjectOfType<podaci>().brzina_kruga;
        GameObject.Find("Krug").GetComponent<Rotate>().promeni_smer_kad_pin_udari = GameObject.FindObjectOfType<podaci>().pin_change_direction;
        GameObject.Find("Krug").GetComponent<Rotate>().random_smer = GameObject.FindObjectOfType<podaci>().random_direction;
        GameObject.Find("Krug").GetComponent<Rotate>().pojaca_brzinu_kad_pin_udari = GameObject.FindObjectOfType<podaci>().increase_speed;
        ime_igrac1.text = GameObject.FindObjectOfType<podaci>().igrac1;
        ime_igrac2.text = GameObject.FindObjectOfType<podaci>().igrac2;

        poeni = 0;
        semafor = GameObject.Find("Rezultat_canvas").gameObject;
        prikaz_kraja = GameObject.Find("Kraj_canvas").gameObject;
        prikaz_kraja_text = GameObject.Find("Kraj_canvas").transform.Find("Natpis").GetComponent<Text>();
        prikaz_kraja.SetActive(false);
        spawner1 = GameObject.Find("Player1").gameObject;
        spawner2 = GameObject.Find("Player2").gameObject;
        pravila = GameObject.Find("Cela_scena").GetComponent<Spawner_multiplayer>();   //skripta za spawner u multyplayer
        boja = GameObject.FindObjectOfType<podaci>().igrac1_boja;
        GameObject.FindObjectOfType<Spawner_multiplayer>().pin_igrac1.GetComponent<SpriteRenderer>().color = boja;
        boja.a = 0.5f;
        spawner1.GetComponent<SpriteRenderer>().color = boja;
        boja.r += 1;
       boja.g += 1;
        boja.b += 1;
        Camera.main.GetComponent<pracenje_igraca>().za_game_over = boja;

        boja = GameObject.FindObjectOfType<podaci>().igrac2_boja;
        GameObject.FindObjectOfType<Spawner_multiplayer>().pin_igrac2.GetComponent<SpriteRenderer>().color = boja;
        boja.a = 0.5f;
        spawner2.GetComponent<SpriteRenderer>().color = boja;
        boja.r += 1;
        boja.g += 1;
        boja.b += 1;
        Camera.main.GetComponent<pracenje_igraca>().za_win = boja;

    }

    private void Update()
    {
        if (kontrola_levela > 0)
        {
            pin1 = GameObject.FindGameObjectsWithTag("igrac1");
            pin2 = GameObject.FindGameObjectsWithTag("igrac2");

            foreach (GameObject go in pin1)
            {
                go.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }

            foreach (GameObject go in pin2)
            {
                go.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }

            semafor.SetActive(false);
            prikaz_kraja.SetActive(true);
        }

        if(izmena == true)
        {
            izmena = false;
            rezultat_za_igraca1.enabled = false;
            rezultat_za_igraca2.enabled = false;
            GameObject.Find("sep").GetComponent<TextMeshProUGUI>().enabled = false;

            if (kontrola_levela == 1)
            {
                rezultat_igrac1 -= 1;
                //kontrola_levela = 0;
                if (rezultat_igrac1 == 0)
                {
                    prikaz_kraja_text.text = ime_igrac2.text + " won\n:)";

                }
                else
                {
                    prikaz_kraja_text.text = ime_igrac1.text + " lost one point :(\n";
                    
                    rezultat_za_igraca1.text = rezultat_igrac1.ToString();

                }
                Camera.main.GetComponent<pracenje_igraca>().kontrola_animacije = 1;


            }
            else if (kontrola_levela == 2)
            {
                rezultat_igrac2 -= 1;
                if (rezultat_igrac2 == 0)
                {
                    prikaz_kraja_text.text = ime_igrac1.text + " won\n:)";
                }
                else
                {
                    prikaz_kraja_text.text = ime_igrac2.text+" lost one point :(\n";
                    rezultat_za_igraca2.text = rezultat_igrac2.ToString();
                    
                }
                Camera.main.GetComponent<pracenje_igraca>().kontrola_animacije = 2;

            }
        }
        else
        {
            if (kontrola_levela == 1) if (Input.GetKeyDown(KeyCode.RightControl)) Reset_igre();
            if (kontrola_levela == 2) if (Input.GetKeyDown(KeyCode.LeftControl)) Reset_igre();
        }
    }


    public void Reset_igre()
    {
        if (kontrola_levela > 0)
        {
            rezultat_za_igraca1.enabled = true;
            rezultat_za_igraca2.enabled = true;
            GameObject.Find("sep").GetComponent<TextMeshProUGUI>().fontSize = rezultat_za_igraca1.fontSize;
            GameObject.Find("sep").GetComponent<TextMeshProUGUI>().enabled = true;
            GameObject.Find("Krug").GetComponent<Rotate>().speed = GameObject.FindObjectOfType<podaci>().brzina_kruga;
            // Debug.Log("Kontrola levela:" + kontrola_levela);
            pin1 = GameObject.FindGameObjectsWithTag("igrac1");
            pin2 = GameObject.FindGameObjectsWithTag("igrac2");
            GameObject.Find("Krug").GetComponent<Rotate>().enabled = true;
            pravila.enabled = true;
            pravila.kontrola_izbacivanja = true;
            spawner1.SetActive(true);
            spawner2.SetActive(true);
            poeni = 0;
            Camera.main.GetComponent<pracenje_igraca>().set_start_time = false;
            Camera.main.GetComponent<pracenje_igraca>().kontrola_animacije = 3;
            if (rezultat_igrac1 == 0 || rezultat_igrac2 == 0)
            {
                rezultat_igrac1 = GameObject.FindObjectOfType<podaci>().poeni;
                rezultat_igrac2 = GameObject.FindObjectOfType<podaci>().poeni;
                rezultat_za_igraca2.text = rezultat_igrac2.ToString();
                rezultat_za_igraca1.text = rezultat_igrac1.ToString();

            }
            if (kontrola_levela == 1)
            {
                pravila.smer = true;
                //Debug.Log("mora igrati igrac1");
            }
            else if (kontrola_levela == 2)
            {
               // Debug.Log("mora igrati igrac2");
                pravila.smer = false;
               

                // prikaz_kraja_text.text = "Player1 won\n:)";
            }


            foreach (GameObject go in pin1)
            {
                Destroy(go);
            }

            foreach (GameObject go in pin2)
            {
                Destroy(go);
            }

            kontrola_levela = 0;
            semafor.SetActive(true);
            prikaz_kraja.SetActive(false);
        }
    }

    public void Vrati_se_u_glavni_meni()
    {
        SceneManager.LoadScene(0);
    }




}
