using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 4.7f;  //brzina rotacije kruga
    //public int broj_varijacije;

    public bool biranje_pinova_pomocu_niza = false;
    public int[] niz_izabranih_pinova; //koje pinve ostavljamo upaljenim

    public bool  ubrzava_do_odredjene_vrednosti_animacija, promeni_smer_kad_pin_udari, pojaca_brzinu_kad_pin_udari, random_smer, smanjuje_brzinu_udarom_pina, sakriva_pinove, pojava_rotaciju, smanjuje_rotaciju, random_brzina, pokreni_krug;


    private int i = 0, j = 0, h = 0;  //pomocne

    [HideInInspector]
    public bool promeni_smer = false;  //kod varijacije 3 i 5

    private GameObject[] nizovi;  //sakuplja sve default pinove

    public float brzina_do_koje_ide_krug;
    public float ubrzanje_kruga;

    private int brojac_za_varijaciju7 = 0;
    public float vreme_za_sakrivanje_pinova;
    public float vreme_random_brzina;

    private bool pin_udario = false;  //koristi se kod varijacije 4 i 6


    private bool promeni_pozciju = false;
    private Vector3 start_position;


    public float pozicija_y_do_koje_ide_krug = 2f;
    public float brzina_kruga = 2f;


    public bool random_brzina_kruga = false;
    public float vreme_random_brzina_kruga;

    public bool random_granica_kruga = false;
    public float vreme_random_granica_kruga;

    [HideInInspector]
    public float start_time;

    private void Start()
    {
        start_time = Time.time;
        start_position = Camera.main.GetComponent<pracenje_igraca>().startpositon;

        if (biranje_pinova_pomocu_niza || sakriva_pinove) nizovi = GameObject.FindGameObjectsWithTag("Default_pinovi");


        if (biranje_pinova_pomocu_niza == true)
        {
            foreach (GameObject child in nizovi)
            {
                bool pin_izabran = false;
                for (h = 0; h < niz_izabranih_pinova.Length; h++) if (child.name == niz_izabranih_pinova[h].ToString()) pin_izabran = true;

                if (!pin_izabran) child.SetActive(false);
            }
        }

        if (sakriva_pinove == true) InvokeRepeating("varijacija7", 0.5f, vreme_za_sakrivanje_pinova);

        if (random_brzina) InvokeRepeating("varijacija10", 0.5f, vreme_random_brzina);
        if (random_brzina_kruga) InvokeRepeating("nasumicna11", 0.5f, vreme_random_brzina_kruga);
        if (random_granica_kruga) InvokeRepeating("nasumicna12", 0.5f, vreme_random_granica_kruga);
    }

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, speed)); //rotiranje kruga      
    }


    private void Update()
    {

        //if (polako_menja_brzinu_pomocu_animacije) varijacija1();

        //varijacije
        if (ubrzava_do_odredjene_vrednosti_animacija) varijacija2();
        if (promeni_smer_kad_pin_udari) varijacija3();
        if (pojaca_brzinu_kad_pin_udari) varijacija4();
        if (random_smer) varijacija5();
        if (smanjuje_brzinu_udarom_pina) varijacija6();
        if (pojava_rotaciju) varijacija8();
        if (smanjuje_rotaciju) varijacija9();
        if (pokreni_krug) varijacija11();
    }


    private void varijacija2()  //polako ubrzava do odredjene vrednosti 
    {
        float t = (Time.time-start_time) / ubrzanje_kruga;
        speed = Mathf.SmoothStep(0, brzina_do_koje_ide_krug, t);

    }


    private void varijacija3()   //promeni smer kada pin udari u krug
    {

        if (promeni_smer == true)
        {
            promeni_smer = false;
            speed *= (-1);
        }

    }

    private void varijacija4() // pojacaj brzinu kada pin udari u krug
    {
        if (pin_udario == true)
        {
            pin_udario = false;
            if (speed > 0) speed += 0.15f;
            else speed -= 0.15f;
        }
    }

    public void pomoc_za_varijaciju4()
    {
        pin_udario = true;
    }


    private void varijacija5()  //random promena smera
    {
        if (!promeni_smer && j == 0)
        {
            j++;
            Invoke("pomoc_za_varijaciju5", Random.Range(1, 5));
        }

        if (promeni_smer == true)
        {
            speed *= (-1);
            promeni_smer = false;
        }
    }

    private void pomoc_za_varijaciju5()
    {
        promeni_smer = true;
        j = 0;
    }


    private void varijacija6()  //pojacava brzinu rotacije ali ako pin udari u krug on smanjuje brzinu 
    {
        if (pin_udario == true)
        {
            pin_udario = false;
            speed -= 0.5f;
        }
    }

    private void varijacija7()   //ova varijacija sakriva pinove
    {
        if (biranje_pinova_pomocu_niza == true)
        {
            if (brojac_za_varijaciju7 == 0)
            {
                brojac_za_varijaciju7++;
                for (h = 0; h < niz_izabranih_pinova.Length; h++)
                {
                    gameObject.transform.FindChild(niz_izabranih_pinova[h].ToString()).GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.transform.FindChild(niz_izabranih_pinova[h].ToString()).transform.FindChild("Spheare").GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            else
            {
                brojac_za_varijaciju7 = 0;
                for (h = 0; h < niz_izabranih_pinova.Length; h++)
                {
                    gameObject.transform.FindChild(niz_izabranih_pinova[h].ToString()).GetComponent<SpriteRenderer>().enabled = true;
                    gameObject.transform.FindChild(niz_izabranih_pinova[h].ToString()).transform.FindChild("Spheare").GetComponent<SpriteRenderer>().enabled = true;

                }
            }
        }
        else
        {
            foreach (GameObject child in nizovi)
            {
                if (brojac_za_varijaciju7 == 0)
                {
                    child.GetComponent<SpriteRenderer>().enabled = true;
                    child.transform.FindChild("Spheare").GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    child.GetComponent<SpriteRenderer>().enabled = false;
                    child.transform.FindChild("Spheare").GetComponent<SpriteRenderer>().enabled = false;
                }
            }

            if (brojac_za_varijaciju7 != 0) brojac_za_varijaciju7 = 0;
            else brojac_za_varijaciju7++;

        }

    }


    private void varijacija8()  //pojacava brzinu rotacije
    {
        if (speed < 150f) speed += 0.009f;
    }

    private void varijacija9()  //smanjuje brzinu rotacije
    {
        if (speed > 0) speed -= 0.009f;
    }

    private void varijacija10()  //random brzina kruga
    {
        speed = Random.Range(-1f, 1f);
    }

    private void varijacija11()  //krug se pomera
    {
        // Debug.Log(promeni_pozciju);
        if (GameManager.poeni != 0)
        {
            if (GameObject.Find("Spawner").transform.position.y + pozicija_y_do_koje_ide_krug < gameObject.transform.position.y && !promeni_pozciju)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -brzina_kruga);

                //Debug.Log("1");
            }
            else promeni_pozciju = true;

            if (promeni_pozciju && gameObject.transform.position.y < start_position.y)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, brzina_kruga);

                //Debug.Log("2");
            }
            else promeni_pozciju = false;
        }
    }

    private void nasumicna11()
    {
        brzina_kruga = Random.Range(1f, 10f);
    }

    private void nasumicna12()
    {
        pozicija_y_do_koje_ide_krug = Random.Range(1f, 10f);
    }




}
