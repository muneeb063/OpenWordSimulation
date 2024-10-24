namespace RackSystem {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RackManager : MonoBehaviour
    {
        [SerializeField] List<RackShape> _totalRackShapesPrefabs = new ();
        [SerializeField] List<Rack> _totalRacksInShop = new ();

       
        private void Start()
        {
            PlaceRackShapesIntoRacks();
        }
        [ContextMenu("Place Rack Shapes")]
        private void PlaceRackShapesIntoRacks()
        {
            if (placingRackShapesRoutine != null) StopCoroutine(placingRackShapesRoutine);
            placingRackShapesRoutine = StartCoroutine(placingRackShapes());
        }
        Coroutine placingRackShapesRoutine = null;
        IEnumerator placingRackShapes()
        {
            for (int i = 0; i < _totalRacksInShop.Count; i++)
            {
                for (int j = 0; j < _totalRacksInShop[i].Capacity; j++) // count will be considered as capacity
                {
                    RackShape addingShape = Instantiate(_totalRackShapesPrefabs[UnityEngine.Random.Range(0, _totalRackShapesPrefabs.Count)]);
                    _totalRacksInShop[i].Add(addingShape);
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
    }


    [Serializable]
    public class RackPlacement {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
    }
    public enum ShapeType
    {
        Shape_1x1,
        Shape_2x2,
        Shape_4x4,
        Shape_6x6,
        Shape_9x9,
        Shape_25x25,
    }
   
}