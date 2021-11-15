using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingPlane : MonoBehaviour
{
    /// <summary>
    /// Class that is used as a means to communicate between the cutting algorithms and the scene
    /// It fetches the values we need from objects in Range and parses them into A Cutter object, so that it calculates the cut
    /// </summary>

    //First list holds all objects within range, the second one holds the indices of all objects that have left the range
    List<GameObject> collidingSplittables = new List<GameObject>();
    List<int> destroyedSplittablesIndeces = new List<int>();

    //Reference to the script calculating full engulfment
    CheckCompleteEngulfment engulfmentChecker;

    [SerializeField] [Tooltip("Parent object of this GameObject. Is used as visual Feedback for the player")]
    Renderer splittingPlaneVisualFeedbackRenderer;

    [SerializeField] [Tooltip("Sets the number of Raycasts for engulfment checking. The more rays the heavier on perfomrance, but the smaller objects can be noticed within range")]
    int numberOfRaycasts = 50;

    //Set to the four corners of our visual Feedback plane
    Vector3[] raycastPoints;

    //flag that gets set to true once the destroyedSplittablesIndeces is populted, so collidingSplittables can be cleared in the next Update Step
    bool clearList;

    // Start is called before the first frame update
    void Start()
    {
        engulfmentChecker = GetComponent<CheckCompleteEngulfment>();

        //translation: {bottom left, top right, bottom right, top left} of our visible splitting plane
        raycastPoints = new Vector3[4] { new Vector3(-splittingPlaneVisualFeedbackRenderer.transform.localScale.x/2, 0, -splittingPlaneVisualFeedbackRenderer.transform.localScale.z/2),
            new Vector3(splittingPlaneVisualFeedbackRenderer.transform.localScale.x/2, 0, splittingPlaneVisualFeedbackRenderer.transform.localScale.z/2),
            new Vector3(splittingPlaneVisualFeedbackRenderer.transform.localScale.x/2, 0, -splittingPlaneVisualFeedbackRenderer.transform.localScale.z/2),
            new Vector3(-splittingPlaneVisualFeedbackRenderer.transform.localScale.x/2, 0, splittingPlaneVisualFeedbackRenderer.transform.localScale.x/2)};
    }

    // Update is called once per frame
    void Update()
    {
        if (clearList) {
            for (int i = destroyedSplittablesIndeces.Count-1; i > -1; i--)
                collidingSplittables.RemoveAt(destroyedSplittablesIndeces[i]);

            clearList = false;
            destroyedSplittablesIndeces = new List<int>();
        }
    }

    public void cut() {
        for (int i = 0; i < collidingSplittables.Count; i++) {
            //All lost and old references to Objects get cued up to be deleted out of our collidingObjects List in the next frame
            if (collidingSplittables[i] == null)
            {
                clearList = true;
                destroyedSplittablesIndeces.Add(i);
            }
            //Initializes a new Cutter Object and calculates the now cut Objects with it
            else
            {
                Cutter cutter = new Cutter();
                //CutterOG cutter = new CutterOG();
                if(collidingSplittables[i].GetComponent<SplittableInfo>())  //if Object includes SplittableInfo script then use its information to initialize the cut
                    cutter.cut(collidingSplittables[i], transform.position - collidingSplittables[i].transform.position, transform.up
                        , collidingSplittables[i].GetComponent<SplittableInfo>().getCutMaterial(), collidingSplittables[i].GetComponent<SplittableInfo>().getCap()
                        , collidingSplittables[i].GetComponent<SplittableInfo>().getConcave(), collidingSplittables[i].GetComponent<SplittableInfo>().getMeshSeperation());
                else  //if not then use default values
                    cutter.cut(collidingSplittables[i], transform.position - collidingSplittables[i].transform.position, transform.up);

                clearList = true;
                destroyedSplittablesIndeces.Add(i);
            }
        }
    }

    //Method used in the Mechanic test scene. Called once the Meshes have been swapped
    public void clearCollidingSplittables() {
        collidingSplittables.Clear();
    }


    private void OnTriggerStay(Collider other)
    {
        //if object isn't yet considered to be colliding
        if (!collidingSplittables.Contains(other.gameObject) && other.gameObject.tag == Tags.splittalbe)
        {
            //to evade reference type errors
            Vector3[] copiedRaycastPoints = new Vector3[4];
            raycastPoints.CopyTo(copiedRaycastPoints, 0);

            //if the Object is enngulfed by our visual feedback plane then add it to our collidingSplittables List and give the player visual confirmation
            if (engulfmentChecker.isEngulfedInCollider(other.GetComponent<MeshCollider>(), copiedRaycastPoints, numberOfRaycasts)) {
                collidingSplittables.Add(other.gameObject);

                splittingPlaneVisualFeedbackRenderer.material.color = Color.red;
            }
        }
        //if object is considered to be colliding
        else if (collidingSplittables.Contains(other.gameObject) && other.gameObject.tag == Tags.splittalbe)
        {
            Vector3[] copiedRaycastPoints = new Vector3[4];
            raycastPoints.CopyTo(copiedRaycastPoints, 0);

            //if the Object isn't enngulfed anymore by our visual feedback plane then remove it to our collidingSplittables List and give the player visual confirmation if there's no more colliding objects
            if (!engulfmentChecker.isEngulfedInCollider(other.GetComponent<MeshCollider>(), copiedRaycastPoints, numberOfRaycasts))
            {
                collidingSplittables.Remove(other.gameObject);

                if(collidingSplittables.Count == 0)
                    splittingPlaneVisualFeedbackRenderer.material.color = new Color(1, 1, 1, 20/255);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //safety function so that object are really removed once they Exit the collider, in case OnTriggerStay didn't catch them leave it
        if (collidingSplittables.Contains(other.gameObject) && other.gameObject.tag == Tags.splittalbe)
        {
            collidingSplittables.Remove(other.gameObject);

            if (collidingSplittables.Count == 0)
                splittingPlaneVisualFeedbackRenderer.material.color = new Color(1, 1, 1, 20 / 255);
        }
    }

    private void OnEnable()
    {
        splittingPlaneVisualFeedbackRenderer.material.color = new Color(1, 1, 1, 20 / 255);
    }

    ///alternative to always become cuttable after collision.
    ///Exchange this with the OnTriggerStayMethod
    ///Doesn't check for complete engulfment
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (!collidingSplittables.Contains(other.gameObject) && other.gameObject.tag == Tags.splittalbe){
            collidingSplittables.Add(other.gameObject);
        
            splittingPlaneVisualFeedbackRenderer.material.color = Color.red;    
        }
    }
    */
}
