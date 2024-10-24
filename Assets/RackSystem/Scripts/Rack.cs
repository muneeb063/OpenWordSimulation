namespace RackSystem {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Rack : MonoBehaviour
    {
        // rack has info of having rack shapes
        public int Capacity { get=>_rackPlacements.Count;}
        [SerializeField] private List<RackPlacement> _rackPlacements = new ();
        [SerializeField] private Transform _rackShapeContainer;

        [SerializeField] private List<RackShape> _placedRackShapes = new ();
        private void Awake()
        {
           
        }
        [ContextMenu("Spawn")]
        public void Add(RackShape _rackShape)
        {
            _rackShape.transform.SetParent(_rackShapeContainer);
            _placedRackShapes.Add(_rackShape);
            
            _rackShape.transform.localPosition = _rackPlacements[_placedRackShapes.Count - 1].Position;
            _rackShape.transform.localRotation = _rackPlacements[_placedRackShapes.Count - 1].Rotation;
            _rackShape.transform.localScale = _rackPlacements[_placedRackShapes.Count - 1].Scale;
            

        }

    }

}