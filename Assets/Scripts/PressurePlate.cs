using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PressurePlate2D : MonoBehaviour
{
    [Header("What can press this plate")]
    [Tooltip("List of allowed tags that can press the plate.")]
    public List<string> allowedTags = new List<string> { "Player", "Pushable" };

    [Header("Optional feedback")]
    public SpriteRenderer indicator;     // change color when pressed (optional)
    public Color idleColor = Color.gray;
    public Color pressedColor = Color.green;

    // Public read-only state
    public bool IsPressed { get; private set; }

    // Fires whenever IsPressed changes (true/false)
    public event Action<bool> OnPressChanged;

    // Track current occupants that count
    private readonly HashSet<Collider2D> _pressing = new HashSet<Collider2D>();
    private Collider2D _coll;

    private void Reset()
    {
        // Make sure the collider is a trigger
        var c = GetComponent<Collider2D>();
        c.isTrigger = true;
    }

    private void Awake()
    {
        _coll = GetComponent<Collider2D>();
        if (_coll != null) _coll.isTrigger = true;
        ApplyIndicator();
    }

    private void OnEnable()
    {
        // Clean slate
        _pressing.Clear();
        SetPressed(false);
    }

    private void OnDisable()
    {
        _pressing.Clear();
        SetPressed(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Counts(other)) return;
        if (_pressing.Add(other))
        {
            if (!IsPressed) SetPressed(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!Counts(other)) return;
        if (_pressing.Remove(other))
        {
            if (_pressing.Count == 0 && IsPressed) SetPressed(false);
        }
    }

    private bool Counts(Collider2D col)
    {
        // Tag filter
        if (allowedTags != null && allowedTags.Count > 0)
        {
            foreach (var t in allowedTags)
                if (col.CompareTag(t)) return true;
            return false;
        }
        return true;
    }

    private void SetPressed(bool value)
    {
        if (IsPressed == value) return;
        IsPressed = value;
        ApplyIndicator();
        OnPressChanged?.Invoke(IsPressed);
    }

    private void ApplyIndicator()
    {
        if (indicator != null)
            indicator.color = IsPressed ? pressedColor : idleColor;
    }
}
