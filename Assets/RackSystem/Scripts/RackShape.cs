namespace RackSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RackShape : MonoBehaviour
    {
        public ShapeType ShapeType;
        public List<RackPlacement> totalPlacementsInShape = new();
        [SerializeField] private List<Transform> ItemPosition = new List<Transform>();

        [ContextMenu("Fetch Placements")] 
        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                RackPlacement rp = new RackPlacement();
                rp.Position = transform.GetChild(i).localPosition;
                rp.Rotation = transform.GetChild(i).localRotation;
                rp.Scale = transform.GetChild(i).localScale;
                totalPlacementsInShape.Add(rp);
            }
        }
        public void Init(ShapeType _shapeType)
        {
            if (_shapeType == ShapeType.Shape_1x1)
            {
                RackPlacement rp = new RackPlacement();
                rp.Position = Vector3.zero;
                rp.Rotation = Quaternion.identity;
                rp.Scale = Vector3.one;
                totalPlacementsInShape.Add(rp);
            }
            else if (_shapeType == ShapeType.Shape_2x2)
            {
                for (int i = 0; i < 2; i++)
                {

                }
            }
        }
    }

}