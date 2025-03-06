using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalableGrid : MonoBehaviour
{
    private GridLayoutGroup gridLayoutGroup;

    public int defWidth = 1440;
    public int defHeight = 2960;
    public Vector2 size;
    public Vector2 spacing;
    public RectOffset padding;

    public bool useUpdate;

    private float defRatio;

    public float divider = 0.5f;

    private float curWidth;
    private float curHeight;
    private int count;
    private float widthSize;

    private void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        defRatio = (float)defWidth / defHeight;
        StartCoroutine(CheckScale());
    }

    private void Update()
    {
        if (useUpdate)
        {
            StartCoroutine(CheckScale());
        }
    }

    private IEnumerator CheckScale()
    {
        yield return new WaitForEndOfFrame();
        curWidth = Screen.width;
        curHeight = Screen.height;

        float curRatio = (float)curWidth / curHeight;
        Debug.Log("curRatio : " + curRatio + " " + defRatio);

        count = Mathf.RoundToInt(curRatio / divider) + 1;
        widthSize =  (((transform.root.GetComponentInChildren<RectTransform>()).sizeDelta.x - (padding.left + padding.right)) - (spacing.x * (count - 1))) / (float)count;
        float multiplier = widthSize / size.x;

        gridLayoutGroup.cellSize = new Vector2(widthSize, size.y * multiplier);
    }
}
