using Settings;
using UnityEngine;

namespace Game.Tiles
{
    public class TileController
    {
        private Tile[,] tiles;
        private readonly Tile tilePrefab;

        public TileController(Tile tilePrefab)
        {
            this.tilePrefab = tilePrefab;
        }

        public void GenerateTiles(int width, int height)
        {
            tiles = new Tile[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var tileGO = Object.Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                    var tile = tileGO.GetComponent<Tile>();
                    tile.Init(x, y);
                    tiles[x, y] = tile;
                }
            }
        }
        
        public Tile GetClosestTileForDroppingDefender(Vector2 mousePos)
        {
            for (var i = 0; i < GameSettingsManager.Instance.boardSettings.width; i++)
            for (var j = 0; j < GameSettingsManager.Instance.boardSettings.height; j++)
                if (Vector2.Distance(mousePos, tiles[i, j].transform.position) < GameSettingsManager.Instance.boardSettings.defenderPlacementThreshold &&
                    j <= GameSettingsManager.Instance.boardSettings.defenceItemMaxPlacebleYIndex && tiles[i, j].GetTileState() == typeof(EmptyTileState))
                    return tiles[i, j];

            return null;
        }
        
        public Vector3 GetRandomUpmostTileSpawnPosition()
        {
            var randomColumn = Random.Range(0, GameSettingsManager.Instance.boardSettings.width);
            var tilePos = tiles[randomColumn, GameSettingsManager.Instance.boardSettings.height - 1].transform.position;
            return tilePos + GameSettingsManager.Instance.boardSettings.boardSpawnOffset;
        }
    }
}
