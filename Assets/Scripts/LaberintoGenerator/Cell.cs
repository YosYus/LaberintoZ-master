public class Cell 
{
   
    public bool Oeste, Norte, Este, Sur;    // Mur Oest, Nor, Est et Sur.
    public bool visitada;                       // Permeite de saber si una casilla ha sido visitada o no

    public int xPos, zPos;                      // Posicion en X y en Z.

    // Constructor.
    public Cell (bool west, bool north, bool est, bool south, bool visited)
    {
        this.Oeste = west;
        this.Norte = north;
        this.Este = est;
        this.Sur = south;
        this.visitada = visited;
    }

}
