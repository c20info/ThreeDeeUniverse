using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creazione della mappa del menu principale
public class MapManagerScript : MonoBehaviour
{
	private Maze maze;
	//contiene il labirinto di sfondo
	public int dimX;
	public int dimY;
	//dimensione
	public int difficulty;
	//percentuale di difficoltà
	public float scale;
	//scala dei prefab
	public int posX = 0;
	public int posY = 0;
	//poosizione
	private GameObject map;
	//riferimento al GameObject della mappa
	public Material material;
	//materiale usato per creare il labirinto

    void Start()
    {
		map = this.gameObject;
		maze = new Maze(dimX, dimY, difficulty, 5);
		maze.calculate();
		List<GameObject> blocks = new List<GameObject>();
		//creazione istanze

		GameObject basement = GameObject.CreatePrimitive(PrimitiveType.Cube);
		basement.transform.position = new Vector3(posX + dimX/2*scale,-1, posY + dimY/2*scale);
		basement.transform.localScale = new Vector3(dimX*scale, 1, dimY*scale);
		basement.transform.parent = map.transform;
		//creazione rettangolo che forma la base del labirinto

		for(int y = 0; y < dimY; y++)
        {
			for(int x = 0; x < dimX; x++)
            {
				if (maze.getArrMaze()[y, x] == 0)
				{
					blocks.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
					blocks[blocks.Count - 1].transform.position = new Vector3(x*scale + posX + scale/2, 0, y*scale + posY  + scale/2);
					blocks[blocks.Count - 1].transform.localScale = new Vector3(scale, scale, scale);
					blocks[blocks.Count - 1].transform.parent = map.transform;
					blocks[blocks.Count - 1].GetComponent<Renderer>().material = material;

				}
			}
        }
		//settaggio e creazione dei dei blocchi che formano le pareti in base al contenuto dell istanza maze (algoritmo per generazione del labirinto)
    }
	//crea dei cubi sulla mappa per formare il labirinto

};

