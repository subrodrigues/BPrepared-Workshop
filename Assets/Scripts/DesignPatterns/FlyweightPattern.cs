using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns {

    public class FlyweightPattern : MonoBehaviour {
        public GameObject treePrefab;
        public List<Tree> trees;
        
        void Start() {
            GameObject grpTrees = GameObject.FindWithTag("GrpTrees"); // OPTIONAL
            
            for (int i = 0; i < 100; i++) {
                for (int j = 0; j < 100; j++) {
                    Tree tree = Instantiate(treePrefab,  new Vector3(i, 0.5f, j), Quaternion.identity).GetComponent<Tree>();
                    tree.transform.parent = grpTrees.transform; // OPTIONAL
                    
                    List<Vector3> leaves = GetLeavesPositions();
                    tree.Initialize(leaves, "red", 1f);

                    trees.Add(tree);
                }
            }
        }

        private List<Vector3> GetLeavesPositions() {
            List<Vector3> positions = new List<Vector3>();
            
            for (int i = 0; i < 5000; i++) {
                positions.Add(new Vector3(Random.Range(-0.4f, 0.4f), 1.5f + Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f)));
            }

            return positions;
        }
    }

}
