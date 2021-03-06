﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_DeviceOrientation : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr l) {
		int v = LuaDLL.lua_tointeger(l, 1);
		UnityEngine.DeviceOrientation o = (UnityEngine.DeviceOrientation)v;
		pushValue(l,o);
		return 1;
	}
	static public void reg(IntPtr l) {
		getEnumTable(l,"UnityEngine.DeviceOrientation");
		addMember(l,0,"Unknown");
		addMember(l,1,"Portrait");
		addMember(l,2,"PortraitUpsideDown");
		addMember(l,3,"LandscapeLeft");
		addMember(l,4,"LandscapeRight");
		addMember(l,5,"FaceUp");
		addMember(l,6,"FaceDown");
		addMember(l,IntToEnum, "IntToEnum");
		LuaDLL.lua_pop(l, 1);
	}
}
