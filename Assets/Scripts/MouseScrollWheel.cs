using System.Collections;
using UnityEngine;

public class MouseScrollWheel : MonoBehaviour
{
    private bool isScrolling = false;
    private float scrollSpeed = 1.0f;
    private float returnSpeed = 0.4f;
    private float returnTimer = 1.0f;
    private float returnDelay = 1.0f;
    private float startPositionY;
    private IEnumerator returnRoutine;


    // Start is called before the first frame update
    void Start()
    {
        startPositionY = transform.position.y; //The initial position, may need to update this as the player extends roots/screen. 
    }

    // Update is called once per frame
    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel != 0) { //if the scrollwheel is active
            isScrolling = true;
            transform.position += new Vector3(0, scrollWheel * 0.5f * scrollSpeed, 0);
            returnTimer = 0.0f;
        }

        else if (Input.GetKey(KeyCode.UpArrow)) { //if the up arrow is active
            isScrolling = true;
            transform.position += new Vector3(0, scrollSpeed * 0.005f, 0);
            returnTimer = 0.0f;
        }

        else if (isScrolling == false && transform.position.y != startPositionY) { //if camera return conditions are met
            if (returnRoutine == null) {
                returnRoutine = ReturnToStartPosition();
                StartCoroutine(returnRoutine);
            }
        }


        else if (isScrolling) { //Ensures that the user has finished pressing the up or scroll input.
            returnTimer += Time.deltaTime; // update timer
            if (returnTimer >= returnDelay) {
                isScrolling = false; // stop scrolling
            }
        }

        IEnumerator ReturnToStartPosition() {
            yield return new WaitForSeconds(returnDelay);

            while (Mathf.Round(transform.position.y) != Mathf.Round(startPositionY)) {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, startPositionY, transform.position.z), Time.deltaTime * returnSpeed);
                yield return null;
            }

            returnRoutine = null;
        }
    }
}





