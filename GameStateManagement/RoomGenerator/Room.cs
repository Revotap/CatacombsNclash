using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatacombsNClash.RoomGenerator
{
    internal class Room
    {
        private int width;
        private int height;

        private Tile[,] roomindex;


        public Room(int width, int height) { 
            this.width = width;
            this.height = height;

             roomindex = new Tile[height,width];
            init(roomindex);
        }

        /*
         In der Init Methode wird der Raum logisch initialisiert
        Hierfür werden die "Ränder" also alle Felder mit einem Index von 
        [0;j][i;0][Länge;j][i;Länge] mit 0 besetzt. Der Rest bekommt eine 1
        bspw
        0000000000
        0111111110
        0111111110
        0111111110
        0000000000

        0 = Walltiles
        1 = Floortiles

        Das hinzufügen von Hindernissen ist ein ToDo-Task
         
         */

        public init(Tile[,] tilearray)
        {
            for (int i = 0; i < tilearray.GetLength(0); i++)
            {
                for(int j = 0; j < tilearray.GetLength(1); j++)
                {
                    if(i == 0 || j == 0 || i == tilearray.GetLength(0) || j == tilearray.GetLength(1) ) // i == tilearray.GetLength(0) || j == tilearray.GetLength(1)
                    {
                        tilearray[i,j]=0;
                    } else tilearray[i,j] = 1; 
                }
            }
        }            

        /*
         Zuerst die Mitte vom Raum ermitteln
         von der Mitte aus dann den Ursprungs des Raums berechnen 
         Tile Size 64x64 px -> 
         Mitte.y - ((höhe/2)*64) || Mitte.x - ((breite/2)*64) =
         set Roomorigin 
         von dem Punkt auch werden die Kacheln nach und gezeichnet.
         
         */
        public void Draw()
        {

        }
    }
}
