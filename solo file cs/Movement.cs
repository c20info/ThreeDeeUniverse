using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public CharacterController controller; 
    //componente che serve per gestire il movimento di un'oggetto nella scena

    private Vector3 velocity; 
    //velovità del player

    public Camera eyes; 
    //camera che si muove in posizione degli occhi e che permette di avere una visualizzazione in prima persona
    public Transform groundCheck;
    //punto che fa da ancora per il controllo dell'appoggio sul terreno

    public LayerMask groundMask;
    //definisce il valore del terreno da controllare per l'appoggio

    public int mouseSens;
    //definisce la sensibilità del mouse

    public int speed;
    //definisce la velocità del player

    private bool isGrounded;
    //è vero quando stà collidendo con il terreno

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //blocca il mouse al centro dello schermo
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);
        //controlla se l'oggetto sta collidendo con il terreno

        if(isGrounded && velocity.y < 0)
            velocity.y = -2;
        //reimposta la velocità ad un valore successivamente controllato
        


        //variabili per rilevare le varie posizioni del mouse e dei tasti del movimento:
        float x = Input.GetAxisRaw("Horizontal"); 
        float z = Input.GetAxisRaw("Vertical");
        float xM = Input.GetAxisRaw("Mouse X");
        float zM = -Input.GetAxisRaw("Mouse Y");

        Vector3 move = transform.right * x * speed + transform.forward * z * speed;
        //crea la nuova posizione dell'oggetto rispetto gli assi
        
        transform.Rotate(0, xM * mouseSens, 0);
        //ruota l'oggetto rispetto l'asse Y

        controller.Move(move * 5f * Time.deltaTime);
        //cambia la posizione dell'oggetto rispetto l'asse orizzonatle

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(1 * -2f * -6.81f);
        //controlla il salto
        

        velocity.y -= 9.81f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //cambia la posizione dell'oggetto rispetto l'asse verticale

        eyes.transform.Rotate(zM * mouseSens, 0, 0);
        //ruota gli occhi



    }
}
