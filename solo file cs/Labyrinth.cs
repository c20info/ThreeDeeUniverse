using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


//gestisce la creazione del labirinto e di tutti gli oggetti

public class Labyrinth : MonoBehaviour
{
    private int[,] labirintoPazzo;
    //contiene il labirinto da creare
    
    public Transform transPerson;
    //si riferisce la posizione del player

    private int xS,zS,xW,zW;
    //posizioni dei punti di inizio e fine

    public GameObject wall, hammer, torch;
    //contine i prefab a cui riferirsi per crare gli oggetti specificati

    private bool winEx = false,startEx = false;
    //sono vere se esistono rispettivamente i punti di inizio e fine(usate per contollo)

    public int enterPointLayer, 
               //contiene il layer che conerrà il blocco di vittoria

               mapGroundLayer,
               //contiene il layer dei blocchi apparenenti al labirinto

               difficulty, 
               //contine la percentuale di difficoltà

               randomPercentage;
               //contine la percentuale di rottura di blocchi random

    public GameObject mapp;
    //contiene un riferiemnto all'oggeto in cui è conenuto

    public Material material;
    //è il materiale base da usare

    private List<GameObject> hammers = new List<GameObject>(), torches = new List<GameObject>();
    //sono tutti gli oggetti in gioco

    private GameObject basement, win, start, startBasement, top;
    //sono le pareti che sconotrnano la mappa

    public int sizeX, sizeZ;
    //sono le dimansioni della mappa

    public int sizeZShifted;
    //sono le dimenzioni Z della mappa se si contano le 2 pareti aggiunte

    private Maze maze;
    //labirinto creato

    private System.Random rnda = new System.Random();
    //oggetto per la gestione dei random



    void Start()
    {
        //serve per leggere le informazioni di crazione del labirinto:
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/map.barucci";
        FileStream stream = new FileStream(path, FileMode.Open);
        MapData data = formatter.Deserialize(stream) as MapData;
        stream.Close();

        //definisce il labirinto:
        sizeX = data.dimX;
        sizeZ = data.dimY;
        difficulty = data.difficulty;
        randomPercentage = data.random;
        sizeZShifted = sizeZ + 2;

        //creao gli oggetti fisici che costituiscono il labirinto:
        crea2(sizeX,sizeZ);
        createMaze();
        createMap(); 
        
        
    }

    void createMap()
    {
        /* 
         * distruggi dst
         * selezziona slz
         * spawnable object spo
         */

        //pavinemto:
        basement = GameObject.CreatePrimitive (PrimitiveType.Cube);
            basement.transform.parent = mapp.transform;
            basement.transform.position = new Vector3 (0 , 0f, 2); 
            basement.transform.localScale = new Vector3 (sizeX*2, 1, sizeZShifted * 2);
            basement.GetComponent<Renderer>().material = material;
            basement.layer = mapGroundLayer;
            basement.name = "basement";

        //punto di vittoria:
        win = GameObject.CreatePrimitive ( PrimitiveType.Cube );
            win.transform.parent = mapp.transform;
            win.transform.position = new Vector3 (xW * 2 - sizeX + 1  , 0.01f, zW * 2 - sizeZShifted + 3); 
            win.transform.localScale = new Vector3 (2,1,2);
            win.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
            win.layer = enterPointLayer;

        //punto di inizio:
        start = GameObject.CreatePrimitive ( PrimitiveType.Cube );
            start.transform.parent = mapp.transform;
            start.transform.localScale = new Vector3 (2,1,2);
            start.transform.position = new Vector3 (xS * 2 - sizeX + 1 , 0.01f, zS * 2 - sizeZShifted + 3); 
            start.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            start.layer = mapGroundLayer;

        //zona di inizio:
        startBasement = GameObject.CreatePrimitive ( PrimitiveType.Cube );
            startBasement.transform.parent = mapp.transform;
            startBasement.transform.localScale = new Vector3 (8, 1, 8);
            startBasement.transform.position = new Vector3(xS * 2 - sizeX - 4f, 0f, zS * 2 - sizeZShifted + 3);
            startBasement.GetComponent<Renderer>().material = material;  
            startBasement.layer = mapGroundLayer;

        //tetto:
        top = GameObject.CreatePrimitive (PrimitiveType.Cube);
            top.transform.parent = mapp.transform;
            top.transform.position = new Vector3 (0 , 5f, 2); 
            top.transform.localScale = new Vector3 (sizeX*2, 1, sizeZShifted * 2);
            top.GetComponent<Renderer>().material = material;
            top.layer = 3;


        //aggiungo glo oggetti di giococ:
        for(int i = 0;i<maze.getTerminalPoint().Count;i++)
        {
            if (maze.getTerminalPoint()[i].x != xS && maze.getTerminalPoint()[i].y != zS && maze.getTerminalPoint()[i].x != xW && maze.getTerminalPoint()[i].y != zW && maze.getTerminalPoint()[i].x > 0)
            {
                if (rnda.Next() % 100 < 50)
                {
                    GameObject terminlP = Instantiate(hammer, new Vector3(maze.getTerminalPoint()[i].x * 2 - sizeX + 1, 1.5f, maze.getTerminalPoint()[i].y * 2 - sizeZShifted + 5), mapp.transform.rotation);
                    terminlP.AddComponent<Tag>();
                    terminlP.GetComponent<Tag>().addTag("spo");
                    hammers.Add(terminlP);
                    maze.getTerminalPoint()[i] = new Vector2(-1, -1);
                }
            }
        }

        for (int i = 0; i < maze.getTerminalPoint().Count; i++)
        {
            if (maze.getTerminalPoint()[i].x != xS && maze.getTerminalPoint()[i].y != zS && maze.getTerminalPoint()[i].x != xW && maze.getTerminalPoint()[i].y != zW && maze.getTerminalPoint()[i].x > 0)
            {
                if (rnda.Next() % 100 < 50)
                {
                    GameObject terminlP = Instantiate(torch, new Vector3(maze.getTerminalPoint()[i].x * 2 - sizeX + 1, 3f, maze.getTerminalPoint()[i].y * 2 - sizeZShifted + 5), mapp.transform.rotation);
                    terminlP.AddComponent<Tag>();
                    terminlP.GetComponent<Tag>().addTag("spo");
                    torches.Add(terminlP);
                    maze.getTerminalPoint()[i] = new Vector2(-1, -1);
                }
            }
        }

    }

