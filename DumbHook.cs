using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ProtoBuf;

// Token: 0x02000003 RID: 3
internal class DumbHook
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000008 RID: 8 RVA: 0x000020A2 File Offset: 0x000002A2
	// (set) Token: 0x06000009 RID: 9 RVA: 0x000020AA File Offset: 0x000002AA
	public MethodInfo OriginalMethod { get; private set; }

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600000A RID: 10 RVA: 0x000020B3 File Offset: 0x000002B3
	// (set) Token: 0x0600000B RID: 11 RVA: 0x000020BB File Offset: 0x000002BB
	public MethodInfo HookMethod { get; private set; }

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600000C RID: 12 RVA: 0x000020C4 File Offset: 0x000002C4
	// (set) Token: 0x0600000D RID: 13 RVA: 0x000020CC File Offset: 0x000002CC
	public MethodInfo OriginalNew { get; private set; }

	// Token: 0x0600000E RID: 14 RVA: 0x00004A88 File Offset: 0x00002C88
	public DumbHook()
	{
		this.original = null;
		MethodInfo methodInfo = this.OriginalMethod = (this.HookMethod = null);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x000020D5 File Offset: 0x000002D5
	public DumbHook(MethodInfo orig, MethodInfo hook, MethodInfo orignew)
	{
		this.original = null;
		this.Init(orig, hook, orignew);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00004ABC File Offset: 0x00002CBC
	public DumbHook(Type typeOrig, string nameOrig, Type typeHook, string nameHook, Type typeOrigNew, string nameOrigNew, byte[] orig)
	{
		this.original = new byte[orig.Length];
		this.original = orig;
		bool flag = nameOrig == "SetTimedAction";
		if (flag)
		{
			this.Init(typeOrig.GetMethod(nameOrig), typeHook.GetMethod(nameHook), typeOrigNew.GetMethod(nameOrigNew));
		}
		bool flag2 = nameOrig == "SendProjectileAttack";
		if (flag2)
		{
			this.Init(typeOrig.GetMethod(nameOrig, BindingFlags.Instance | BindingFlags.Public, null, new Type[]
			{
				typeof(PlayerProjectileAttack)
			}, null), typeHook.GetMethod(nameHook, BindingFlags.Instance | BindingFlags.Public, null, new Type[]
			{
				typeof(PlayerProjectileAttack)
			}, null), typeOrigNew.GetMethod(nameOrigNew, BindingFlags.Instance | BindingFlags.Public, null, new Type[]
			{
				typeof(PlayerProjectileAttack)
			}, null));
		}
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00004B90 File Offset: 0x00002D90
	public void Init(MethodInfo orig, MethodInfo hook, MethodInfo orignew)
	{
		bool flag = object.Equals(orig, null);
		if (flag)
		{
			throw new ArgumentException("Original method is null");
		}
		bool flag2 = object.Equals(hook, null);
		if (flag2)
		{
			throw new ArgumentException("Hook method is null");
		}
		RuntimeHelpers.PrepareMethod(orig.MethodHandle);
		RuntimeHelpers.PrepareMethod(hook.MethodHandle);
		RuntimeHelpers.PrepareMethod(orignew.MethodHandle);
		this.OriginalMethod = orig;
		this.HookMethod = hook;
		this.OriginalNew = orignew;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00004C08 File Offset: 0x00002E08
	public unsafe void Hook()
	{
		bool flag = object.Equals(this.OriginalMethod, null) || object.Equals(this.HookMethod, null);
		if (flag)
		{
			throw new ArgumentException("Hook has to be properly Init'd before use");
		}
		IntPtr functionPointer = this.OriginalMethod.MethodHandle.GetFunctionPointer();
		IntPtr functionPointer2 = this.HookMethod.MethodHandle.GetFunctionPointer();
		IntPtr functionPointer3 = this.OriginalNew.MethodHandle.GetFunctionPointer();
		uint newProtect;
		DumbHook.Import.VirtualProtect(functionPointer3, (uint)(this.original.Length + 12), 64u, out newProtect);
		byte* ptr = (byte*)((void*)functionPointer3);
		for (int i = 0; i < this.original.Length; i++)
		{
			ptr[i] = this.original[i];
		}
		ptr[this.original.Length] = 72;
		(ptr + this.original.Length)[1] = 184;
		*(IntPtr*)(ptr + this.original.Length + 2) = (IntPtr)(functionPointer.ToInt64() + (long)this.original.Length);
		(ptr + this.original.Length)[10] = byte.MaxValue;
		(ptr + this.original.Length)[11] = 224;
		DumbHook.Import.VirtualProtect(functionPointer3, (uint)(this.original.Length + 12), newProtect, out newProtect);
		DumbHook.Import.VirtualProtect(functionPointer, 12u, 64u, out newProtect);
		byte* ptr2 = (byte*)((void*)functionPointer);
		*ptr2 = 72;
		ptr2[1] = 184;
		*(IntPtr*)(ptr2 + 2) = functionPointer2;
		ptr2[10] = byte.MaxValue;
		ptr2[11] = 224;
		DumbHook.Import.VirtualProtect(functionPointer, 12u, newProtect, out newProtect);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00004DA0 File Offset: 0x00002FA0
	public unsafe void Unhook()
	{
		IntPtr functionPointer = this.OriginalMethod.MethodHandle.GetFunctionPointer();
		IntPtr functionPointer2 = this.HookMethod.MethodHandle.GetFunctionPointer();
		IntPtr functionPointer3 = this.OriginalNew.MethodHandle.GetFunctionPointer();
		uint newProtect;
		DumbHook.Import.VirtualProtect(functionPointer, 12u, 64u, out newProtect);
		byte* ptr = (byte*)((void*)functionPointer);
		for (int i = 0; i < this.original.Length; i++)
		{
			ptr[i] = this.original[i];
		}
		DumbHook.Import.VirtualProtect(functionPointer, 12u, newProtect, out newProtect);
	}

	// Token: 0x04000006 RID: 6
	private byte[] original;

	// Token: 0x02000004 RID: 4
	internal class Import
	{
		// Token: 0x06000014 RID: 20
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool VirtualProtect(IntPtr address, uint size, uint newProtect, out uint oldProtect);

		// Token: 0x06000015 RID: 21
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr VirtualAlloc(uint lpAddress, uint dwSize, DumbHook.Import.AllocationType lAllocationType, DumbHook.Import.MemoryProtection flProtect);

		// Token: 0x02000005 RID: 5
		[Flags]
		public enum AllocationType
		{
			// Token: 0x0400000B RID: 11
			Commit = 4096,
			// Token: 0x0400000C RID: 12
			Reserve = 8192,
			// Token: 0x0400000D RID: 13
			Decommit = 16384,
			// Token: 0x0400000E RID: 14
			Release = 32768,
			// Token: 0x0400000F RID: 15
			Reset = 524288,
			// Token: 0x04000010 RID: 16
			Physical = 4194304,
			// Token: 0x04000011 RID: 17
			TopDown = 1048576,
			// Token: 0x04000012 RID: 18
			WriteWatch = 2097152,
			// Token: 0x04000013 RID: 19
			LargePages = 536870912
		}

		// Token: 0x02000006 RID: 6
		[Flags]
		public enum MemoryProtection : uint
		{
			// Token: 0x04000015 RID: 21
			EXECUTE = 16u,
			// Token: 0x04000016 RID: 22
			EXECUTE_READ = 32u,
			// Token: 0x04000017 RID: 23
			EXECUTE_READWRITE = 64u,
			// Token: 0x04000018 RID: 24
			EXECUTE_WRITECOPY = 128u,
			// Token: 0x04000019 RID: 25
			NOACCESS = 1u,
			// Token: 0x0400001A RID: 26
			READONLY = 2u,
			// Token: 0x0400001B RID: 27
			READWRITE = 4u,
			// Token: 0x0400001C RID: 28
			WRITECOPY = 8u,
			// Token: 0x0400001D RID: 29
			GUARD_Modifierflag = 256u,
			// Token: 0x0400001E RID: 30
			NOCACHE_Modifierflag = 512u,
			// Token: 0x0400001F RID: 31
			WRITECOMBINE_Modifierflag = 1024u
		}
	}
}
