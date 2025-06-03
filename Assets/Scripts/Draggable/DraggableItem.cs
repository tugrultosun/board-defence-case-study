using Game;
using Game.Board;
using Game.Defender;
using Game.Tiles;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Draggable
{
    [RequireComponent(typeof(Collider2D))]
    public class DraggableItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private Defender defender;
        
        private Collider2D _collider;

        private Tile tile;

        public Vector3 initialPos;
        
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            _collider.enabled = false;
            if (tile != null)
            {
                tile.ChangeState(typeof(EmptyTileState));
                tile.ContainsDefender = false;
                tile = null;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _collider.enabled = true;
            var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var closestTile = BoardManager.Instance.GetClosestTileForDroppingDefender(mousePos);
            if (closestTile != null)
            {
                transform.position = closestTile.transform.position;
                closestTile.ChangeState(typeof(NonEmptyTileState));
                closestTile.ContainsDefender = true;
                tile = closestTile;
            }
            else
            {
                tile = null;
                transform.position = initialPos;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }
    }
}