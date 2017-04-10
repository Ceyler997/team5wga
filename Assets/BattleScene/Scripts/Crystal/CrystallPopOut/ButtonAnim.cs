using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnim : MonoBehaviour {

    public RectTransform Pivot;
    public RectTransform mTransform;
	// Use this for initialization
	void Start () {
        mTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Animation(false));
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(Animation(true));
        }

    }

    public void GUIReact(bool revert)
    {
        if (anim != null)
            StopCoroutine(anim);
        anim = Animation(revert);
        StartCoroutine(anim);
    }

    public BoxCollider mCollider;
    IEnumerator anim;
    IEnumerator Animation(bool reverce)
    {
        Vector2 startBounds = mCollider.size;
        Vector2 targetBounds = new Vector2(15, 13);
        float t = 0;
        Vector2 startPos = mTransform.anchoredPosition;
        Vector2 targetPos = new Vector2(0, 4f);
        Vector2 startScale = mTransform.localScale;
        Vector2 targetScale = Vector2.one * 3;
        float startAngle = Pivot.localEulerAngles.z;
        float targetAngle = 0;
        if (reverce)
        {
            targetPos = Vector2.zero;
            targetScale = Vector2.zero;  
            targetAngle = -90;
            targetBounds = new Vector2(2f, 4f);
        }
        while(t < 1)
        {
            Vector2 newPos = Vector2.Lerp(startPos, targetPos, t);
            float newAngle = Mathf.LerpAngle(startAngle, targetAngle, t);
            Vector2 newSize = Vector2.Lerp(startScale, targetScale, t);
            Vector2 newbounds = Vector2.Lerp(startBounds, targetBounds, t);
            mCollider.size = newbounds;
            mTransform.localScale = newSize;
            mTransform.anchoredPosition = newPos;
            Pivot.localEulerAngles = new Vector3(0, 0, newAngle);
            t += Time.deltaTime * 2;
            yield return new WaitForEndOfFrame();
        }
    }
}
