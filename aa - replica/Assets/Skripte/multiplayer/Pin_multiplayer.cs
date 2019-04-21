using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin_multiplayer : MonoBehaviour
{

    public float brzina;
    public bool pusti_pin = false;    //ukoliko je ovo aktivno pin dobija silu
    private bool pin_pusten = false;  //samo jednom mozemo izvrsiti fixedupdate
    private GameManger_multiplayer nivo; //za pristup skrpti gamemanager_multiplayer

    private GameObject spawner1;  //igrac1
    private GameObject spawner2;  //igrac2

    private void Start() //povezujemo odredjene komponente sa promenljivima
    {
        nivo = GameObject.Find("Cela_scena").GetComponent<GameManger_multiplayer>();
        spawner1 = GameObject.Find("Player1").gameObject;
        spawner2 = GameObject.Find("Player2").gameObject;
    }


    private void Update()  //dodaje se sila pinu
    {
        if (gameObject.tag == "igrac1" && Input.GetKeyDown(KeyCode.LeftControl)) pusti_pin = true;
        else if (gameObject.tag == "igrac2" && Input.GetKeyDown(KeyCode.RightControl)) pusti_pin = true;
    }


    private void FixedUpdate()
    {
        if (pusti_pin == true && pin_pusten == false)   //pin_pusten sam stavio zbog toga da se ne bi slucajno taj pin dvaput ubrzao, znaci samo jednom program prolazi kroz ovaj deo koda
        {
            pin_pusten = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, brzina);
            pusti_pin = false;
            //kontrola izbacivanja sluzi ukoliko je ona setovana na true, da izbaci novi pin drugom igracu
            GameObject.Find("Cela_scena").GetComponent<Spawner_multiplayer>().kontrola_izbacivanja = true;
            //ovde se odredjuje smer, tj. sila  koja je prilagodjena  tom novom pinu, tj. da li pin ide ka gore ili ka dole :)
            if (GameObject.Find("Cela_scena").GetComponent<Spawner_multiplayer>().smer == true) GameObject.Find("Cela_scena").GetComponent<Spawner_multiplayer>().smer = false;
            else if (GameObject.Find("Cela_scena").GetComponent<Spawner_multiplayer>().smer == false) GameObject.Find("Cela_scena").GetComponent<Spawner_multiplayer>().smer = true;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)  //detektuje pinove  i zavrsava igru
    {
        if (collision.gameObject.CompareTag("igrac1") == true || collision.gameObject.CompareTag("igrac2") == true) igraci_za_gubljenje();

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Krug") == true)  //ukoliko je krug, ova skipta se brise, pin postaje child kruga, i poeni se povecavaju za 1
        {
            if(collision.gameObject.GetComponent<Rotate>().promeni_smer_kad_pin_udari == true) collision.gameObject.GetComponent<Rotate>().promeni_smer = true;
            if (collision.gameObject.GetComponent<Rotate>().pojaca_brzinu_kad_pin_udari == true) collision.gameObject.GetComponent<Rotate>().pomoc_za_varijaciju4();
            gameObject.transform.parent = collision.gameObject.transform;
            Destroy(gameObject.GetComponent<Pin_multiplayer>());
            GameManger_multiplayer.poeni++;
        }

        //ukoliko se sudare dva pina, zavrsava se igra
        if (collision.gameObject.CompareTag("igrac1") == true || collision.gameObject.CompareTag("igrac2") == true)
        {
            gameObject.transform.parent = collision.gameObject.transform;
            igraci_za_gubljenje();
        }

        FindObjectOfType<AudioManager>().Play("Hit");

    }

    private void igraci_za_gubljenje()
    {
        
        GameObject.FindObjectOfType<GameManger_multiplayer>().izmena = true;
        if (gameObject.CompareTag("igrac1"))  
            nivo.kontrola_levela = 1;   
        else nivo.kontrola_levela = 2;
       // Debug.Log(nivo.kontrola_levela);  
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GameObject.Find("Krug").GetComponent<Rotate>().enabled = false;
        GameObject.Find("Krug").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        spawner1.SetActive(false);
        spawner2.SetActive(false);
        GameObject.Find("Cela_scena").GetComponent<Spawner_multiplayer>().enabled = false;

    }

    


}
