using System;
using Game.Tiles;
using States;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class Tile : MonoBehaviour
    {
        public int xCoord;

        public int yCoord;
        public bool ContainsDefender { get; set; }
        
        private StateManager tileStateManager;

        public void Init(int x, int y)
        {
            tileStateManager = new StateManager();
            tileStateManager.AddState(new EmptyTileState());
            tileStateManager.AddState(new NonEmptyTileState());
            xCoord = x;
            yCoord = y;
            tileStateManager.ChangeState(typeof(EmptyTileState));
            name = "Tile x:" + xCoord + " y:" + yCoord;
        }

        public void ChangeState(Type type)
        {
            tileStateManager.ChangeState(type);
        }

        public Type GetTileState()
        {
            return tileStateManager.GetCurrentStateType();
        }
    }
}