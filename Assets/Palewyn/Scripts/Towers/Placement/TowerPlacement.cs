using System.Collections;
using System.Collections.Generic;
using TowerDefense.Towers.Placement;
using UnityEngine;
using Core.Utilities;

/// <summary>
/// Uses ray to detect the tile placed on
/// </summary>



public class TowerPlacement : MonoBehaviour {
    public LayerMask hitLayers;
    //public Tower currentSelectedTower

    private void OnDrawGizmos()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(camRay.origin, camRay.origin + camRay.direction * 1000f);
    }

    private void FixedUpdate()
    {
        //Perform raycast
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(camRay, out hit, 1000f, hitLayers))
        {
            //check if the grid is hit
            IPlacementArea placement = hit.collider.GetComponent<IPlacementArea>();
            //get grid point
            if (placement != null)
            {
                //snap position of tower to the grid element
                transform.position = placement.Snap(hit.point, new IntVector2(1, 1));
            }
        }
    }
}
