using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class ItemCell
{
    protected int CurIndex;
    protected Transform _trans;
    protected GameObject _go;
    protected RectTransform _rect;

    public ItemCell()
    {

    }

    public virtual void Init(GameObject go)
    {
        this._go = go;
        this._trans = go.transform;
        this._rect = go.GetComponent<RectTransform>();
    }
    public virtual void updateItem(int index)
    {
        this.CurIndex = index;
    }

    public Transform transform
    {
        get
        {
            return this._trans;
        }
    }
    public GameObject gameObject
    {
        get
        {
            return this._go;
        }
    }
    public RectTransform rectTransform
    {
        get
        {
            return this._rect;
        }
    }


}


public class VerticalScrollMove : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private class LoopList
    {
        private int curIndex = 0;
        private int maxCount = 0;
        private List<List<ItemCell>> allItemList = new List<List<ItemCell>>();
        public LoopList(List<List<ItemCell>> allItemList)
        {
            this.allItemList = allItemList;
            this.maxCount = this.allItemList.Count;
        }
        public List<ItemCell> getFirst()
        {
            return this.allItemList[curIndex];
        }
        public List<ItemCell> getLast()
        {
            int n = (curIndex + maxCount - 1) % maxCount;
            return this.allItemList[n];
        }
        public void UpMove()
        {
            int newIndex = this.curIndex + 1;
            if (newIndex == this.maxCount)
            {
                this.curIndex = 0;
            }
            else
            {
                this.curIndex = newIndex;
            }
        }
        public void DownMove()
        {
            int newIndex = this.curIndex - 1;
            if (newIndex == -1)
            {
                this.curIndex = maxCount - 1;
            }
            else
            {
                this.curIndex = newIndex;
            }
        }
        public void Refresh(int startYindex, int colCount)
        {
            for (int i = 0; i < allItemList.Count; i++)
            {
                List<ItemCell> list = allItemList[(i + curIndex) % maxCount];
                for (int j = 0; j < list.Count; j++)
                {
                    list[j].updateItem((startYindex + i) * colCount + j);
                }
            }
        }
    }

    public ScrollRect scrollRect;
    public RectTransform gridRect;
    public RectTransform willContextView;

    public RectTransform prefab;
    public Vector2 spacing;


    [Header("固定列数，默认为1")]
    public int colCount = 1;
    public int itemYCount = 10;

    private float itemHeight;
    private float itemWidth;

    private int upIndex = 0;
    private int botomIndex = 0;
    private int oldMoveIndex = 0;
    private float postionY = 0;
    private float scrollRectHeight;

    private LoopList loopPrefabList;
    private bool isEditor = true;
    private bool isDraging = false;

    /// <summary>
    /// 请在Start执行之前调用--
    /// </summary>
    /// <param name="listData">List data.</param>
    public void InitListData<T>(int num) where T : ItemCell, new()
    {
        this.isEditor = false;

        itemYCount = num / this.colCount;
        scrollRectHeight = scrollRect.GetComponent<RectTransform>().rect.height;
        scrollRect.onValueChanged = new ScrollRect.ScrollRectEvent();
        scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        initScrollview<T>();
    }

    public void Refresh()
    {
        this.loopPrefabList.Refresh(this.upIndex, this.colCount);
    }
    TweenerCore<Vector2, Vector2, VectorOptions> tweenPos;
    /**
    移动列表到指定位置，
     */
    public void setPosition(int distance,bool isAnimation= false)
    {
        // 有时间就是拖过去的，省了设置位置的操作，因为自己走监听OnScrollValueChanged了
        var time = isAnimation ? 0.3f: 0.01f;
        scrollRect.vertical = false;

        tweenPos =  DOTween.To(() => gridRect.anchoredPosition, y => gridRect.anchoredPosition = y, new Vector2(gridRect.anchoredPosition.x, itemHeight * distance), time).OnComplete(() =>
        {
            scrollRect.vertical = true;
        });

    }

    // Use this for initialization
    void Start()
    {
        if (this.isEditor)
        {
            initScrollview<ItemCell>();
            scrollRectHeight = scrollRect.GetComponent<RectTransform>().rect.height;
            scrollRect.onValueChanged = new ScrollRect.ScrollRectEvent();
            scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        }
    }

    void initScrollview<T>() where T : ItemCell, new()
    {
        itemHeight = prefab.sizeDelta.y + spacing.y;
        itemWidth = prefab.sizeDelta.x + spacing.x;
        gridRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, itemYCount * itemHeight);

        //垂直方向需要多少个
        int needYCount = Mathf.CeilToInt(scrollRectHeight / itemHeight) + 1;//获取需要实例化的对象个数
        InitPrefabList<T>(needYCount);
        postionY = gridRect.anchoredPosition.y;
        oldMoveIndex = Mathf.FloorToInt(Mathf.Abs((gridRect.anchoredPosition.y - itemHeight / 2) / itemHeight));//当前移动到了第几格;
    }

    void InitPrefabList<T>(int needYCount) where T : ItemCell, new()
    {
        List<List<ItemCell>> prefabsList = new List<List<ItemCell>>();
        //实例化循环的item
        for (int i = 0; i < needYCount; i++)
        {
            List<ItemCell> list = new List<ItemCell>();
            if (colCount > 1)
            {
                for (int j = 0; j < colCount; j++)
                {
                    GameObject gob = Instantiate(prefab.gameObject, gridRect.transform) as GameObject;
                    T t = new T();
                    t.Init(gob);
                    t.rectTransform.anchoredPosition = new Vector2((j - 1) * itemWidth, -itemHeight / 2 - i * itemHeight);
                    t.updateItem(i * colCount + j);
                    list.Add(t as ItemCell);
                }
            }
            else
            {
                for (int j = 0; j < colCount; j++)
                {
                    GameObject gob = Instantiate(prefab.gameObject, gridRect.transform) as GameObject;
                    T t = new T();
                    t.Init(gob);
                    t.rectTransform.anchoredPosition = new Vector2(0, -itemHeight / 2 - i * itemHeight);
                    t.updateItem(i * colCount + j);
                    list.Add(t as ItemCell);
                }
            }
            prefabsList.Add(list);//折扣
        }
        botomIndex = needYCount - 1;
        this.loopPrefabList = new LoopList(prefabsList);
        this.prefab.gameObject.SetActive(false);
    }

    void OnScrollValueChanged(Vector2 vec)
    {
        float curPosY = gridRect.anchoredPosition.y;//获取grid	对象y方向的位置--
        int curMoveIndex = Mathf.FloorToInt(Mathf.Abs(gridRect.anchoredPosition.y / itemHeight));
        int offsetCount = Mathf.Abs(curMoveIndex - oldMoveIndex);
        for (int i = 0; i < offsetCount; i++)
        {
            //向上移动，判断控制顶部刷新位置，判断移动方向，判断控制底部刷新位置 只有当上面一个对象完全看不到的时候才开始刷新--
            if (botomIndex < itemYCount && curPosY > itemHeight && curPosY > postionY && botomIndex < itemYCount - 1)
            {
                upIndex++;
                botomIndex++;
                updateListviewPos(true);//更新item位置
            }
            //向下移动 只有当下面一个对象完全看不到的时候才开始刷新
            else if (upIndex > 0 && curPosY > 0 && curPosY < postionY && (curPosY + scrollRectHeight) < (itemYCount - 1) * itemHeight)
            {
                upIndex--;
                botomIndex--;
                updateListviewPos(false);
            }
        }
        oldMoveIndex = curMoveIndex;
        postionY = curPosY;
       // Debugger.Log("upIndex={0},botomIndex={1},scrollRect.vertical={2}", upIndex, botomIndex, scrollRect.vertical);
    }

    void updateListviewPos(bool isMoveUp)
    {
        if (isMoveUp)
        {
            //向上移动，把集合第一个对象移动的最后位置，调整该对象在父对象中的相对位置
            List<ItemCell> list = this.loopPrefabList.getFirst();
            this.loopPrefabList.UpMove();
            if (list.Count > 1)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].rectTransform.anchoredPosition = new Vector2((i-1) * itemWidth, -itemHeight / 2 - botomIndex * itemHeight);
                    list[i].updateItem(botomIndex * colCount + i);
                }
            }
            else {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].rectTransform.anchoredPosition = new Vector2(0, -itemHeight / 2 - botomIndex * itemHeight);
                    list[i].updateItem(botomIndex * colCount + i);
                }
            }
        }
        else
        {
            //向下移动，把集合最后一个对象移动的第一个位置，调整该对象在父对象中的相对位置
            List<ItemCell> list = this.loopPrefabList.getLast();
            this.loopPrefabList.DownMove();
            if (list.Count > 1)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].rectTransform.anchoredPosition = new Vector2((i - 1) * itemWidth, -itemHeight / 2 - upIndex * itemHeight);
                    list[i].updateItem(upIndex * colCount + i);
                }
            }
            else {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].rectTransform.anchoredPosition = new Vector2(0, -itemHeight / 2 - upIndex * itemHeight);
                    list[i].updateItem(upIndex * colCount + i);
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;
    }

    public bool IsDraging
    {
        get
        {
            return isDraging;
        }
    }
}