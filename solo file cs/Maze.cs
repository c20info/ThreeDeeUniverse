using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//crea e contiene una matrice di interi che forma la struttura del labirinto
public class Maze : ScriptableObject
{
	//oggetto per gestire un punto bidimensionale
	public class COORD
	{
		public COORD(int x, int y) 
		{
			this.x = x;
			this.y = y;
		}
		public COORD(COORD c)
		{
			x = c.x;
			y = c.y;
		}
		public int x;
		public int y;
		public void copy(COORD c)
		{
			x = c.x;
			y = c.y;
		}
	}


	public COORD dim;
	//dimensione di blocchi
	private int[,] maze;
	//matrice contenente i valori per il labirinto (0=muro | 1=passaggio)
	private double randomPercentage;
	//percentuale per il piazzamento di passaggi randomici
	private double difficultyPercentage;
	//percentuale del riempimento di passaggi nella mappa
	private List<Vector2> terminalPoint = new List<Vector2>();
	//lista di vicoli cechi (confinanti con un solo altro passaggio)
	private System.Random rnda = new System.Random();
	//istanza Random

	public Maze(int dimx, int dimy, int difficultyPercentage, int randomPercentage)
	{
		dim = new COORD(dimx, dimy);
		this.randomPercentage = (double)randomPercentage / 100;
		this.difficultyPercentage = (double)difficultyPercentage / 100;
		maze = new int[dimy, dimx];
		for (int i = 0; i < dim.y; i++)
		{
			for (int j = 0; j < dim.x; j++) maze[i, j] = 0;
		}
	}
	//costruttore che crea il labirinto vuoto e setta i parametri passati da input

	int checkCollision(COORD pos)
	{
		int collisions = 0;
		if (pos.x < 0 || pos.x >= dim.x || pos.y < 0 || pos.y >= dim.y) collisions = 5;//error code, se il punto è fuori dalla mappa
		else
		{
			for (int x = -1; x < 2; x += 2)
				if (pos.x + x >= 0 && pos.x + x < dim.x && maze[(int)(pos.y), (int)(pos.x + x)] == 1)
					collisions++;
			for (int y = -1; y < 2; y += 2)
				if (pos.y + y >= 0 && pos.y + y < dim.y && maze[(int)(pos.y + y), (int)(pos.x)] == 1)
					collisions++;
			//controlla se nei punti adiacenti ci sono dei passaggi
		}
		return collisions;
	}
	//controlla quanti passaggi sono affiancati ad un punto 

	public void calculate()
	{
		COORD header = new COORD(rnda.Next() % (dim.x - 1) + 1, 0);
		Stack<COORD> chronology = new Stack<COORD>();
		int placedBlocks = 1;
		int failedAttempts = 0;
		maze[header.y, header.x] = 1;
		chronology.Push(new COORD(header));
		bool completed = false;
		//settaggi iniziali dell header, cronologia, blocchi piazzati ecc

		do
		{
			bool back = true;
			//si attiva per retrocedere con la posizione
			COORD nextHeader = new COORD(0, 0);
			//contiene il blocco successivo da piazzare

			for (int attempts = 5; attempts > 0; attempts--)
			{
				nextHeader.copy(header);
				//il prossimo punto si setta sull attuale

				int rnd = rnda.Next() % 4;
				switch (rnd)
				{
					case 0:
					case 2:
						nextHeader.x += rnd - 1;
						break;
					case 1:
					case 3:
						nextHeader.y += rnd - 2;
						break;
				}
				//randomicamente si sposta di una posizione

				if (checkCollision(nextHeader) == 1 && maze[nextHeader.y, nextHeader.x] == 0 && nextHeader.y > 0 && (!completed || (nextHeader.y != dim.y - 1 && completed)))
				{
					header.copy(nextHeader);
					maze[header.y, header.x] = 1;
					chronology.Push(new COORD(header));
					placedBlocks++;
					back = false;
					if (header.y == dim.y - 1) completed = true;
					//se il punto è sull ultima riga, quella di arrivo, si setta su completo

					break;
				}
				//controlla che il punto non collida con niente o che non sia completo, poichè ci può essere solo un arrivo, e se è giusto back si disattiva e aggiunge il punto alla matrice e alla cronologia
			}
			//prova 5 volte a trovare un successivo passaggio vicino, se non trova niente back rimane attivo per tornare indietro

			if (back)
			{
				for (int i = rnda.Next() % 5; i > 0 && chronology.Count > 1; i--) chronology.Pop();
				header.copy(chronology.Peek());
				back = false;
			}
			//torna indietro di un numero random di posizioni piazzate

			if (chronology.Count == 1)
			{
				failedAttempts++;
				do
				{
					nextHeader = new COORD(rnda.Next() % dim.x, rnda.Next() % (dim.y - 1));
				} while (maze[nextHeader.y, nextHeader.x] == 0);
				header.copy(nextHeader);
				chronology.Push(new COORD(header));
			}
			//se si retrocede fino al punto di arrivare all inizio cerca un altro punto random da cui ripartire

			if(failedAttempts > 1000)
			{
				failedAttempts = 0;
				difficultyPercentage = (difficultyPercentage * 100 - 10) / 100;
			}
			//se non trova un passaggio da piazzare per più di 1000 volte abbassa la difficoltà

		} while (placedBlocks < dim.x * dim.y * difficultyPercentage || !completed);
		//generà dei passaggi per creare il labirinto finchè non c è un uscita e finche i blocchi piazzati sono minori del calcolo con la percentuale di difficoltà

		for (int i = 0, max = 0; i < randomPercentage * dim.x * dim.y; i++, max = 0)
		{
			
			do
			{
				header = new COORD(rnda.Next() % dim.x, rnda.Next() % (dim.y - 2) + 1);
				max++;
			} while (maze[header.y, header.x] == 1 && max < 50);
			//finchè non trova un blocco pieno da rendere vuoto o finche non supera i 50 tentativi
			maze[header.y, header.x] = 1;
		}
		//con la percentuale del random piazza a caso un certo numero di passaggi

		for(int i = 0;i<dim.x;i++)
			for(int j = 0;j<dim.y;j++)
				if(checkCollision(new COORD(i,j)) == 1)
					terminalPoint.Add(new Vector2(j,i));
		//controlla tutti i blocchi per trovare vicoli cechi e gli aggiunge alla lista

	}
	//algoritmo che crea randomicamente un labirinto in base ai parametri dell oggetto

	public int[,] getArrMaze()
	{
		return maze;
	}
	//restituisce la matrice con i dati del labirinto

	public List<Vector2> getTerminalPoint()
	{
		return terminalPoint;
	}
	//restituisce la lista di punti terminali

};



