using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    void Update ()
    {
        // 07 Shooting shots 7~12분까지 내용 참고 !!!
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate; // Time.time(시간)에 fireRate(발사주기)를 더하여 연속 발사를 막는다

            // 일단 발사한 탄환에 대해 아무것도 할 필요가 없어 레퍼런스 필요없다. (아래 주석 내용)
            // GameObject clone = 
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // as GameObject;
            GetComponent<AudioSource>().Play();
        }
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        
        // rigidbody.velocity = movement;
        Rigidbody rb;
        rb = GetComponent<Rigidbody>();
        rb.velocity = movement * speed;
        //rb.AddForce(movement);
        rb.position = new Vector3
            (
                Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
                0.0f, 
                Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
            );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
