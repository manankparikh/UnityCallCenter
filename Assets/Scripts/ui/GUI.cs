using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class GUI : MonoBehaviour
{
	private static readonly WaitForSeconds _eof = new WaitForSeconds( 0.125f );

	private static GUI _instance;
	internal static GUI Instance
	{
		get
		{
			return _instance;
		}
	}


	public RectTransform TextEnvelope;
    public Text TextLog;
	private StringWriter _log;
	private object lockObj = new object();

	internal void AppendLog( string text )
	{
		lock ( lockObj )
		{
			_log.WriteLine( text );
		}
	}

	private void UpdateLog()
	{
		lock ( lockObj )
		{
			TextLog.text = _log.ToString();
		}
		var tempString = TextLog.preferredHeight;
		var size = TextEnvelope.sizeDelta;
		size.y = tempString;
		TextEnvelope.sizeDelta = size;
	}

	private void Start()
	{
		_log = new StringWriter();
		_instance = this;

		StartCoroutine( PollLog() );
	}

	private void Update()
	{
		CCallCenter.Instance().Update();
	}

	private IEnumerator PollLog()
	{
		while ( true )
		{
			yield return _eof;
			UpdateLog();
		}
	}

	public void GenerateCall()
	{
		//Add 25 Freshers To The Call Center.
		for ( int i = 0; i < 25; i++ )
		{
			CCallCenter.Instance().AddEmployee( new CFresher() );
		}
        
		CCallCenter.Instance().AddEmployee( new CTeamLeader() );
		CCallCenter.Instance().AddEmployee( new CProjectManager() );

		//Generate 100 Calls.
		for ( int i = 0; i < 100; i++ )
		{
			CCallCenter.Instance().HandleCall( new CCall() );
		}
	}
}
