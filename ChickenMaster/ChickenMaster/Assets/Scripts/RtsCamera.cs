using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RtsCamera : MonoBehaviour
{
    //TODO: mozna by jeszcze dodac obszary gdzie szybkosc przewijania jest mniejsza troche blizej srodka np.

    public Transform LeftMapEdge;
    public Transform RightMapEdge;
    public Transform TopMapEdge;
    public Transform BottomtMapEdge;


    public Transform CenterScreen;


    public Transform LeftScrollArea;
    public Transform RightScrollArea;
    public Transform TopScrollArea;
    public Transform BottomScrollArea;

    float correction;

    [SerializeField]
    float scrollSpeed = 70;


    // Start is called before the first frame update
    void Start()
    {
        //var distance = Vector3.Distance(RightBoarder.position, LeftBoarder.position);
        //correction = distance != 0 ? distance / 2 : 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        //transform.position = new Vector3(Mathf.Clamp(FollowTarget.position.x, LeftMapEdge.position.x + correction, RightMapEdge.position.x - correction), transform.position.y, transform.position.z);

        var mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mPosX = mPos.x; //Input.mousePosition.x;
        var mPosY = mPos.y;//Input.mousePosition.y;


        var topScrollArea = TopScrollArea.position; //TopScrollArea.TransformPoint(Vector3.zero);
        var leftScrollArea = LeftScrollArea.position;//LeftScrollArea.TransformPoint(Vector3.zero);
        var rightScrollArea = RightScrollArea.position;//RightScrollArea.TransformPoint(Vector3.zero);
        var bottomScrollArea = BottomScrollArea.position;//BottomScrollArea.TransformPoint(Vector3.zero);

        //Debug.Log($"Mxy {mPosX}; {mPosY}  Area ltrb:{leftScrollArea.x}, {topScrollArea.y}, {rightScrollArea.x}, {bottomScrollArea.y}" +
        //    $" Edges ltrb { LeftMapEdge.position.x},{TopMapEdge.position.y},{RightMapEdge.position.x}, {BottomtMapEdge.position.y}");

        // Do camera movement by mouse position
        if (mPosX > rightScrollArea.x && mPosX < RightMapEdge.position.x) { transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime); }
        if (mPosX < leftScrollArea.x && mPosX >= LeftMapEdge.position.x) { transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime); }
        if (mPosY > topScrollArea.y && mPosY < TopMapEdge.position.y) { transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime); }
        if (mPosY < bottomScrollArea.y && mPosY >= BottomtMapEdge.position.y) { transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime); }

        Vector3 cameraPos = transform.position;
        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        float halfScreenWidth = width / 2;
        float halfScreenHeight = height / 2;
        //Debug.Log($"Cam XY {cameraPos.x} ; {cameraPos.y} , WH {width}x{height}");

        Debug.Log($"camPosX {cameraPos.x} camPosY {cameraPos.y} LEX {LeftMapEdge.position.x} Cond C < E {cameraPos.x < LeftMapEdge.position.x} TEY {TopMapEdge.position.y} REX {RightMapEdge.position.x} BEY {BottomtMapEdge.position.y}");
        if (cameraPos.x < LeftMapEdge.position.x - halfScreenWidth)
        {
            transform.position = new Vector3(LeftMapEdge.position.x + halfScreenWidth, transform.position.y, transform.position.z);
            Debug.Log($"Left Correction X {transform.position.x} Y {transform.position.y}");
        }
        if (cameraPos.y > TopMapEdge.position.y - halfScreenHeight)
            transform.position = new Vector3(transform.position.x, TopMapEdge.position.y - halfScreenHeight, transform.position.z);
        if (cameraPos.x > RightMapEdge.position.x - halfScreenWidth)
            transform.position = new Vector3(RightMapEdge.position.x - halfScreenWidth, transform.position.y, transform.position.z);
        if (cameraPos.y < BottomtMapEdge.position.y + halfScreenHeight)
            transform.position = new Vector3(transform.position.x, BottomtMapEdge.position.y + halfScreenHeight, transform.position.z);


        //// Do camera movement by keyboard
        //myTransform.Translate(Vector3(Input.GetAxis("EditorHorizontal")  scrollSpeed  Time.deltaTime,
        //                              Input.GetAxis("EditorVertical")  scrollSpeed  Time.deltaTime, 0));


        //// Do camera movement by holding down option                 or middle mouse button and then moving mouse
        //if ((Input.GetKey("left alt") || Input.GetKey("right alt")) || Input.GetMouseButton(2))
        //{
        //    myTransform.Translate(-Vector3(Input.GetAxis("Mouse X") * dragSpeed, Input.GetAxis("Mouse Y") * dragSpeed, 0));
        //}
    }
}
