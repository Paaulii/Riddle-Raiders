using UnityEngine;

public class Laser : MonoBehaviour
{
   public LayerMask layersToHit;
   public Direction direction;
   public LineRenderer laser;

   void Awake()
   {
      laser.SetPosition(0, transform.position);
   }
   
   private void Update()
   {
      Vector3 dir = GetLaserDirection(direction);
      RaycastHit hit;
      
      if (Physics.Raycast(transform.position,dir, out hit, Mathf.Infinity, layersToHit))
      {
         laser.SetPosition(1, hit.point);
      }
      else
      {
         laser.SetPosition(1, transform.position + dir * 1000);
      }
   }

   Vector3 GetLaserDirection(Direction direction)
   {
      switch (direction)
      {
         case Direction.Left:
            return -transform.right;
         case Direction.Right:
            return transform.right;
         case Direction.Down:
            return -transform.up;
         case Direction.Up:
            return transform.up;
         default:
            return Vector3.zero;
      }
   }
}
