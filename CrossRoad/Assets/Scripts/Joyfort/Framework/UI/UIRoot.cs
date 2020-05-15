using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Init The UI Root
/// 
/// UIRoot
/// -Canvas
/// --FixedRoot
/// --NormalRoot
/// --PopupRoot
/// -Camera
/// </summary>
public class UIRoot : MonoBehaviour {
	private static UIRoot m_Instance = null;
	public static UIRoot Instance {
		get {
            if (m_Instance == null)
            {
                //var go = GameObject.Instantiate(Resources.Load("UIPrefab/UIRoot")) as GameObject;
                InitRoot();
            }
            return m_Instance;
		}
	}
    private void Awake()
    {
        m_Instance = this;
    }

    public Transform root;
	public Transform fixedRoot;
	public Transform normalRoot;
	public Transform popupRoot;
    public Transform maskRoot;
    public Transform promptRoot;
    public Camera uiCamera;

    public EventSystem m_Sys = null;

	static void InitRoot() {
       
		GameObject go = new GameObject("UIRoot");
		go.layer = LayerMask.NameToLayer("UI");
		m_Instance = go.AddComponent<UIRoot>();
		go.AddComponent<RectTransform>();

		//Canvas can = go.AddComponent<Canvas>();
		//can.renderMode = RenderMode.ScreenSpaceCamera;
		//can.pixelPerfect = false;
  //      can.sortingOrder = 3;

  //      YHGraphicRaycaster raycaster =  go.AddComponent<YHGraphicRaycaster>();

  //      raycaster.blockingObjects = GraphicRaycaster.BlockingObjects.TwoD;


        m_Instance.root = go.transform;

		GameObject camObj = new GameObject("UICamera");

		camObj.layer = LayerMask.NameToLayer("UI");
        
        camObj.transform.parent = go.transform;
		camObj.transform.localPosition = new Vector3(0, 0, -100f);
		Camera cam = camObj.AddComponent<Camera>();
		cam.clearFlags = CameraClearFlags.Depth;
		cam.orthographic = true;
		cam.depth = 5;
		cam.farClipPlane = 200f;
		//can.worldCamera = cam;
		cam.cullingMask = 1 << 5;
		cam.nearClipPlane = -50f;
		cam.farClipPlane = 200f;

		m_Instance.uiCamera = cam;


		//CanvasScaler cs = go.AddComponent<CanvasScaler>();
		//cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		//cs.referenceResolution = new Vector2(720f, 1280f);
		//cs.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;

		GameObject subRoot;

		subRoot = CreateSubCanvasForRoot(go.transform, 0, cam);
		subRoot.name = "NormalRoot";
		m_Instance.normalRoot = subRoot.transform;
		m_Instance.normalRoot.transform.localScale = Vector3.one;

		subRoot = CreateSubCanvasForRoot(go.transform, 10, cam);
		subRoot.name = "FixedRoot";
		m_Instance.fixedRoot = subRoot.transform;
		m_Instance.fixedRoot.transform.localScale = Vector3.one;

		subRoot = CreateSubCanvasForRoot(go.transform, 20, cam);
		subRoot.name = "PopupRoot";
		m_Instance.popupRoot = subRoot.transform;
		m_Instance.popupRoot.transform.localScale = Vector3.one;

        subRoot = CreateSubCanvasForRoot(go.transform, 30, cam);
        subRoot.name = "MaskRoot";
        m_Instance.maskRoot = subRoot.transform;
        m_Instance.maskRoot.transform.localScale = Vector3.one;

        subRoot = CreateSubCanvasForRoot(go.transform, 700, cam);
        subRoot.name = "promptRoot";
        m_Instance.promptRoot = subRoot.transform;
        m_Instance.promptRoot.transform.localScale = Vector3.one;

        GameObject eventObj = new GameObject("EventSystem");
		eventObj.layer = LayerMask.NameToLayer("UI");
		eventObj.transform.SetParent(go.transform);
        eventObj.transform.localScale = Vector3.one;
        eventObj.transform.localPosition = Vector3.zero;


        m_Instance.m_Sys = eventObj.AddComponent<EventSystem>();
		eventObj.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();

		GameObject.DontDestroyOnLoad(go);
	}

	static GameObject CreateSubCanvasForRoot(Transform root, int sort, Camera cam) {
		GameObject go = new GameObject("canvas");
		go.transform.parent = root;
		go.layer = LayerMask.NameToLayer("UI");

		RectTransform rect = go.AddComponent<RectTransform>();
		rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
		rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
		rect.anchorMin = Vector2.zero;
		rect.anchorMax = Vector2.one;

        Canvas can = go.AddComponent<Canvas>();
        can.renderMode = RenderMode.ScreenSpaceCamera;
        can.pixelPerfect = false;
        can.sortingOrder = sort;
        can.worldCamera = cam;

        YHGraphicRaycaster raycaster = go.AddComponent<YHGraphicRaycaster>();

        raycaster.blockingObjects = GraphicRaycaster.BlockingObjects.TwoD;

        CanvasScaler cs = go.AddComponent<CanvasScaler>();
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.referenceResolution = new Vector2(720f, 1280f);
        cs.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;

        return go;
	}

    static GameObject CreateSubCanvasForRoot(Transform root, int sort)
    {
        GameObject go = new GameObject("canvas");
        go.transform.parent = root;
        go.layer = LayerMask.NameToLayer("UI");

        RectTransform rect = go.AddComponent<RectTransform>();
        rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        rect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;

        return go;
    }

    public void EnableEventSystem(bool enable)
    {
        //if (Global.gPlayerData.iGuideId < GuideEnum.Guide03)
        //    return;
        m_Sys.gameObject.SetActive(enable);
        EnableOtherRoot(enable);
    }

    public void EnableUICamera(bool enable)
    {
        //if (Global.gPlayerData.iGuideId < GuideEnum.Guide03)
        //    return;
        if (!enable)
        {
            //解决某些手机关闭2D摄像机时候会导致3D摄像机不渲染
            //CameraMgr.Inst.ClearCameraTexture();
            StopCoroutine(DelayST());
            StartCoroutine(DelayST());
        }
        else
        {
            gameObject.SetActive(enable);
        }
    }
    void OnDestroy() {
		m_Instance = null;
	}

    IEnumerator DelayST()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
    void EnableOtherRoot(bool enable)
    {
        if(normalRoot != null)
        {
            normalRoot.gameObject.SetActive(enable);
        }
        if (popupRoot != null)
        {
            popupRoot.gameObject.SetActive(enable);
        }
        if (maskRoot != null)
        {
            maskRoot.gameObject.SetActive(enable);
        }
    }
}