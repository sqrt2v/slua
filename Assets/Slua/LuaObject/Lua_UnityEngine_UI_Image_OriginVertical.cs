﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_UI_Image_OriginVertical : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr l) {
		int v = LuaDLL.lua_tointeger(l, 1);
		UnityEngine.UI.Image.OriginVertical o = (UnityEngine.UI.Image.OriginVertical)v;
		pushValue(l,o);
		return 1;
	}
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.UI.Image.OriginVertical");
		addMember(l,0,"Bottom");
		addMember(l,1,"Top");
		addMember(l,IntToEnum, "IntToEnum");
		LuaDLL.lua_pop(l, 1);
	}
}
