using DG.Tweening;
using UnityEngine;

public class InGameUIOrientationPortrait : InGameUIOrientationVariable
{
	private readonly float StarPositionLength = 149f;

	public Transform AnchorBoardPositionTop;

	public Transform AnchorBoardPositionBottom;

	public Transform PositionTopInfo;

	public RectTransform TweenValueCollectInfo;

	private float tweenValueCollectInfoStartX;

	public RectTransform TweenValueLevelInfo;

	private float tweenValueLevelInfoStartX;

	public RectTransform TweenValueMoveInfo;

	private float tweenValueMoveInfoStartY;

	public RectTransform TweenValueScoreInfo;

	private float tweenValueScoreInfoStartX;

	public Camera cam;

	public override void Start()
	{
		base.Start();
		Invoke(nameof(CamFix), 0.5f);
		orientationType = OrientationType.Portrait;
		if ((float)Screen.width / (float)Screen.height == 0.75f || MonoSingleton<PlayerDataManager>.Instance.IsIPad)
		{
			Transform positionTopInfo = PositionTopInfo;
			Vector3 localPosition = PositionTopInfo.localPosition;
			float x = localPosition.x;
			Vector3 localPosition2 = PositionTopInfo.localPosition;
			positionTopInfo.localPosition = new Vector3(x, 253f, localPosition2.z);
		}
	}

	public void CamFix()
	{
        switch (levelNow)
        {
            case 1:
                cam.orthographicSize = 480f;
                break;
            case 2:
                cam.orthographicSize = 480f;
                break;
            case 3:
                cam.orthographicSize = 480f;
                break;
            case 4:
                cam.orthographicSize = 480f;
                break;
            case 5:
                cam.orthographicSize = 480f;
                break;
            case 6:
                cam.orthographicSize = 580f;
                break;
            case 7:
                cam.orthographicSize = 680f;
                break;
            case 8:
                cam.orthographicSize = 500f;
                break;
            case 9:
                cam.orthographicSize = 580f;
                break;
            case 10:
                cam.orthographicSize = 580f;
                break;
            case 11:
                cam.orthographicSize = 580f;
                break;
            case 12:
                cam.orthographicSize = 580f;
                break;
            case 13:
                cam.orthographicSize = 480f;
                break;
            case 14:
                cam.orthographicSize = 580f;
                break;
            case 15:
                cam.orthographicSize = 580f;
                break;
            case 16:
                cam.orthographicSize = 580f;
                break;
            case 17:
                cam.orthographicSize = 580f;
                break;
            case 18:
                cam.orthographicSize = 500f;
                break;
            case 19:
                cam.orthographicSize = 580f;
                break;
            case 20:
                cam.orthographicSize = 580f;
                break;
            case 21:
                cam.orthographicSize = 580f;
                break;
            case 22:
                cam.orthographicSize = 480f;
                break;
            case 23:
                cam.orthographicSize = 480f;
                break;
            case 24:
                cam.orthographicSize = 580f;
                break;
            case 25:
                cam.orthographicSize = 580f;
                break;
            default:
                cam.orthographicSize = 680f;
                break;
        }
    }

	public override void InitUITween()
	{
		base.InitUITween();
		if ((bool)TweenValueScoreInfo)
		{
			Vector2 anchoredPosition = TweenValueScoreInfo.anchoredPosition;
			tweenValueScoreInfoStartX = anchoredPosition.x;
			RectTransform tweenValueScoreInfo = TweenValueScoreInfo;
			Vector2 anchoredPosition2 = TweenValueScoreInfo.anchoredPosition;
			tweenValueScoreInfo.anchoredPosition = new Vector2(580f, anchoredPosition2.y);
		}
		if ((bool)TweenValueCollectInfo)
		{
			Vector2 anchoredPosition3 = TweenValueCollectInfo.anchoredPosition;
			tweenValueCollectInfoStartX = anchoredPosition3.x;
			RectTransform tweenValueCollectInfo = TweenValueCollectInfo;
			Vector2 anchoredPosition4 = TweenValueCollectInfo.anchoredPosition;
			tweenValueCollectInfo.anchoredPosition = new Vector2(500f, anchoredPosition4.y);
		}
		if ((bool)TweenValueMoveInfo)
		{
			Vector2 anchoredPosition5 = TweenValueMoveInfo.anchoredPosition;
			tweenValueMoveInfoStartY = anchoredPosition5.y;
			RectTransform tweenValueMoveInfo = TweenValueMoveInfo;
			Vector2 anchoredPosition6 = TweenValueMoveInfo.anchoredPosition;
			tweenValueMoveInfo.anchoredPosition = new Vector2(anchoredPosition6.x, 510f);
		}
		if ((bool)TweenValueLevelInfo)
		{
			Vector2 anchoredPosition7 = TweenValueLevelInfo.anchoredPosition;
			tweenValueLevelInfoStartX = anchoredPosition7.x;
			RectTransform tweenValueLevelInfo = TweenValueLevelInfo;
			Vector2 anchoredPosition8 = TweenValueLevelInfo.anchoredPosition;
			tweenValueLevelInfo.anchoredPosition = new Vector2(720f, anchoredPosition8.y);
		}
	}

	public override void ShowUITween()
	{
		GameMain.main.UIGameCamera.gameObject.SetActive(value: true);
		base.ShowUITween();
		if ((bool)TweenValueScoreInfo)
		{
			TweenValueScoreInfo.DOAnchorPosX(tweenValueScoreInfoStartX, 0.6f, snapping: true).SetEase(Ease.OutQuart).SetDelay(0.8f);
		}
		if ((bool)TweenValueCollectInfo)
		{
			TweenValueCollectInfo.DOAnchorPosX(tweenValueCollectInfoStartX, 0.6f, snapping: true).SetEase(Ease.OutQuart).SetDelay(0.8f);
		}
		if ((bool)TweenValueMoveInfo)
		{
			TweenValueMoveInfo.DOAnchorPosY(tweenValueMoveInfoStartY, 0.6f, snapping: true).SetEase(Ease.OutBack).SetDelay(0.9f);
		}
		if ((bool)TweenValueLevelInfo)
		{
			TweenValueLevelInfo.DOAnchorPosX(tweenValueLevelInfoStartX, 0.6f, snapping: true).SetEase(Ease.OutQuart).SetDelay(0.8f);
		}
		base.transform.DOScale(new Vector3(1.3f, 1.3f, 1f), 0.8f).SetEase(Ease.OutQuad).SetDelay(0.1f)
			.From();
	}

	public Vector3 GetBoardPosition()
	{
		if (Camera.main == null || GameMain.main.UIGameCamera == null || AnchorBoardPositionTop == null || AnchorBoardPositionBottom == null)
		{
			return Vector3.zero;
		}
		Vector3 position = AnchorBoardPositionTop.position;
		Vector3 position2 = AnchorBoardPositionTop.position;
		float y = position2.y;
		Vector3 position3 = AnchorBoardPositionTop.position;
		float y2 = position3.y;
		Vector3 position4 = AnchorBoardPositionBottom.position;
		position.y = y - (y2 - position4.y) / 2f;
		return Camera.main.ViewportToWorldPoint(GameMain.main.UIGameCamera.WorldToViewportPoint(position));
	}
}
