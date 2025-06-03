using Game;
using Game.Board;
using Game.Defender;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Draggable
{
    [RequireComponent(typeof(Collider2D))]
    public class DraggableItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private Collider2D _collider;

        private Tile _tile;

        public Vector3 initialPos;

        [SerializeField] private Defender defender;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            _collider.enabled = false;
            if (_tile != null)
            {
                _tile.IsEmpty = true;
                _tile.ContainsDefender = false;
                _tile = null;
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
                closestTile.IsEmpty = false;
                closestTile.ContainsDefender = true;
                _tile = closestTile;
            }
            else
            {
                _tile = null;
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