﻿using UnityEngine;

public class Bubble_Hazard : MonoBehaviour
{
    [Tooltip("Use 1-10 for Speed")]
    public float moveSpeed = 0;
    public GameObject player;
    public GameObject player2;
    public bool Overlap;
    public bool Overlap2;
    public GameObject Coral;
    public float Meter = 0;
    public GameObject parent;
    private float Transparency = .30f;
    public float Meter2 = 0;
    public Spawner Spawner;
    public float TimeToDeath;
    public float KillY;

    private void Start()
    {
        player = PlayerValues.GetPlayer(0).gameObject;//GameObject.FindGameObjectWithTag("Player");
        player2 = PlayerValues.GetPlayer(1).gameObject;//GameObject.FindGameObjectWithTag("Player2");
        Coral = GameObject.FindGameObjectWithTag("Coral");
        Destroy(gameObject, TimeToDeath);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        PlayerValues playerValues = other.gameObject.GetComponent<PlayerValues>();
        if (playerValues != null)
        {     
            Debug.Log("Bubble bobble");
            if (playerValues.Id == 0)
            {
                player.GetComponent<PlayerMovement>().enabled = false;
                player.transform.Translate(0, moveSpeed * Time.deltaTime, 0);
                Overlap = true;
            }
    
            if (playerValues.Id == 1)
            {
                player2.GetComponent<PlayerMovement>().enabled = false;
                player2.transform.Translate(0, moveSpeed * Time.deltaTime, 0);
                Overlap2 = true;
            }
        }

        if (other.tag == "Coral")
        {
            Destroy(transform.root.gameObject);

            if (Overlap)
            {
                Meter = 0;
                player.GetComponent<PlayerValues>().Die();
                player.GetComponent<PlayerMovement>().enabled = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Overlap = false;
        }

    }

    void Update()
    {
        parent.transform.Translate(0, moveSpeed * Time.deltaTime, 0);

        if (Overlap)
        {
            player.transform.position = gameObject.transform.position;
            if (player.transform.position.y > KillY)
            {
                Destroy(gameObject);
                player.GetComponent<PlayerValues>().Die();
            }
        }


        if (Overlap2)
        {
            player2.transform.position = gameObject.transform.position;
            if (player2.transform.position.y > KillY)
            {
                Destroy(gameObject);
                player2.GetComponent<PlayerValues>().Die();
            }
        }

        if (Overlap)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Meter = Meter + 1;
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Transparency - .1f);
            }
        }

        if (Overlap2)
        {
            if (Input.GetButtonDown("Submit"))
            {
                Meter2 = Meter2 + 1;
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Transparency - .1f);
            }
        }

        if (Meter >= 30)
        {
            Destroy(transform.root.gameObject);
            player.GetComponent<PlayerMovement>().enabled = true;
            Meter = 0;
            print("You broke free");
        }

        if (Meter2 >= 30)
        {
            Destroy(transform.root.gameObject);
            player2.GetComponent<PlayerMovement>().enabled = true;
            Meter2 = 0;
            print("You broke free");
        }


    }



}
