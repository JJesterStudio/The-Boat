using UnityEngine;

public class Sail : MonoBehaviour
{
    [SerializeField]
    private GameObject boomUp;
    [SerializeField]
    private GameObject boomDown;
    [SerializeField]
    private GameObject sail;

    private float maxSailScale;
    private float minSailScale;
    private float maxBoomY;
    private float minBoomY;

    private void Start()
    {
        float temp = (boomDown.transform.localScale.z + boomUp.transform.localScale.z) / 2f;
        maxBoomY = boomDown.transform.localPosition.y;
        minBoomY = boomUp.transform.localPosition.y - temp;
        maxSailScale = sail.transform.localScale.y;
        minSailScale = temp;
    }

    public void SetSailPercentage(float percentage)
    {
        boomDown.transform.localPosition = new Vector3(boomDown.transform.localPosition.x, minBoomY + percentage * (maxBoomY - minBoomY), boomDown.transform.localPosition.z);
        sail.transform.localScale = new Vector3(sail.transform.localScale.x, minSailScale + percentage * (maxSailScale - minSailScale), sail.transform.localScale.z);
        sail.transform.localPosition = new Vector3(sail.transform.localPosition.x, (boomUp.transform.localPosition.y + boomDown.transform.localPosition.y) / 2f, sail.transform.localPosition.z);
    }
}
