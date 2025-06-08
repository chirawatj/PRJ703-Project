using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null) return;

        // Restore position after combat
        if (BattleMemory.playerReturnPosition != Vector3.zero)
        {
            player.transform.position = BattleMemory.playerReturnPosition;
            BattleMemory.playerReturnPosition = Vector3.zero; 
            return;
        }

        // Normal scene entry
        string spawnName = SceneTransition.lastExitPoint;
        GameObject spawnPoint = GameObject.Find(spawnName);

        if (spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
        }
    }
}
