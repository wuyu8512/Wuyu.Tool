using System;
using System.Reflection;

namespace Wuyu.Tool.Common
{
	public class ComObject
	{
		public readonly object obj;

		public readonly Type type;

		public ComObject(object o)
		{
			obj = o;
			type = o.GetType();
		}

		public ComObject(Type t, params object[] args)
		{
			type = t;
			obj = Activator.CreateInstance(type, args);
		}

		public ComObject(string ProgID, params object[] args)
		{
			type = Type.GetTypeFromProgID(ProgID);
			if (type == null)
			{
				throw new Exception("无法创建名为" + ProgID + "的对象");
			}
			obj = Activator.CreateInstance(type, args);
		}

		public ComObject(Guid guif, params object[] args)
		{
			type = Type.GetTypeFromCLSID(guif);
			if (type == null)
			{
				Guid guid = guif;
				throw new Exception("无法创建名为" + guid.ToString() + "的对象");
			}
			obj = Activator.CreateInstance(type, args);
		}

		public object SetProperty(string name, object[] args)
		{
			return type.InvokeMember(name, BindingFlags.SetProperty, null, obj, args);
		}

		public object InvokeMethod(string name, object[] args = null)
		{
			return type.InvokeMember(name, BindingFlags.InvokeMethod, null, obj, args);
		}

		public object GetProperty(string name, object[] args = null)
		{
			return type.InvokeMember(name, BindingFlags.GetProperty, null, obj, args);
		}
	}
}
