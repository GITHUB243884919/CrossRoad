using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Bind Some Delegate Func For Yours.
/// </summary>
public class UIBind : MonoBehaviour
{
	static bool isBind = false;

	public static void Bind()
	{
		if (!isBind)
		{
			isBind = true;
			//bind for your loader api to load UI.
			PageMgr.delegateSyncLoadUI = Resources.Load;
			//PageMgr.delegateAsyncLoadUI = UILoader.Load;
		}
	}
}