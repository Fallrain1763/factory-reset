using UnityEngine;

public class HackManager : MonoBehaviour
{
    public static HackManager Instance { get; private set; }
    public static bool IsHacking => Instance && Instance._currentHacked != null;

    // NEW: short UI suppress after ending hack
    private float _inputSuppressTimer = 0f;
    public static bool SuppressUI => Instance && Instance._inputSuppressTimer > 0f;

    [SerializeField] private GridMovement _player;
    private GridMovement _currentHacked;

    void Awake()
    {
        if (Instance && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Update()
    {
        if (_inputSuppressTimer > 0f) _inputSuppressTimer -= Time.deltaTime;

        if (IsHacking && Input.GetKeyDown(KeyCode.Space))
            EndHack();
    }

    public void BeginHack(GridMovement npc)
    {
        if (!_player || !npc) return;

        _player.HasControl = false;
        npc.HasControl = true;
        _currentHacked = npc;
        // optional: close any open panel here if you hold a ref to it
    }

    public void EndHack()
    {
        if (!_player) return;

        _player.HasControl = true;
        if (_currentHacked) _currentHacked.HasControl = false;
        _currentHacked = null;

        _inputSuppressTimer = 0.15f; // NEW: debounce Space for one tick
    }

    public void NotifyActorKilled(GameObject actorGO)
    {
        if (_currentHacked && actorGO == _currentHacked.gameObject)
            EndHack();
    }
}
