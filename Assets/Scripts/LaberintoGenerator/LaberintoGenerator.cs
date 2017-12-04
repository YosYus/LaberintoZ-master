using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaberintoGenerator : MonoBehaviour 
{
  
    public int width, height; //Largo et altura del laberinto.

    public VisualCell visualCellPrefab; // Prefab que sirve de modelo para la instanciacion

    public Cell[,] casillas; //Tabla de cell's de dos dimensiones.

    private Vector2 _randomCellPos; //Posicion de la casilla aleatoire donde empezar la generacion.
    private VisualCell visualCellInst; //  la copia de prefab de la  instanciacion.

    private List<CellAndRelativePosition> vecinas; //Lista des casillas vecinas

	public Zombie zombie;
   
    void Start ()
    {
        casillas = new Cell[width, height]; //Initialisation de tabla
        Init(); 
    }

    void Init ()
    {
        for(int i = 0; i < width; i++)
        {

            for(int j = 0; j < height; j++)
            {

                casillas[i, j] = new Cell(false, false, false, false, false);
                casillas[i, j].xPos = i;
                casillas[i, j].zPos = j;
				zombie= new Zombie( i, j);
            }
        }
        RandomCell(); 
		RandomZombieCell ();

        InitVisualCell(); 
    }

    void RandomCell ()
    {
        //Recupera una pos X y Y aleatoria
        _randomCellPos = new Vector2((int)UnityEngine.Random.Range(0, width), (int)UnityEngine.Random.Range(0, height));

        //Lanza la funcion GeneratorLab con la pos X,Y 
        GeneratorLab((int)_randomCellPos.x, (int)_randomCellPos.y); 
    }


	void RandomZombieCell ()
	{
		//Recupera una pos X y z aleatoria
		_randomCellPos = new Vector2((int)UnityEngine.Random.Range(0, width), (int)UnityEngine.Random.Range(0, height));

		//Lanza la funcion GeneratorLab con la pos X,z
		zombie= new Zombie( 9, 8);
	}

    void GeneratorLab (int x, int y)
    {
        //	Debug.Log("Doing " + x + " " + y);
        Cell currentCell = casillas[x, y]; //casilla corriente
        vecinas = new List<CellAndRelativePosition>(); //Initialise la liste
        if(currentCell.visitada == true) return;
        currentCell.visitada = true;

        if(x + 1 < width && casillas[x + 1, y].visitada == false)
        { //Si on est pas a la largeur limite max du laby et que la cellule de droite n'est pas visiter alors on peut aller a droite
            vecinas.Add(new CellAndRelativePosition(casillas[x + 1, y], CellAndRelativePosition.Direction.Este)); //Ajoute la cellule voisine de droite dans la liste des voisins
        }

        if(y + 1 < height && casillas[x, y + 1].visitada == false)
        { //Si on est pas a la longueur limite du laby et que la cellule du bas n'est pas visiter alors on peut aller en bas
            vecinas.Add(new CellAndRelativePosition(casillas[x, y + 1], CellAndRelativePosition.Direction.Sur)); //Ajoute la cellule voisine du bas dans liste des voisins
        }

        if(x - 1 >= 0 && casillas[x - 1, y].visitada == false)
        { //Si on est pas a la largeur limite mini du laby et que la cellule de gauche n'est pas visiter alors on peut aller a gauche
            vecinas.Add(new CellAndRelativePosition(casillas[x - 1, y], CellAndRelativePosition.Direction.Oeste)); //Ajoute la cellule voisine de gauche dans la liste des voisins
        }

        if(y - 1 >= 0 && casillas[x, y - 1].visitada == false)
        { //Si on est pas a la longueur limite mini du laby et que la cellule du haut n'est pas visiter alors on peut aller en haut
            vecinas.Add(new CellAndRelativePosition(casillas[x, y - 1], CellAndRelativePosition.Direction.Norte)); //Ajoute la cellule voisine du haut dans la liste des voisins
        }

        if(vecinas.Count == 0) return;  // Si il y a 0 voisins dans la liste on sort de la méthode.

        vecinas.Shuffle(); // Mezcla la liste de vecinas

        foreach(CellAndRelativePosition selectedcell in vecinas)
        {
            if(selectedcell.direction == CellAndRelativePosition.Direction.Este)
            { // A droite
                if(selectedcell.cell.visitada) continue;
                currentCell.Este = true; //Detruit le mur de droite de la cellule courante
                selectedcell.cell.Oeste = true; //Detruit le mur de gauche de la cellule voisine choisie
                GeneratorLab(x + 1, y); //Relance la fonction avec la position de la cellule voisine
            }

            else if(selectedcell.direction == CellAndRelativePosition.Direction.Sur)
            { // En bas
                if(selectedcell.cell.visitada) continue;
                currentCell.Sur = true; //Detruit le mur du bas de la cellule courante
                selectedcell.cell.Norte = true; //Detruit le mur du haut de la cellule voisine choisie
                GeneratorLab(x, y + 1); //Relance la fonction avec la position de la cellule voisine
            }
            else if(selectedcell.direction == CellAndRelativePosition.Direction.Oeste)
            { // A gauche
                if(selectedcell.cell.visitada) continue;
                currentCell.Oeste = true; //Detruit le mur de gauche de la cellule courante
                selectedcell.cell.Este = true; //Detruit le mur de droite de la cellule voisine choisie
                GeneratorLab(x - 1, y); //Relance la fonction avec la position de la cellule voisine
            }
            else if(selectedcell.direction == CellAndRelativePosition.Direction.Norte)
            { // En haut
                if(selectedcell.cell.visitada) continue;
                currentCell.Norte = true; //Detruit le mur du haut de la cellule courante
                selectedcell.cell.Sur = true; //Detruit le mur du bas de la cellule voisine choisie
                GeneratorLab(x, y - 1); //Relance la fonction avec la position de la cellule voisine
            }
        }


    }

    void InitVisualCell ()
    {
        // Initialise mes cellules visuel et detruit les murs en fonction des cellules virtuel
        foreach(Cell cell in casillas)
        {

            visualCellInst = Instantiate(visualCellPrefab, new Vector3(cell.xPos * 3, 0, height * 3f - cell.zPos * 3), Quaternion.identity) as VisualCell;
            visualCellInst.transform.parent = transform;
            visualCellInst.Norte.gameObject.SetActive(!cell.Norte);
            visualCellInst.Sur.gameObject.SetActive(!cell.Sur);
            visualCellInst.Este.gameObject.SetActive(!cell.Este);
            visualCellInst.Oeste.gameObject.SetActive(!cell.Oeste);

            visualCellInst.transform.name = cell.xPos.ToString() + "_" + cell.zPos.ToString();
        }

    }

}
