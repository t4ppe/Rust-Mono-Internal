using System;
using System.Reflection;

namespace Hacks
{
	// Token: 0x02000069 RID: 105
	public static class Helpers
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x0002059C File Offset: 0x0001E79C
		private static FieldInfo GetFieldInfo(Type type, string fieldName)
		{
			FieldInfo field;
			do
			{
				field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				type = type.BaseType;
			}
			while (field == null && type != null);
			return field;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000205DC File Offset: 0x0001E7DC
		public static object GetFieldValue(this object obj, string fieldName)
		{
			bool flag = obj == null;
			if (flag)
			{
				throw new ArgumentNullException("obj");
			}
			Type type = obj.GetType();
			FieldInfo fieldInfo = Helpers.GetFieldInfo(type, fieldName);
			bool flag2 = fieldInfo == null;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("fieldName", "Couldn't find field " + fieldName + " in type " + type.FullName);
			}
			return fieldInfo.GetValue(obj);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00020648 File Offset: 0x0001E848
		public static void SetFieldValue(this object obj, string fieldName, object val)
		{
			bool flag = obj == null;
			if (flag)
			{
				throw new ArgumentNullException("obj");
			}
			Type type = obj.GetType();
			FieldInfo fieldInfo = Helpers.GetFieldInfo(type, fieldName);
			bool flag2 = fieldInfo == null;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("fieldName", "Couldn't find field " + fieldName + " in type " + type.FullName);
			}
			fieldInfo.SetValue(obj, val);
		}
	}
}
