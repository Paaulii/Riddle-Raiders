using UnityEngine;

public class LaserInitiator : MonoBehaviour
{
   [SerializeField] private Material material;
   [SerializeField] private Color laserColor;
   [SerializeField] private float raycastDistance;
   [SerializeField] private float laserWidth;
   private LaserBeam beam;

   private void Update()
   {
      Destroy(beam?.LaserObj);
      beam = new LaserBeam(transform.position, transform.up, material, laserColor, laserWidth, raycastDistance);
   }
}
