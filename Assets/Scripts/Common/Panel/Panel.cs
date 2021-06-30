using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug; 
public class Panel : MonoBehaviour
{
	static Panel instance = null;
	Dictionary<System.Type, GameObject> prefabs = new Dictionary<System.Type, GameObject>();
	Dictionary<System.Type, PanelItem> panels = new Dictionary<System.Type, PanelItem>();
	Dictionary<string, PanelItem> showingPanels = new Dictionary<string, PanelItem>();
	 
    private void Awake()
    {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
    }

    class PanelItem
	{
		public readonly MonoBehaviour Panel;
		public readonly string Group;
		public readonly System.Type Type;

		public PanelItem(MonoBehaviour panel, string group, System.Type type)
		{
			this.Panel = panel;
			this.Group = group;
			this.Type = type;
		}
	}

	public static void PreloadPrefab<T>() where T : MonoBehaviour
	{
		
		if (instance == null)
		{
			Debug.LogError("PanelRoot did not initialized");
			return;
		}

		System.Type panelType = typeof(T);
		GameObject prefab;
		if (instance.prefabs.TryGetValue(panelType, out prefab) == true)
		{
			Debug.LogWarning("already reserved " + panelType);
			return;
		}

		var prefabAttributes = panelType.GetCustomAttributes(typeof(UIPanelPrefab), false);
		if (prefabAttributes == null || prefabAttributes.Length <= 0)
		{
			Debug.LogError("Panel " + panelType + " has no valid attribute.");
			return;
		}

		UIPanelPrefab attribute = (UIPanelPrefab)prefabAttributes[0];

		prefab = (GameObject)Resources.Load(attribute.PrefabPath);
		if (prefab == null)
		{
			Debug.LogError("cannot load " + attribute.PrefabPath);
			return;
		}

		instance.prefabs[panelType] = prefab;
	}

	public static void Register<T>(T panel, bool clearPrefab = false, string groupName = null) where T : MonoBehaviour
	{
		if (instance == null)
		{
			Debug.LogError("PanelRoot did not initialized");
			return;
		}
		System.Type panelType = typeof(T);
		if (instance.panels.ContainsKey(panelType) == true)
		{
			Debug.Log("replacing panel " + panel.name);
		}

		if (string.IsNullOrEmpty(groupName) == true)
		{
			instance.panels[panelType] = new PanelItem(panel, "", panelType);
		}
		else
		{
			instance.panels[panelType] = new PanelItem(panel, groupName.ToLower(), panelType);
		}

		var autoUnregister = panel.gameObject.AddComponent<PanelAutoUnRegister>();
		autoUnregister.SetPanelType(panelType, clearPrefab);
	}

	public static void Unregister(System.Type panelType, bool clearPrefab)
	{
		if (instance == null)
			return;

		PanelItem item;
		string groupName = "";
		foreach (var panelitem in instance.showingPanels)
		{
			if (panelitem.Value.Type == panelType)
			{
				groupName = panelitem.Value.Group;
			}
		}
		if (string.IsNullOrEmpty(groupName) == false)
		{
			instance.showingPanels.Remove(groupName);
		}

		if (instance.panels.TryGetValue(panelType, out item) == false)
		{
			Debug.LogWarning("no item to Unregister " + panelType);
			return;
		}

		instance.panels.Remove(panelType);

		if (clearPrefab == true)
		{
			instance.prefabs.Remove(panelType);
		}

		//Debug.Log( "unregister " + panelType );
	}

	public static bool Contains(string PanelName)
	{
		if (instance == null)
		{
			return false;
		}

		System.Type panelType = System.Type.GetType(PanelName);
		return instance.panels.ContainsKey(panelType);
	}

	public static bool Contains<T>() where T : MonoBehaviour
	{
		if (instance == null)
		{
			return false;
		}
		System.Type panelType = typeof(T);
		return instance.panels.ContainsKey(panelType);
	}

	public static T Get<T>() where T : MonoBehaviour
	{
		if (instance == null)
		{
			Debug.LogError("PanelRoot did not initialized");
			return null;
		}

		PanelItem item;
		System.Type panelType = typeof(T);
		if (instance.panels.TryGetValue(panelType, out item) == false)
		{
			T script = tryCreate<T>();
			if (script == null)
			{
				Debug.Log("cannot create " + panelType);
			}
			if (instance.panels.ContainsKey(panelType) == false)
			{
				Debug.Log("not registered panel " + panelType);
				return null;
			}
			return script;
		}

		return (T)item.Panel;
	}

