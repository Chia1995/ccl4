#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
//////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2014 Audiokinetic Inc. / All Rights Reserved
//
//////////////////////////////////////////////////////////////////////


using UnityEngine;

public class AkTriggerEnter : AkTriggerBase
{
	public UnityEngine.GameObject triggerObject = null;


    private void Start()
    {
        if (triggerObject == null)
        { 
            triggerObject = GameObject.FindWithTag("Player");
            Debug.Log($"TriggerEnter Init { transform.name } ---> {triggerObject.name} ");
        }
    }

    private void OnTriggerEnter(UnityEngine.Collider in_other)
	{

        if (triggerDelegate != null && ( triggerObject == null || triggerObject == in_other.gameObject))
        {
            Debug.Log($"TriggerEnter { transform.name } ---> {in_other.name} ");
            triggerDelegate(in_other.gameObject);
        }
	}
}

#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.