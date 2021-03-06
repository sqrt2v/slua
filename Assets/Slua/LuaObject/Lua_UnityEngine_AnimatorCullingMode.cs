﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_AnimatorCullingMode : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr l) {
		int v = LuaDLL.lua_tointeger(l, 1);
		UnityEngine.AnimatorCullingMode o = (UnityEngine.AnimatorCullingMode)v;
		pushValue(l,o);
		return 1;
	}
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.AnimatorCullingMode");
		addMember(l,0,"AlwaysAnimate");
		addMember(l,1,"BasedOnRenderers");
		addMember(l,IntToEnum, "IntToEnum");
		LuaDLL.lua_pop(l, 1);
	}
}
