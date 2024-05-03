using UnityEngine;

public class ShootLaser : MonoBehaviour
{
   [SerializeField] private Material material;
   private LaserBeam beam;

   private void Update()
   {
      Destroy(beam?.LaserObj);
      beam = new LaserBeam(transform.position, transform.up, material);
   }
}
