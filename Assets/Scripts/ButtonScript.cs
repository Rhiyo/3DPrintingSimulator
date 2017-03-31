using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public string label = "Button";
    public Renderer pushable;
    public TextMesh textMesh;
    public Color buttonColour = Color.red;
    public ButtonScript[] offButtons;
    public bool startOn;

	// Use this for initialization
	void Start () {
        if (textMesh != null && label != "")
            textMesh.text = label;
        if(pushable != null)
            pushable.material.color = buttonColour;
        if (startOn)
            Pressed();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Pressed()
    {
        Animator animator = gameObject.GetComponent<Animator>();

        if(offButtons.Length > 0) {
            if (animator != null)
            {
                animator.SetBool("On", true);
            }
            pushable.material.color = Color.green;
            foreach(ButtonScript button in offButtons){
                button.Off();
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetTrigger("Toggle");
            }
        }
    }

    public void Off()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("On", false);
        }
        pushable.material.color = buttonColour;
    }
}