    void createMaze()
    {

        for(int x = 0; x<sizeX; x++)
        {

            for(int z = 0;z<sizeZ + 2;z++)
            {
                if(labirintoPazzo[x,z] == 0) 
                {
                    //creo il labirinto
                    GameObject[,,] matrApp = new GameObject[2,sizeX,sizeZ + 2];
                    matrApp[0,x,z] = Instantiate(wall, new Vector3 (x*2-sizeX + 1, 1.5f, z*2-sizeZ + 1), Quaternion.Euler(90, (rnda.Next() % 4 + 1) * 90f, mapp.transform.rotation.z));
                    matrApp[0,x,z].transform.parent = mapp.transform;
                    matrApp[1,x,z] = Instantiate(wall, new Vector3 (x*2-sizeX + 1, 3.5f, z*2-sizeZ + 1), Quaternion.Euler(90, (rnda.Next() % 4 + 1) * 90f, mapp.transform.rotation.z));
                    matrApp[1,x,z].transform.parent = mapp.transform;

                    //aggiungo i tag gli oggetti labirinto
                    if (z != 0 && z != sizeZ + 1 && x != 0 && x != sizeX - 1)
                    {
                        matrApp[0, x, z].AddComponent<Tag>();
                        matrApp[0, x, z].GetComponent<Tag>().addTag("dst");
                        matrApp[0, x, z].GetComponent<Tag>().addTag("slz");

                        matrApp[1, x, z].AddComponent<Tag>();
                        matrApp[1, x, z].GetComponent<Tag>().addTag("dst");
                        matrApp[1, x, z].GetComponent<Tag>().addTag("slz");
                    }


                }
                //definisco inizio e fine
                if(x == sizeX - 1 && labirintoPazzo[x,z]==1 && !winEx)
                {
                    zW = z;
                    xW = x;
                    winEx = true;
                }
                else if(x == 0 && labirintoPazzo[x,z]==1 && !startEx)
                {
                    zS = z;
                    xS = x;
                    transPerson.position = new Vector3(xS * 2 - sizeX - 4f, 1f, zS * 2 - sizeZ);
                    startEx = true;
                }

                    
                    
            }
                    
        }



    }


    //creo il labirinto nell'array
    void crea2(int w,int h)
    {
        
        maze = new Maze(h, w, difficulty, randomPercentage);
        labirintoPazzo = new int[w, h + 2];
        maze.calculate();
        for (int y = 0; y < w     ; y++)
        for (int x = 0; x < h + 2 ; x++)
            if (x == 0 || x == h + 1)
                labirintoPazzo[y, x] = 0;
            else
                labirintoPazzo[y, x] = maze.getArrMaze()[y, x - 1];
    }
}
