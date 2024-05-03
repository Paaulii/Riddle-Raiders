using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    public GameObject LaserObj => laserObj;
    private Vector3 pos, dir;
    private GameObject laserObj;
    private LineRenderer laser;
    private List<Vector3> laserPositions = new();

    public LaserBeam(Vector3 pos, Vector3 dir, Material material)
    {
        laser = new LineRenderer();
        laserObj = new GameObject();
        laserObj.name = "Laser Beam";
        this.pos = pos;
        this.dir = dir;
        laser = laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;
        laser.material = material;
        laser.startColor = new Color32(245, 207, 130, 255);
        laser.endColor = new Color32(245, 207, 130, 255);
        laser.sortingLayerName = "Laser line";
        CastRay(pos, dir);
    }
    
    void CastRay(Vector3 pos, Vector3 dir)
    {
        laserPositions.Add(pos);
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30, 1))
        {
            CheckHit(hit, dir);
        }
        else
        {
           laserPositions.Add(ray.GetPoint(30));
           UpdateLaser();
        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction)
    {
        LaserReciever laserReciever = hitInfo.collider.GetComponent<LaserReciever>();

        if (laserReciever) 
        {
            laserReciever.isActive = true;
        }
       
        if (hitInfo.collider.transform.parent.GetComponent<Mirror>())
        {
            Vector3 hitPosition = hitInfo.point;
            Vector3 reflectedDirection = Vector3.Reflect(direction, hitInfo.normal);
            CastRay(hitPosition, reflectedDirection);
        } 
        else
        {
            laserPositions.Add(hitInfo.point);
            UpdateLaser();
        }
    }
    
    void UpdateLaser()
    {
        laser.positionCount = laserPositions.Count;

        for (int i = 0; i < laserPositions.Count; i++)
        {
            laser.SetPosition(i, laserPositions[i]);
        }
    }
}
