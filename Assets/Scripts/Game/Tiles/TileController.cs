using Settings;
using UnityEngine;
using Zenject;

namespace Game.Tiles
{
    public class TileController
    {
        private Tile[,] tiles;
        private readonly Tile tilePrefab;
        private readonly Transform parent;
        private readonly BoardSettings boardSettings;

        [Inject]
        public TileController(Tile tilePrefab,Transform board,BoardSettings settings)
        {
            this.tilePrefab = tilePrefab;
            parent = board;
            boardSettings = settings;
            GenerateTiles();
        }

        private void GenerateTiles()
        {
            tiles = new Tile[boardSettings.width, boardSettings.height];
            for (int x = 0; x < boardSettings.width; x++)
            {
                for (int y = 0; y < boardSettings.height; y++)
                {
                    var tileGo = Object.Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                    tileGo.transform.SetParent(parent);
                    var tile = tileGo.GetComponent<Tile>();
                    tile.Init(x, y);
                    tiles[x, y] = tile;
                }
            }
        }
        
        public Tile GetClosestTileForDroppingDefender(Vector2 mousePos)
        {
            for (var i = 0; i < boardSettings.width; i++)
            for (var j = 0; j < boardSettings.height; j++)
                if (Vector2.Distance(mousePos, tiles[i, j].transform.position) < boardSettings.defenderPlacementThreshold &&
                    j <= boardSettings.defenceItemMaxPlacebleYIndex && tiles[i, j].GetTileState() == typeof(EmptyTileState))
                    return tiles[i, j];

            return null;
        }
        
        public Vector3 GetRandomUpmostTileSpawnPosition()
        {
            var randomColumn = Random.Range(0, boardSettings.width);
            var tilePos = tiles[randomColumn, boardSettings.height - 1].transform.position;
            return tilePos + boardSettings.boardSpawnOffset;
        }
    }
}
