using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TextDependentImageSize : MonoBehaviour
{
    [Header("References")]
    [Tooltip("크기를 기준으로 삼을 Text 컴포넌트")]
    public Text targetText;
    [Tooltip("크기를 변경할 Image의 RectTransform")]
    public RectTransform imageRect;

    [Header("Padding")]
    [Tooltip("텍스트 너비에 더할 여백(px)")]
    public float horizontalPadding = 20f;
    [Tooltip("텍스트 높이에 더할 여백(px)")]
    public float verticalPadding = 10f;

    private void LateUpdate()
    {
        if (targetText == null || imageRect == null) return;

        // 1) 텍스트의 실제 렌더 가능한 크기
        float w = targetText.preferredWidth;
        float h = targetText.preferredHeight;

        // 2) 이미지 RectTransform에 적용 (Anchor 방식에 따라 SetSizeWithCurrentAnchors 사용)
        imageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w + horizontalPadding);
        imageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,   h + verticalPadding);
    }
}