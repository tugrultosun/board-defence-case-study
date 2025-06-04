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

        private Tile assignedTile;

        public Vector3 initialPos;
        
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            _collider.enabled = false;
            if (assignedTile != null)
            {
                assignedTile.ChangeState(typeof(EmptyTileState));
                assignedTile = null;
                defender.Deactivate();
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
                assignedTile = closestTile;
                defender.Activate();
            }
            else
            {
                assignedTile = null;
                transform.position = initialPos;
                defender.Deactivate();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }
    }
}