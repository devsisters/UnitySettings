#if DEBUG
using System;
using UnityEngine;

public class ReporterGUI : MonoBehaviour {
	public static class Events {
		public static Func<string> getBuildInfo;
		public static Action<ReporterGUI> onStart;
		public static Action<ReporterGUI> onDestroy;
		public static Action<ReporterGUI> onEnable;
		public static Action<ReporterGUI> onDisable;
	}

	public static ReporterGUI inst;

	Reporter reporter ;

	void Awake()
	{
		if (inst == null) inst = this;
		reporter = gameObject.GetComponent<Reporter>();
	}

	void Start()
	{
		if (Events.onStart != null)
			Events.onStart(this);
	}

	void OnDestroy()
	{
		if (inst == this) inst = null;
		if (Events.onDestroy != null)
			Events.onDestroy(this);
	}

	void OnEnable()
	{
		if (Events.onEnable != null)
			Events.onEnable(this);
	}

	void OnDisable()
	{
		if (Events.onDisable != null)
			Events.onDisable(this);
	}

	void OnGUI()
	{
		reporter.OnGUIDraw();
	}
}
#endif