	static T tryCreate<T>() where T : MonoBehaviour
	{
		System.Type panelType = typeof(T);
		var prefabAttributes = panelType.GetCustomAttributes(typeof(UIPanelPrefab), false);
		if (prefabAttributes == null || prefabAttributes.Length <= 0)
		{
			Debug.LogError("Panel " + panelType + " has no valid attribute.");
			return null;
		}

		UIPanelPrefab attribute = (UIPanelPrefab)prefabAttributes[0];

		GameObject prefab;
		if (instance.prefabs.TryGetValue(panelType, out prefab) == false)
		{
			prefab = (GameObject)Resources.Load(attribute.PrefabPath);
			if (prefab == null)
			{
				Debug.LogError("cannot load " + attribute.PrefabPath);
				return null;
			}

			instance.prefabs[panelType] = prefab;
		}

		GameObject anchor = GameObject.Find(attribute.AnchorName);
		if (anchor == null)
		{
#if UNITY_EDITOR
			Debug.LogWarning("cannot find anchor " + attribute.AnchorName);
#endif
			anchor = GameObject.Find("Anchor");
			if (anchor == null)
			{
                UIAnchor anchorObject = GameObject.FindObjectOfType(typeof(UIAnchor)) as UIAnchor;
                if (anchorObject != null)
                {
                    anchor = anchorObject.gameObject;
                }
            }
			if (anchor == null)
			{
#if UNITY_EDITOR
				Debug.LogError("cannot find any anchor in this scene");
#endif
			}
		}

		GameObject panel = AddChild(anchor, prefab);
		panel.transform.localPosition = prefab.transform.localPosition;
		var script = panel.GetComponent<T>();
		if (script == null)
		{
			Debug.LogError("panel " + panelType + " has no script in prefab");
		}
		return script;
	}

	public static T Show<T>(ShowHidePanel.onEventHandler onShowBegin, ShowHidePanel.onEventHandler onShowFinished) where T : MonoBehaviour
	{
		if (instance == null)
		{
			Debug.LogError("PanelRoot did not initialized");
			return null;
		}

		PanelItem item;
		System.Type panelType = typeof(T);
		if (instance.panels.TryGetValue(panelType, out item) == false)
		{
			T script = tryCreate<T>();
			if (script == null)
			{
				Debug.Log("cannot create " + panelType);
				return null;
			}
			if (instance.panels.ContainsKey(panelType) == false)
			{
				Debug.Log("not registered panel " + panelType);
				return null;
			}
			return Show<T>(onShowBegin, onShowFinished);
		}

		bool hasGroup = (string.IsNullOrEmpty(item.Group) == false);
		if (hasGroup == true)
		{
			PanelItem oldPanelItem;
			if (instance.showingPanels.TryGetValue(item.Group, out oldPanelItem) == true)
			{
				hidePanel(oldPanelItem.Panel);
			}
			instance.showingPanels[item.Group] = new PanelItem(item.Panel, item.Group, item.Type);
		}

		showPanel(item.Panel, onShowBegin, onShowFinished);
		return (T)item.Panel;
	}

	public static T Show<T>() where T : MonoBehaviour
	{
		if (instance == null)
		{
			Debug.LogError("PanelRoot did not initialized");
			return null;
		}

		PanelItem item;
		System.Type panelType = typeof(T);
		if (instance.panels.TryGetValue(panelType, out item) == false)
		{
			T script = tryCreate<T>();
			if (script == null)
			{
				Debug.Log("cannot create " + panelType);
				return null;
			}
			if (instance.panels.ContainsKey(panelType) == false)
			{
#if UNITY_EDITOR
				Debug.LogWarning("not registered panel " + panelType + ".  need register to PanelRoot from Awake()");
#else
				Debug.Log( "not registered panel " + panelType );
#endif
				return null;
			}
			return Show<T>();
		}

		bool hasGroup = (string.IsNullOrEmpty(item.Group) == false);
		if (hasGroup == true)
		{
			PanelItem oldPanelItem;
			if (instance.showingPanels.TryGetValue(item.Group, out oldPanelItem) == true)
			{
				hidePanel(oldPanelItem.Panel);
			}
			instance.showingPanels[item.Group] = new PanelItem(item.Panel, item.Group, item.Type);
		}
#if _DEV
        Debug.Log("popup item : " + item.GetType() + " - " + item.Group + " - " + item.Panel.name + " - " + item.Type + " - " + item.ToString());
#endif
		showPanel(item.Panel);
		return (T)item.Panel;
	}

	public static void Hide<T>(ShowHidePanel.onEventHandler onHideBegin, ShowHidePanel.onEventHandler onHideFinished) where T : MonoBehaviour
	{
		Hide(typeof(T), onHideBegin, onHideFinished);
	}

	public static void Hide<T>() where T : MonoBehaviour
	{
		Hide(typeof(T));
	}

