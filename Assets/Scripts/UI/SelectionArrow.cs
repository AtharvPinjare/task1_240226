using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;//For storing all the options
    private RectTransform rect;
    private int currentpositon;//To indicate which options is current.
    [SerializeField]private AudioClip ChangeSound;//When to move arrow up and down
    [SerializeField]private AudioClip InteratSuund;//When to select an option
    

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) { ChangePosition(1); }

        //Interact with current option
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
            Interact();
    }
    private void ChangePosition(int _change) 
    {
        currentpositon += _change;

        if (_change != 0 && SoundManager.instance != null)
        {
            SoundManager.instance.PlaySound(ChangeSound);
        }

        if (currentpositon < 0) 
        {
            //When goes above the first option, this directs to the last options
            currentpositon = options.Length - 1;
        }
        else if(currentpositon > (options.Length - 1)) 
        {
            currentpositon = 0;
        }

        //Moves the arrow to the current option.
        rect.position = new Vector3(rect.position.x, options[currentpositon].position.y ,0);
        //AssignPosition();
    }
    /*
    private void AssignPosition()
    {
        //Assign the Y position of the current option to the arrow (basically moving it up and down)
        arrow.position = new Vector3(arrow.position.x, options[currentpositon].position.y);
    }
    */
    private void Interact()
    {
        SoundManager.instance.PlaySound(InteratSuund);

        //Access the button component on each option and call its function
        options[currentpositon].GetComponent<Button>().onClick.Invoke();
    }

}
