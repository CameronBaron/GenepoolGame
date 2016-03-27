using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public RectTransform healthBarTransform;
	public Image visualHealthBar;
	public Vector2 offset = new Vector2(0, 0);
	Vector2 resizeRatio;
	[HideInInspector]	public GameObject playerRef;

	private int currentHealth;
	private Health health;
	private GameObject[] players;
	private float healthBarY;
	private float healthBarMinX;
	private float healthBarMaxX;

	private Image framRenderer;
	private Image backgroundRenderer;
	private Image healthRenderer;
	private Color currentColour;
	private float fadeTime = 1.0f;
	private float currentFadeTime = 0.0f;

    private RectTransform m_RectTransform;

	// Use this for initialization
	void Start ()
	{
        m_RectTransform = gameObject.GetComponent<RectTransform>();
		backgroundRenderer = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
		healthRenderer = transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>();
		framRenderer = transform.GetComponent<Image>();

		players = GameObject.FindGameObjectsWithTag("Player");
		for (int i = 0; i < players.Length; i++)
		{
			if (int.Parse(name[name.Length - 1].ToString()) == players[i].GetComponent<Player>().playerID)
			{
				playerRef = players[i];
            }
		}

		if (playerRef != null)
		{
			//GetComponent<RectTransform>().transform.position = playerRef.transform.position;
			health = playerRef.GetComponent<Health>();
			healthBarY = healthBarTransform.localPosition.y;
			healthBarMaxX = healthBarTransform.localPosition.x;
			healthBarMinX = healthBarTransform.localPosition.x - healthBarTransform.rect.width;

			currentHealth = health.currentHP;
        }
		else
		{
			gameObject.SetActive(false);
		}
	}

	void Update()
	{
		if (currentHealth <= 0)
		{
			Color current = backgroundRenderer.color;
			current.a = 0.0f;
			backgroundRenderer.color = current;

			current = healthRenderer.color;
			current.a = 0.0f;
			healthRenderer.color = current;

			current = framRenderer.color;
			current.a = 0.0f;
			framRenderer.color = current;
			//if(currentFadeTime < fadeTime)
			//{
			//	currentFadeTime += Time.deltaTime;
			//	float t = currentFadeTime / fadeTime;
			//	backgroundRenderer.color = Color.Lerp(new Color(79.0f / 255, 79.0f / 255, 79.0f / 255, 1.0f), new Color(79.0f / 255, 79.0f / 255, 79.0f / 255, 0.0f), t);
			//	healthRenderer.color = Color.Lerp(Color.red, new Color(79.0f / 255, 79.0f / 255, 79.0f / 255, 0.0f), t);
			//}
		}
		else if (currentHealth == health.maxHP)
		{
			Color current = backgroundRenderer.color;
			current.a = 1.0f;
			backgroundRenderer.color = current;

			current = healthRenderer.color;
			current.a = 1.0f;
			healthRenderer.color = current;

			current = framRenderer.color;
			current.a = 1.0f;
			framRenderer.color = current;
		}

		if (currentHealth != health.currentHP)
		{
			HandleHealthBar();
		}

        Vector3 sp = Camera.main.WorldToViewportPoint(playerRef.transform.localPosition);
        GameObject go = GameObject.FindGameObjectWithTag("Canvas");
        RectTransform canRect = go.GetComponent<RectTransform>();

        Vector3 ap = new Vector3(
            (sp.x * canRect.sizeDelta.x) - (canRect.sizeDelta.x * 0.5f),
            (sp.y * canRect.sizeDelta.y) - (canRect.sizeDelta.y * 0.5f));

        m_RectTransform.anchoredPosition = ap;

        //Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, playerRef.transform.position);
        //gameObject.GetComponent<RectTransform>().anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2;
	}

	public void HandleHealthBar()
	{
		float currentXVale = MapHealthBarValues(health.currentHP, 0, health.maxHP, healthBarMinX, healthBarMaxX);
		healthBarTransform.localPosition = new Vector3(currentXVale, healthBarY);

		if (health.currentHP > health.maxHP / 2)
		{
			visualHealthBar.color = new Color32((byte)MapHealthBarValues(health.currentHP, health.maxHP / 2, health.maxHP, 255, 0), 255, 0, 255);
		}
		else
		{
			visualHealthBar.color = new Color32(255, (byte)MapHealthBarValues(health.currentHP, 0, health.maxHP / 2, 0, 255), 0, 255);
		}

		currentHealth = health.currentHP;
	}

	private float MapHealthBarValues(float x, float inMin, float inMax, float outMin, float outMax)
	{
		return ((x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin);
	}
}
