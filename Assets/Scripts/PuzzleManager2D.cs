using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager2D : MonoBehaviour
{
    public static PuzzleManager2D instance;

    [Header("Door")]
    public GameObject door; // Reference to the door GameObject (can disable to "open")

    [Header("Order Settings")]
    public List<int> correctOrder = new List<int> { 0, 2, 1 }; // The correct sequence
    private List<int> currentOrder = new List<int>(); // Player's current input sequence

    [Header("Plates")]
    public PressurePlateLevel4[] plates; // All pressure plates in the scene

    private bool puzzleSolved = false;
    
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        instance = this;

        // Optional: Automatically find all plates in the scene (you can also assign manually)
        if (plates == null || plates.Length == 0)
        {
            plates = FindObjectsOfType<PressurePlateLevel4>();
        }
    }

    public void PlatePressed(int id)
    {
        if (puzzleSolved)
            return; // Ignore input if puzzle is already solved

        currentOrder.Add(id);

        // If player pressed more plates than needed → wrong, reset
        if (currentOrder.Count > correctOrder.Count)
        {
            ResetPuzzle();
            return;
        }

        // Check current order against the correct sequence so far
        for (int i = 0; i < currentOrder.Count; i++)
        {
            if (currentOrder[i] != correctOrder[i])
            {
                ResetPuzzle();
                return;
            }
        }

        // If all plates are pressed in correct order → puzzle solved
        if (currentOrder.Count == correctOrder.Count)
        {
            PuzzleSuccess();
        }
    }

    private void PuzzleSuccess()
    {
        Debug.Log("✅ Puzzle Solved!");
        puzzleSolved = true;

        audioSource.Play();
        
        // Open the door (disable or trigger animation)
        if (door != null)
            door.SetActive(false);

        // Lock all plates in their pressed color
        foreach (PressurePlateLevel4 plate in plates)
        {
            if (plate == null) continue;

            if (correctOrder.Contains(plate.plateID))
                plate.LockPressedColor();
        }
    }

    private void ResetPuzzle()
    {
        Debug.Log("❌ Wrong order! Resetting puzzle...");
        currentOrder.Clear();

        // Reset all plates to idle color
        foreach (PressurePlateLevel4 plate in plates)
        {
            if (plate == null) continue;
            plate.UnlockAndReset();
        }
    }
}
