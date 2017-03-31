using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

    public UnityEvent onDown;
    public UnityEvent onHold;
    public UnityEvent onUp;

    public string message = "Press 'E' to interact.";
    public float interactionDistance = 2;

    bool isSelected;

    //For object glow
    private Color GlowColor = Color.cyan;
    public float LerpFactor = 10;

    public Renderer[] Renderers
    {
        get;
        private set;
    }

    public Color CurrentColor
    {
        get { return _currentColor; }
    }

    private List<Material> _materials = new List<Material>();
    private Color _currentColor;
    private Color _targetColor;
    private bool isChanging;

    // Use this for initialization
    void Start () {
        Renderers = GetComponentsInChildren<Renderer>();

        foreach (var renderer in Renderers)
        {
            _materials.AddRange(renderer.materials);
        }
    }
	
	// Update is called once per frame
	void Update () {
        FirstPersonController player = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        Interactable obj = player.GetInteractableObj();
        if (this == obj)
        {
            isSelected = true;
            if(!isChanging)
                isChanging = true;
            _targetColor = GlowColor;
        }
        else
        {
            isSelected = false;
            _targetColor = Color.black;
            if (!isChanging)
                isChanging = true;
        }

        if (isSelected)
        {

            if (Input.GetKeyUp(KeyCode.E))
            {

                onUp.Invoke();

            }
            if (Input.GetKey(KeyCode.E))
            {

                onHold.Invoke();

            }
            if (Input.GetKeyDown(KeyCode.E))
            {

                onDown.Invoke();

            }
        }

        if (isChanging)
        {
            _currentColor = Color.Lerp(_currentColor, _targetColor, Time.deltaTime * LerpFactor);

            for (int i = 0; i < _materials.Count; i++)
            {
                _materials[i].SetColor("_GlowColor", _currentColor);
            }

            if (_currentColor.Equals(_targetColor))
            {
                isChanging = false;
            }
        }
    }
}
