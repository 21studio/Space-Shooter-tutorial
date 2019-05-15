using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;

    void Start ()
    {
        // 14 Counting Points 참고, 잘 이해 안됨!!
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent <GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        
        // 09 Creating hazards 7분 40초인데, 왜 나타나자 마자 사라지는지??
        if (other.tag == "Boundary")
        {
            Debug.Log(other.name);
            return;
        }
        
        Instantiate(explosion, transform.position, transform.rotation); //소행성 폭발 프리팹

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation); //본체 폭발 프리팹
            gameController.GameOver ();
        }

        gameController.AddScore (scoreValue);

        Debug.Log(other.name);
        Destroy(other.gameObject); //총알을 파괴        

        Debug.Log(gameObject);
        Destroy(gameObject); //소행성을 파괴                
        
    }
}