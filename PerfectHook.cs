using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000009 RID: 9
internal class PerfectHook
{
    // Token: 0x17000004 RID: 4
    // (get) Token: 0x0600001B RID: 27 RVA: 0x00002128 File Offset: 0x00000328
    // (set) Token: 0x0600001C RID: 28 RVA: 0x00002130 File Offset: 0x00000330
    public MethodInfo OriginalMethod { get; private set; }

    // Token: 0x17000005 RID: 5
    // (get) Token: 0x0600001D RID: 29 RVA: 0x00002139 File Offset: 0x00000339
    // (set) Token: 0x0600001E RID: 30 RVA: 0x00002141 File Offset: 0x00000341
    public MethodInfo HookMethod { get; private set; }

    // Token: 0x17000006 RID: 6
    // (get) Token: 0x0600001F RID: 31 RVA: 0x0000214A File Offset: 0x0000034A
    // (set) Token: 0x06000020 RID: 32 RVA: 0x00002152 File Offset: 0x00000352
    public MethodInfo OriginalNew { get; private set; }

    // Token: 0x06000021 RID: 33 RVA: 0x00004F6C File Offset: 0x0000316C
    public PerfectHook()
    {
        original = null;
        OriginalMethod = (HookMethod = null);
    }

    // Token: 0x06000022 RID: 34 RVA: 0x0000215B File Offset: 0x0000035B
    public PerfectHook(MethodInfo orig, MethodInfo hook, MethodInfo orignew)
    {
        original = null;
        Init(orig, hook, orignew);
    }

    // Token: 0x06000023 RID: 35 RVA: 0x00004F9C File Offset: 0x0000319C
    public PerfectHook(Type typeOrig, string nameOrig, Type typeHook, string nameHook, Type typeOrigNew, string nameOrigNew, byte[] orig)
    {
        original = new byte[orig.Length];
        original = orig;
        bool flag = nameOrig == "StartAttackCooldown";
        if (flag)
        {
            Init(typeOrig.GetMethod(nameOrig, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
            {
                typeof(float)
            }, null), typeHook.GetMethod(nameHook, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
            {
                typeof(float)
            }, null), typeOrigNew.GetMethod(nameOrigNew, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
            {
                typeof(float)
            }, null));
        }

        bool flag2 = nameOrig == "CreateProjectile" && object.Equals(typeOrig, typeof(BaseProjectile));
        if (flag2)
        {
            Init(typeOrig.GetMethod(nameOrig, BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
            {
                typeof(string),
                typeof(Vector3),
                typeof(Vector3),
                typeof(Vector3)
            }, null), typeHook.GetMethod(nameHook, BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
            {
                typeof(string),
                typeof(Vector3),
                typeof(Vector3),
                typeof(Vector3)
            }, null), typeOrigNew.GetMethod(nameOrigNew, BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
            {
                typeof(string),
                typeof(Vector3),
                typeof(Vector3),
                typeof(Vector3)
            }, null));
        }

        bool flag3 = nameOrig == "CreateProjectile" && object.Equals(typeOrig, typeof(BaseMelee));
        if (flag3)
        {
            Init(typeOrig.GetMethod(nameOrig, BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
            {
                typeof(string),
                typeof(Vector3),
                typeof(Vector3),
                typeof(Vector3)
            }, null), typeHook.GetMethod(nameHook, BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
            {
                typeof(string),
                typeof(Vector3),
                typeof(Vector3),
                typeof(Vector3)
            }, null), typeOrigNew.GetMethod(nameOrigNew, BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
            {
                typeof(string),
                typeof(Vector3),
                typeof(Vector3),
                typeof(Vector3)
            }, null));
        }

        bool flag4 = nameOrig == "CanJump";
        if (flag4)
        {
            Init(typeOrig.GetMethod(nameOrig, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null), typeHook.GetMethod(nameHook, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null), typeOrigNew.GetMethod(nameOrigNew, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null));
        }

        bool flag5 = nameOrig == "CanAttack";
        if (flag5)
        {
            Init(typeOrig.GetMethod(nameOrig), typeHook.GetMethod(nameHook), typeOrigNew.GetMethod(nameOrigNew));
        }
    }

