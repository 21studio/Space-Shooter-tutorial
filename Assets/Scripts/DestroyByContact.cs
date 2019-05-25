using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController; // 인스턴스에 대한 레퍼런스를 보관할 변수를 작성

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
        // Boundary 안에 생성되자마자 사라지는 상황을 방지. 리턴하여 빠져나옴
        if (other.tag == "Boundary")
        {            
            return;
        }
        
        Instantiate(explosion, transform.position, transform.rotation); //소행성 폭발 프리팹

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation); //player 폭발 프리팹
            gameController.GameOver ();
        }

        gameController.AddScore (scoreValue);

        Debug.Log(other.name);
        Destroy(other.gameObject); //총알을 파괴        

        Debug.Log(gameObject);
        Destroy(gameObject); //소행성을 파괴                
        
    }
}