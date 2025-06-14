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
        
        private Collider2D draggableItemCollider;

        private Tile assignedTile;

        public Vector3 InitialPos { get; set; }
        
        private void Awake()
        {
            draggableItemCollider = GetComponent<Collider2D>();
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            draggableItemCollider.enabled = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (assignedTile != null)
            {
                return;
            }
            draggableItemCollider.enabled = true;
            var closestTile = GetClosestTile();
            if (closestTile != null)
            {
                PutOnTile(closestTile);
                defender.Activate();
            }
            else
            {
                assignedTile = null;
                transform.position = InitialPos;
                defender.Deactivate();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (assignedTile != null)
            {
                return;
            }
            var mainCamera = Camera.main;
            if (mainCamera != null)
            {
                var mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = mousePos;
            }
        }
        
        private Tile GetClosestTile()
        {
            var mainCamera = Camera.main;
            if (mainCamera == null) 
                return null;

            var mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            return BoardManager.Instance.GetClosestTileForDroppingDefender(mousePos);
        }

        private void PutOnTile(Tile tile)
        {
            transform.position = tile.transform.position;
            tile.ChangeState(typeof(NonEmptyTileState));
            assignedTile = tile;
        }
    }
}