    // Token: 0x06000024 RID: 36 RVA: 0x000052C4 File Offset: 0x000034C4
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
        OriginalMethod = orig;
        HookMethod = hook;
        OriginalNew = orignew;
    }

    // Token: 0x06000025 RID: 37 RVA: 0x0000533C File Offset: 0x0000353C
    public unsafe void Hook()
    {
        bool flag = object.Equals(OriginalMethod, null) || object.Equals(HookMethod, null);
        if (flag)
        {
            throw new ArgumentException("Hook has to be properly Init'd before use");
        }

        IntPtr functionPointer = OriginalMethod.MethodHandle.GetFunctionPointer();
        IntPtr functionPointer2 = HookMethod.MethodHandle.GetFunctionPointer();
        IntPtr functionPointer3 = OriginalNew.MethodHandle.GetFunctionPointer();
        uint newProtect;
        PerfectHook.Import.VirtualProtect(functionPointer3, (uint)(original.Length + 12), 64u, out newProtect);
        byte* ptr = (byte*)((void*)functionPointer3);
        for (int i = 0; i < original.Length; i++)
            ptr[i] = original[i];


        ptr[original.Length] = 72;
        (ptr + original.Length)[1] = 184;
        *(IntPtr*)(ptr + original.Length + 2) = (IntPtr)(functionPointer.ToInt64() + (long)original.Length);
        (ptr + original.Length)[10] = byte.MaxValue;
        (ptr + original.Length)[11] = 224;
        PerfectHook.Import.VirtualProtect(functionPointer3, (uint)(original.Length + 12), newProtect, out newProtect);
        PerfectHook.Import.VirtualProtect(functionPointer, 12u, 64u, out newProtect);
        byte* ptr2 = (byte*)((void*)functionPointer);
        *ptr2 = 72;
        ptr2[1] = 184;
        *(IntPtr*)(ptr2 + 2) = functionPointer2;
        ptr2[10] = byte.MaxValue;
        ptr2[11] = 224;
        PerfectHook.Import.VirtualProtect(functionPointer, 12u, newProtect, out newProtect);
    }

    // Token: 0x06000026 RID: 38 RVA: 0x000054D4 File Offset: 0x000036D4
    public unsafe void Unhook()
    {
        IntPtr functionPointer = OriginalMethod.MethodHandle.GetFunctionPointer();
        HookMethod.MethodHandle.GetFunctionPointer();
        OriginalNew.MethodHandle.GetFunctionPointer();
        uint newProtect;
        PerfectHook.Import.VirtualProtect(functionPointer, 12u, 64u, out newProtect);
        byte* ptr = (byte*)((void*)functionPointer);
        for (int i = 0; i < original.Length; i++)
            ptr[i] = original[i];


        PerfectHook.Import.VirtualProtect(functionPointer, 12u, newProtect, out newProtect);
    }

    // Token: 0x0400002B RID: 43
    byte[] original;

    // Token: 0x0200000A RID: 10
    internal class Import
    {
        // Token: 0x06000027 RID: 39
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool VirtualProtect(IntPtr address, uint size, uint newProtect, out uint oldProtect);

        // Token: 0x06000028 RID: 40
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr VirtualAlloc(uint lpAddress, uint dwSize, PerfectHook.Import.AllocationType lAllocationType, PerfectHook.Import.MemoryProtection flProtect);

        // Token: 0x0200000B RID: 11
        [Flags]
        public enum AllocationType
        {
            // Token: 0x0400002D RID: 45
            Commit = 4096,
            // Token: 0x0400002E RID: 46
            Reserve = 8192,
            // Token: 0x0400002F RID: 47
            Decommit = 16384,
            // Token: 0x04000030 RID: 48
            Release = 32768,
            // Token: 0x04000031 RID: 49
            Reset = 524288,
            // Token: 0x04000032 RID: 50
            Physical = 4194304,
            // Token: 0x04000033 RID: 51
            TopDown = 1048576,
            // Token: 0x04000034 RID: 52
            WriteWatch = 2097152,
            // Token: 0x04000035 RID: 53
            LargePages = 536870912
        }

        // Token: 0x0200000C RID: 12
        [Flags]
        public enum MemoryProtection : uint
        {
            // Token: 0x04000037 RID: 55
            EXECUTE = 16u,
            // Token: 0x04000038 RID: 56
            EXECUTE_READ = 32u,
            // Token: 0x04000039 RID: 57
            EXECUTE_READWRITE = 64u,
            // Token: 0x0400003A RID: 58
            EXECUTE_WRITECOPY = 128u,
            // Token: 0x0400003B RID: 59
            NOACCESS = 1u,
            // Token: 0x0400003C RID: 60
            READONLY = 2u,
            // Token: 0x0400003D RID: 61
            READWRITE = 4u,
            // Token: 0x0400003E RID: 62
            WRITECOPY = 8u,
            // Token: 0x0400003F RID: 63
            GUARD_Modifierflag = 256u,
            // Token: 0x04000040 RID: 64
            NOCACHE_Modifierflag = 512u,
            // Token: 0x04000041 RID: 65
            WRITECOMBINE_Modifierflag = 1024u
        }
    }
}