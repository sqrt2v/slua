﻿// The MIT License (MIT)

// Copyright 2015 Siney/Pangweiwei siney@yeah.net
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LuaInterface;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SLua
{


    class LuaVarObject : LuaObject
    {

        class MethodWrapper
        {
            object self;
            MemberInfo[] mis;
            public MethodWrapper(object self, MemberInfo[] mi)
            {
                this.self = self;
                this.mis = mi;
            }

            bool matchType(IntPtr l, int p, LuaTypes lt, Type t)
            {
                string tn = t.Name;
                
                switch (tn)
                {
                    case "String":
                        return lt == LuaTypes.LUA_TSTRING;
                    case "Int32":
                    case "Uint32":
                    case "Single":
                    case "Double":
                        return lt == LuaTypes.LUA_TNUMBER;
                    default:
                        return lt == LuaTypes.LUA_TUSERDATA;
                }
            }

            object checkVar(IntPtr l, int p, Type t)
            {
                string tn = t.Name;

                switch (tn)
                {
                    case "String":
                        return LuaDLL.lua_tostring(l, p);
                    case "Int32":
                        return (int)LuaDLL.lua_tointeger(l, p);
                    case "Uint32":
                        return (uint)LuaDLL.lua_tointeger(l, p);
                    case "Single":
                        return (float)LuaDLL.lua_tonumber(l, p);
                    case "Double":
                        return (double)LuaDLL.lua_tonumber(l, p);

                    default:
                        object o;
                        checkType(l, p, out o);
                        return o;
                }
            }

            internal bool matchType(IntPtr l, int from, ParameterInfo[] pis)
            {
                int top = LuaDLL.lua_gettop(l);
                if (top - from + 1 != pis.Length)
                    return false;

                for (int n = 0; n < pis.Length; n++)
                {
                    int p = n + from;
                    LuaTypes t = LuaDLL.lua_type(l, p);
                    if (!matchType(l, p, t, pis[n].ParameterType))
                        return false;
                }
                return true;
            }

            public int invoke(IntPtr l)
            {
                for (int k = 0; k < mis.Length;k++) {
                    MethodInfo m = (MethodInfo) mis[k];
                    if(matchType(l,3,m.GetParameters())) {
                        object[] args;
                        checkArgs(l, 3, m, out args);
                        object ret = m.Invoke(self, args);
                        if (ret != null)
                        {
                            pushVar(l, ret);
                            return 1;
                        }
                        return 0;
                    }
                }
                LuaDLL.luaL_error(l,"Can't find valid overload function {0} to invoke or parameter type mis-matched.",mis[0].Name);
                return 0;
            }

            public void checkArgs(IntPtr l, int from, MethodInfo m, out object[] args)
            {
                ParameterInfo[] ps = m.GetParameters();
                args = new object[ps.Length];
                int k=0;
                for (int n = from; n <= LuaDLL.lua_gettop(l); n++)
                {
                    args[k] = checkVar(l, n, ps[k].ParameterType );
                    k++;
                }
            }
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static public int luaIndex(IntPtr l)
        {
            ObjectCache oc = ObjectCache.get(l);
            object self = oc.get(l, 1);

            LuaTypes t = LuaDLL.lua_type(l, 2);
            switch (t)
            {
                case LuaTypes.LUA_TSTRING:
                    return indexString(l, self, LuaDLL.lua_tostring(l,2));
                case LuaTypes.LUA_TNUMBER:
                    return indexInt(l, self, LuaDLL.lua_tointeger(l,2));

                default:
                    LuaDLL.luaL_error(l, "Invalid member get");
                    return 0;
            }
        }

        static int indexString(IntPtr l, object self, string key)
        {
            Type t = self.GetType();
            MemberInfo[] mis = t.GetMember(key, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
            if (mis.Length == 0)
            {
                LuaDLL.luaL_error(l, "Can't find " + key);
            }

            MemberInfo mi = mis[0];
            switch (mi.MemberType)
            {
                case MemberTypes.Property:
                    PropertyInfo p = (PropertyInfo)mi;
                    pushVar(l, p.GetValue(self,null));
                    break;
                case MemberTypes.Field:
                    FieldInfo f = (FieldInfo)mi;
                    pushVar(l, f.GetValue(self));
                    break;
                case MemberTypes.Method:
                    pushValue(l, new MethodWrapper(self, mis).invoke);
                    break;
                case MemberTypes.Event:
                    break;
                default:
                    return 0;
            }

            return 1;
            
        }

        static int indexInt(IntPtr l, object self, int index)
        {
            return 0;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static public int luaNewIndex(IntPtr l)
        {
            return 0;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static public int methodWrapper(IntPtr l)
        {
            try
            {
                ObjectCache oc = ObjectCache.get(l);
                LuaCSFunction func = (LuaCSFunction)oc.get(l, 1);
                return func(l);
            }
            catch (Exception e)
            {
                LuaDLL.luaL_error(l, e.ToString());
                return 0;
            }
        }

        static internal void init(IntPtr l)
        {
            LuaDLL.lua_newtable(l);
            LuaDLL.lua_pushstdcallcfunction(l, luaIndex);
            LuaDLL.lua_setfield(l, -2, "__index");
            LuaDLL.lua_pushstdcallcfunction(l, luaIndex);
            LuaDLL.lua_setfield(l, -2, "__newindex");
            LuaDLL.lua_pushstdcallcfunction(l, luaGC);
            LuaDLL.lua_setfield(l, -2, "__gc");
            LuaDLL.lua_setfield(l, LuaIndexes.LUA_REGISTRYINDEX, "LuaVarObject");

            LuaDLL.lua_newtable(l);
            LuaDLL.lua_pushstdcallcfunction(l, methodWrapper);
            LuaDLL.lua_setfield(l, -2, "__call");
            LuaDLL.lua_setfield(l, LuaIndexes.LUA_REGISTRYINDEX, ObjectCache.getAQName(typeof(LuaCSFunction)));
        }
    }
}
