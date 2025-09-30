using UnityEngine;

public class HackManager : MonoBehaviour
{
    public static HackManager Instance { get; private set; }

    public static bool IsHacking => Instance && Instance._currentHacked != null;

    [SerializeField] private GridMovement _player;   // assign in Inspector (your main player)
    private GridMovement _currentHacked;             // NPC being controlled

    private void Awake()
    {
        if (Instance && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Update()
    {
        // Space ends hacking (if we are hacking)
        if (IsHacking && Input.GetKeyDown(KeyCode.Space))
        {
            EndHack();
        }
    }

    public void BeginHack(GridMovement npc)
    {
        if (!npc) return;
        if (!_player) { Debug.LogWarning("HackManager: Player reference not set."); return; }

        // Give control to NPC
        _player.HasControl = false;
        npc.HasControl = true;
        _currentHacked = npc;

        // Optional: visually indicate hacking (tint, UI, etc.)
        Debug.Log($"[Hack] Took control of {npc.name}");
    }

    public void EndHack()
    {
        if (!_player) return;

        // Return control to player
        _player.HasControl = true;

        if (_currentHacked)
            _currentHacked.HasControl = false;

        Debug.Log("[Hack] Control returned to Player");
        _currentHacked = null;
    }

    // Call this when something dies/despawns to ensure control returns
    public void NotifyActorKilled(GameObject actorGO)
    {
        if (!actorGO) return;
        if (_currentHacked && actorGO == _currentHacked.gameObject)
        {
            EndHack();
        }
    }
}
