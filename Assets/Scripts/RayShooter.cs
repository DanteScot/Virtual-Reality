using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;

    public GameObject sniperScope;
    public bool sniperMode = false;

    [SerializeField] private int cursorSize;
    [SerializeField] private Texture cursorTexture;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        sniperScope.GetComponent<Renderer>().enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnGUI()
    {
        if(!sniperMode)
        {
            int x = Screen.width / 2 - cursorSize / 2;
            int y = Screen.height / 2 - cursorSize / 2;
            GUI.DrawTexture(new Rect(x, y, cursorSize, cursorSize), cursorTexture);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // StartCoroutine(SphereIndicator(hit.point));
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && !sniperMode)
        {
            _camera.fieldOfView = 10;
            MouseLook sensVert= GetComponent<MouseLook>();
            sensVert.sensitivityVer = 1f;

            sniperScope.GetComponent<Renderer>().enabled = true;

            PlayerCharacter player = GetComponentInParent<PlayerCharacter>();
            MouseLook sensHor = player.GetComponent<MouseLook>();
            sensHor.sensitivityHor = 1f;
            sniperMode = true;
        }else if(Input.GetMouseButtonDown(1) && sniperMode)
        {
            _camera.fieldOfView = 60;
            MouseLook sensVert= GetComponent<MouseLook>();
            sensVert.sensitivityVer = 9f;

            sniperScope.GetComponent<Renderer>().enabled = false;

            PlayerCharacter player = GetComponentInParent<PlayerCharacter>();
            MouseLook sensHor = player.GetComponent<MouseLook>();
            sensHor.sensitivityHor = 9f;
            sniperMode = false;
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}
