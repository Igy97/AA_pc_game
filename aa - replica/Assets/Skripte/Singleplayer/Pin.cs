using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;  //pristup tekst komponenti i njenim funkcijama
using UnityEngine;


public class Pin : MonoBehaviour
{

    //Definisanje promenljivih

    private bool pusti_pin = false;

    public Rigidbody2D telo_pina; //rigidbody pina
    private Collider2D kolider;//kolider od pina
    //private SpriteRenderer podnozje_pina;//podnozje pina


    public GameObject Krug; //ceo objekat kruga

    private GameObject Spawner;//objekat spawner



    public int count = 0;  //koliko puta smo pritisnuli space, jer da nema ove promenljive povecavali bi brzinu pina beskonacno puta



    //  public static int brojac = 1;


    //Povezivanje elemenata

    private void Start()
    {
       // Debug.Log("Start");

        //Definisanje promenljivih
        telo_pina = gameObject.GetComponent<Rigidbody2D>();
        kolider = GetComponent<CircleCollider2D>();
       // podnozje_pina = gameObject.transform.Find("Spheare").gameObject.GetComponent<SpriteRenderer>();
        Krug = GameObject.Find("Krug");
        Spawner = GameObject.Find("Spawner");
    }

    //Detektujemo taster
    private void Update()
    {
       // Debug.Log("Poziciija pina" + gameObject.transform.position);
        //pritiskom space dobijamo brzinu po y osi 
        if (Input.GetKeyUp(KeyCode.Space) && count == 0) pusti_pin = true;
    }


    private void FixedUpdate()  //sva fizika ide u fixedupdate
    {

        if (pusti_pin == true)
        {
            
            //GameManager.brzina_pina += 1.8f;  //povecavamo brzinu pina za 1.8, i svaki sledeci bin uzima tu brzinu i opet je povecava, sve dok jedan pin ne dodirne krug, tad pin dobija pocetnu vrednost brzine
           // Debug.Log("Brzina pina" + GameManager.brzina_pina);
            telo_pina.velocity = new Vector2(0, GameObject.Find("Cela_scena").GetComponent<GameManager>().brzina_pina);
            count++;  //cim ova promenljiva dobije 1 vise nije moguce povecati brzinu pina
           // Debug.Log("Pin dobija brzinu");
            pusti_pin = false;
            Spawner.GetComponent<Spawner>().postavljen_pin = 0;
        }

        //if(gameObject.transform.position.y > -2.3) Spawner.GetComponent<Spawner>().postavljen_pin = 0;

    }



    //Cololision dogaddjaji

    private void OnCollisionStay2D(Collision2D collision)
    {
        

        if (collision.gameObject.CompareTag("Pin_Kruga") || collision.gameObject.CompareTag("Default_pinovi"))
        {
           
            pogodjen_pin(collision);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Krug") && gameObject.CompareTag("Default_pinovi")==false)
        {
            gameObject.transform.parent = collision.transform;   //pin postaje child nase lopte
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //GameManager.brzina_pina = 12f;
            gameObject.tag = "Pin_Kruga";
            gameObject.transform.Find("Spheare").GetComponent<SpriteRenderer>().enabled = true;
            GameManager.poeni--;
            if(Krug.GetComponent<Rotate>().promeni_smer_kad_pin_udari) Krug.GetComponent<Rotate>().promeni_smer = true;
            if (Krug.GetComponent<Rotate>().pojaca_brzinu_kad_pin_udari || Krug.GetComponent<Rotate>().smanjuje_brzinu_udarom_pina) Krug.GetComponent<Rotate>().pomoc_za_varijaciju4();
            // Debug.Log("Zakacio se pin");
        }

        // Debug.Log("Pin");
        if (collision.gameObject.CompareTag("Pin_Kruga") || collision.gameObject.CompareTag("Default_pinovi"))
        {
            // Debug.Log("Detektovan pin");
            gameObject.transform.parent = collision.transform;   //pin postaje child nase lopte
            
            
           // Debug.Log("Poziciija udarenog pina" + gameObject.transform.position);
            pogodjen_pin(collision);

        }
        FindObjectOfType<AudioManager>().Play("Hit");

    }


    private void pogodjen_pin(Collision2D collision)
    {
        if (GameObject.Find("Cela_scena").GetComponent<GameManager>().gasi == 0)
        {
            gameObject.tag = "Pin_Kruga";
            Krug.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
           // Krug.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GameObject.Find("Cela_scena").GetComponent<GameManager>().gasi = 1;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            //gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }


    //sada sve radi pomocu taga, kad stvorimo pin on je sa tagom pin a kad se poveze sa krugom
    //dobija tag pin kruga, tako da kad drugi pin udari od pin koji ima tag pin kruga sve staje!!!

}














