using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Maze : ScriptableObject
{
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
	private int[,] maze;
	private double randomPercentage;
	private List<Vector2> terminalPoint= new List<Vector2>();
	private double difficultyPercentage;
	private System.Random rnda = new System.Random();

	int checkCollision(COORD pos)
	{
		int collisions = 0;
		if (pos.x < 0 || pos.x >= dim.x || pos.y < 0 || pos.y >= dim.y) collisions = 5;//error code
		else
		{
			for (int x = -1; x < 2; x += 2)
				if (pos.x + x >= 0 && pos.x + x < dim.x && maze[(int)(pos.y), (int)(pos.x + x)] == 1)
					collisions++;
			for (int y = -1; y < 2; y += 2)
				if (pos.y + y >= 0 && pos.y + y < dim.y && maze[(int)(pos.y + y), (int)(pos.x)] == 1)
					collisions++;
		}
		return collisions;
	}
	public Maze(int dimx, int dimy, int difficultyPercentage, int randomPercentage)
	{
		dim = new COORD(dimx, dimy);
		this.randomPercentage = (double)randomPercentage/100;
		this.difficultyPercentage = (double)difficultyPercentage/100;
		maze = new int[dimy, dimx];
		for (int i = 0; i < dim.y; i++)
		{
			for (int j = 0; j < dim.x; j++) maze[i, j] = 0;
		}
	}

	public void calculate()
	{
		COORD header = new COORD(rnda.Next() % (dim.x - 1) + 1, 0);
		Stack<COORD> chronology = new Stack<COORD>();
		int placedBlocks = 1;
		int failedAttempts = 0;
		maze[header.y, header.x] = 1;
		chronology.Push(new COORD(header));
		bool completed = false;
		do
		{
			//printMaze(10, 3);
			//system("pause >nul");
			bool back = true;
			COORD nextHeader = new COORD(0, 0);
			for (int attempts = 5; attempts > 0; attempts--)
			{
				nextHeader.copy(header);
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
				if (checkCollision(nextHeader) == 1 && maze[nextHeader.y, nextHeader.x] == 0 && nextHeader.y > 0 && (!completed || (nextHeader.y != dim.y - 1 && completed)))
				{
					header.copy(nextHeader);
					maze[header.y, header.x] = 1;
					chronology.Push(new COORD(header));
					placedBlocks++;
					back = false;
					if (header.y == dim.y - 1) completed = true;
					break;
				}
			}
			if (back)
			{
				for (int i = rnda.Next() % 5; i > 0 && chronology.Count > 1; i--) chronology.Pop();
				header.copy(chronology.Peek());
				back = false;
			}
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
			if(failedAttempts > 1000)
			{
				failedAttempts = 0;
				difficultyPercentage = (difficultyPercentage * 100 - 10) / 100;
			}
		} while (placedBlocks < dim.x * dim.y * difficultyPercentage || !completed);
		for (int i = 0, max = 0; i < randomPercentage * dim.x * dim.y; i++, max = 0)
		{
			
			do
			{
				header = new COORD(rnda.Next() % dim.x, rnda.Next() % (dim.y - 2) + 1);
				max++;
			} while (maze[header.y, header.x] == 1 && max < 50);
			maze[header.y, header.x] = 1;
		}

		for(int i = 0;i<dim.x;i++)
			for(int j = 0;j<dim.y;j++)
				if(checkCollision(new COORD(i,j)) == 1)
					terminalPoint.Add(new Vector2(j,i));


	}

	public int[,] getArrMaze()
	{
		return maze;
	}

	public List<Vector2> getTerminalPoint()
	{
		return terminalPoint;
	}

	/*public String getMaze()
	{
		int c = 0;
		string app = "";
		for (int i = 0; i < dim.y; i++)
		{
			for (int j = 0; j < dim.x; j++)
			{
				app += Convert.ToString(maze[i, j]) + " ";
				if (maze[i, j] == 1) c++;
			}
			app += "\n";
		}
		app += Convert.ToString(c);
		return app;
	}*/

};



