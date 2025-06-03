using UnityEngine;

namespace Game
{
    public class Tile : MonoBehaviour
    {
        public int xCoord;
        public int yCoord;

        public void Init(int x, int y)
        {
            xCoord = x;
            yCoord = y;
            name = "Tile x:" + xCoord + " y:" + yCoord;
        }
    }
}
