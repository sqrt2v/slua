﻿using UnityEngine;
using System;
using LuaInterface;
using SLua;
using System.Collections.Generic;
public class Lua_UnityEngine_RenderSettings : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		LuaDLL.lua_remove(l,1);
		UnityEngine.RenderSettings o;
		if(matchType(l,1)){
			o=new UnityEngine.RenderSettings();
			pushObject(l,o);
			return 1;
		}
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_fog(IntPtr l) {
		pushValue(l,UnityEngine.RenderSettings.fog);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_fog(IntPtr l) {
		bool v;
		checkType(l,2,out v);
		UnityEngine.RenderSettings.fog=v;
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_fogMode(IntPtr l) {
		pushValue(l,UnityEngine.RenderSettings.fogMode);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_fogMode(IntPtr l) {
		UnityEngine.FogMode v;
		checkEnum(l,2,out v);
		UnityEngine.RenderSettings.fogMode=v;
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_fogColor(IntPtr l) {
		pushValue(l,UnityEngine.RenderSettings.fogColor);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_fogColor(IntPtr l) {
		UnityEngine.Color v;
		checkType(l,2,out v);
		UnityEngine.RenderSettings.fogColor=v;
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_fogDensity(IntPtr l) {
		pushValue(l,UnityEngine.RenderSettings.fogDensity);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_fogDensity(IntPtr l) {
		float v;
		checkType(l,2,out v);
		UnityEngine.RenderSettings.fogDensity=v;
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_fogStartDistance(IntPtr l) {
		pushValue(l,UnityEngine.RenderSettings.fogStartDistance);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_fogStartDistance(IntPtr l) {
		float v;
		checkType(l,2,out v);
		UnityEngine.RenderSettings.fogStartDistance=v;
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_fogEndDistance(IntPtr l) {
		pushValue(l,UnityEngine.RenderSettings.fogEndDistance);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_fogEndDistance(IntPtr l) {
		float v;
		checkType(l,2,out v);
		UnityEngine.RenderSettings.fogEndDistance=v;
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_ambientLight(IntPtr l) {
		pushValue(l,UnityEngine.RenderSettings.ambientLight);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_ambientLight(IntPtr l) {
		UnityEngine.Color v;
		checkType(l,2,out v);
		UnityEngine.RenderSettings.ambientLight=v;
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_haloStrength(IntPtr l) {
		pushValue(l,UnityEngine.RenderSettings.haloStrength);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_haloStrength(IntPtr l) {
		float v;
		checkType(l,2,out v);
		UnityEngine.RenderSettings.haloStrength=v;
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_flareStrength(IntPtr l) {
		pushValue(l,UnityEngine.RenderSettings.flareStrength);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_flareStrength(IntPtr l) {
		float v;
		checkType(l,2,out v);
		UnityEngine.RenderSettings.flareStrength=v;
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_flareFadeSpeed(IntPtr l) {
		pushValue(l,UnityEngine.RenderSettings.flareFadeSpeed);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_flareFadeSpeed(IntPtr l) {
		float v;
		checkType(l,2,out v);
		UnityEngine.RenderSettings.flareFadeSpeed=v;
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_skybox(IntPtr l) {
		pushValue(l,UnityEngine.RenderSettings.skybox);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_skybox(IntPtr l) {
		UnityEngine.Material v;
		checkType(l,2,out v);
		UnityEngine.RenderSettings.skybox=v;
		return 0;
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.RenderSettings");
		addMember(l,"fog",get_fog,set_fog);
		addMember(l,"fogMode",get_fogMode,set_fogMode);
		addMember(l,"fogColor",get_fogColor,set_fogColor);
		addMember(l,"fogDensity",get_fogDensity,set_fogDensity);
		addMember(l,"fogStartDistance",get_fogStartDistance,set_fogStartDistance);
		addMember(l,"fogEndDistance",get_fogEndDistance,set_fogEndDistance);
		addMember(l,"ambientLight",get_ambientLight,set_ambientLight);
		addMember(l,"haloStrength",get_haloStrength,set_haloStrength);
		addMember(l,"flareStrength",get_flareStrength,set_flareStrength);
		addMember(l,"flareFadeSpeed",get_flareFadeSpeed,set_flareFadeSpeed);
		addMember(l,"skybox",get_skybox,set_skybox);
		createTypeMetatable(l,constructor, typeof(UnityEngine.RenderSettings),typeof(UnityEngine.Object));
	}
}
