using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody myBody;
    public float speed = 400f;

    private Camera mainCam;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfFallDown();
    }
    void FixedUpdate()
    {
        Movement();
        MobileMovement();
    }

    void Movement()
    {
        float hoz_Axis = Input.GetAxis("Horizontal") * (Time.deltaTime * 2f);
        float vert_Axis = Input.GetAxis("Vertical") * (Time.deltaTime * 2f);

        Vector3 vert_Cam = mainCam.transform.forward;
        Vector3 hoz_Cam = mainCam.transform.right;

        vert_Cam.y = 0;
        hoz_Cam.y = 0;

        vert_Cam.Normalize();
        hoz_Cam.Normalize();

        Vector3 playerMove = (vert_Cam * vert_Axis + hoz_Cam * hoz_Axis) * speed;
        myBody.AddForce(playerMove);

    }//Movement

    void MobileMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray currenyRay = mainCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(currenyRay, out hit))
            {
                Vector3 newPos = hit.point;
                Vector3 currentPos = transform.position;
                Vector3 direction = newPos - currentPos;
                direction.Normalize();
                direction.y = 0f;

                myBody.AddForce(direction * speed * (Time.deltaTime * 2f));
            }
        }
    }//Mobile Movement

    void CheckIfFallDown()
    {
        if (transform.position.y < -2f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Finish")
        {
            Invoke("GameWin", 2f);
        }
    }

    void GameWin()
    {
        SceneManager.LoadScene("GameWin");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
