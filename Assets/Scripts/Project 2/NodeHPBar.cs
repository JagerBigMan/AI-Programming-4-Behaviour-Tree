using UnityEngine;
using UnityEngine.UI;

public class NodeHPBar : MonoBehaviour
{
    public Image fillImage;
    public Vector3 worldOffset = new Vector3(0f, 2f, 0f);

    private RepairNode targetNode;
    private Transform anchor;
    private Camera mainCam;

    public void Initialize(RepairNode node, Transform followAnchor)
    {
        targetNode = node;
        anchor = followAnchor;
        mainCam = Camera.main;
        Refresh();
    }

    private void Update()
    {
        if (targetNode == null || anchor == null)
        {
            Destroy(gameObject);
            return;
        }

        if (mainCam == null)
            mainCam = Camera.main;

        if (mainCam == null) return;

        Vector3 worldPos = anchor.position + worldOffset;
        Vector3 screenPos = mainCam.WorldToScreenPoint(worldPos);

        transform.position = screenPos;
        gameObject.SetActive(screenPos.z > 0f);
    }

    public void Refresh()
    {
        if (targetNode == null || fillImage == null) return;
        fillImage.fillAmount = targetNode.GetHPPercent();
    }
}