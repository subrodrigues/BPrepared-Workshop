using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns {

    public class Tree : MonoBehaviour {
        private List<Vector3> _leavesPositions;
        private string _color;
        private float _reflectivity;

        public void Initialize(List<Vector3> positionsLeaves, string color, float reflectivity) {
            this._leavesPositions = positionsLeaves;
            this._reflectivity = reflectivity;
            this._color = color;
        }
    }
}
