﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_Rendering_CullMode : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr l) {
		int v = LuaDLL.lua_tointeger(l, 1);
		UnityEngine.Rendering.CullMode o = (UnityEngine.Rendering.CullMode)v;
		pushValue(l,o);
		return 1;
	}
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.Rendering.CullMode");
		addMember(l,0,"Off");
		addMember(l,1,"Front");
		addMember(l,2,"Back");
		addMember(l,IntToEnum, "IntToEnum");
		LuaDLL.lua_pop(l, 1);
	}
}