	public static void Hide(System.Type type, ShowHidePanel.onEventHandler onHideBegin, ShowHidePanel.onEventHandler onHideFinished)
	{
		if (instance == null)
		{
			Debug.LogError("PanelRoot did not initialized");
			return;
		}

		PanelItem item;
		if (instance.panels.TryGetValue(type, out item) == false)
		{
			Debug.LogWarning("Panel " + type + " is not registered");
			//Debug.LogError( "Panel " + type + " is not registered" );
			return;
		}

		bool hasGroup = (string.IsNullOrEmpty(item.Group) == false);
		if (hasGroup == true)
		{
			PanelItem oldPanelItem;
			if (instance.showingPanels.TryGetValue(item.Group, out oldPanelItem) == true)
			{
				instance.showingPanels.Remove(item.Group);
				//if ( item.Panel != oldPanel )
				{
					//if ( item.Panel.GetType() != oldPanel.GetType() )
					{
						Debug.LogWarning("try hide : old panel " + oldPanelItem.Type + " new Panel " + item.Panel.GetType());
					}
				}
			}
		}

		hidePanel(item.Panel, onHideBegin, onHideFinished);
	}

	public static void Hide(System.Type type)
	{
		if (instance == null)
		{
			Debug.LogError("PanelRoot did not initialized");
			return;
		}

		PanelItem item;
		if (instance.panels.TryGetValue(type, out item) == false)
		{
			Debug.LogWarning("Panel " + type + " is not registered");
			//Debug.LogError( "Panel " + type + " is not registered" );
			return;
		}

		bool hasGroup = (string.IsNullOrEmpty(item.Group) == false);
		if (hasGroup == true)
		{
			PanelItem oldPanelItem;
			if (instance.showingPanels.TryGetValue(item.Group, out oldPanelItem) == true)
			{
				instance.showingPanels.Remove(item.Group);
				//if ( item.Panel != oldPanel )
				{
					//if ( item.Panel.GetType() != oldPanel.GetType() )
					{
						Debug.LogWarning("try hide : old panel " + oldPanelItem.Type + " new Panel " + item.Panel.GetType());
					}
				}
			}
		}

		hidePanel(item.Panel);
	}

	public static void Hide(MonoBehaviour panelScript)
	{
		Hide(panelScript.GetType());
	}

	public static void HidePanelGroup(string groupName)
	{
		if (instance == null)
		{
			Debug.LogError("PanelRoot did not initialized");
			return;
		}
		if (string.IsNullOrEmpty(groupName) == true)
		{
			return;
		}

		PanelItem oldPanelItem;
		if (instance.showingPanels.TryGetValue(groupName.ToLower(), out oldPanelItem) == false)
		{
			return;
		}

		instance.showingPanels.Remove(groupName);
		hidePanel(oldPanelItem.Panel);
	}

	static void hidePanel(MonoBehaviour panel, ShowHidePanel.onEventHandler onHideBegin, ShowHidePanel.onEventHandler onHideFinished)
	{
		var script = panel.GetComponent<ShowHidePanel>();
		if (script == null)
		{
			panel.gameObject.SetActive(false);
			return;
		}

		script.onHideBegin = onHideBegin;
		script.onHideFinished = onHideFinished;
		script.Hide();
	}

	static void hidePanel(MonoBehaviour panel)
	{
		var script = panel.GetComponent<ShowHidePanel>();
		if (script == null)
		{
			panel.gameObject.SetActive(false);
			return;
		}

		script.Hide();
	}

	static void showPanel(MonoBehaviour panel, ShowHidePanel.onEventHandler onShowBegin, ShowHidePanel.onEventHandler onShowFinished)
	{
		var script = panel.GetComponent<ShowHidePanel>();
		if (script == null)
		{
			panel.gameObject.SetActive(true);
			return;
		}

		script.onShowBegin = onShowBegin;
		script.onShowFinished = onShowFinished;
		script.Show();
	}

	static void showPanel(MonoBehaviour panel)
	{
		var script = panel.GetComponent<ShowHidePanel>();
		if (script == null)
		{
			panel.gameObject.SetActive(true);
			return;
		}

		script.Show();
	}

	static public GameObject AddChild(GameObject parent, GameObject prefab)
	{
		GameObject go = GameObject.Instantiate(prefab) as GameObject;

		if (go != null && parent != null)
		{
			Transform t = go.transform;
			t.parent = parent.transform;
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			t.localScale = Vector3.one;
			go.layer = parent.layer;
		}
		return go;
	}

	void OnDestroy()
	{
		panels.Clear();
		showingPanels.Clear();
		prefabs.Clear();
		instance = null;
	}

	public class PanelAutoUnRegister : MonoBehaviour
	{
		System.Type panelType = null;

		bool clearPrefab = false;

		public void SetPanelType(System.Type type, bool clearPrefab)
		{
			this.panelType = type;

			this.clearPrefab = clearPrefab;
		}

		void OnDestroy()
		{
			Panel.Unregister(this.panelType, clearPrefab);
		}
	}

}

public class UIPanelPrefab : System.Attribute
{
	public readonly string PrefabPath = "";
	public readonly string AnchorName = "";

	public UIPanelPrefab(string prefabPath, string anchorName)
	{
		this.PrefabPath = prefabPath;
		this.AnchorName = anchorName;
	}
}

