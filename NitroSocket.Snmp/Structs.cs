using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace NitroSocket.Snmp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VENDORINFO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string vendorName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string vendorContact;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string vendorVersionId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string vendorVersionDate;

        public int vendorEnterprise;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SMIOCTETS
    {
        public uint size;
        public IntPtr octets;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SMIOID
    {
        public uint size;
        public IntPtr dwords;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct SMIVUNION
    {
        [FieldOffset(0)]
        public int sNumber;
        [FieldOffset(0)]
        public UInt32 uNumber;
        [FieldOffset(0)]
        public Int64 hNumber;
        [FieldOffset(0)]
        public SMIOCTETS str;
        [FieldOffset(0)]
        public SMIOID oid;
        [FieldOffset(0)]
        public byte empty;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct SMIVALUE
    {
        [FieldOffset(0)]
        public SNMPAPI_SYNTAX type;
#if(WIN32)
        [FieldOffset(4)]
        public SMIVUNION val;
#else
        [FieldOffset(8)]
        public SMIVUNION val;
#endif
    }

    // this public struct defines a single variable item
    public struct VARIABLE_ITEM
    {
        public string Entity;
        public string Value;
        public VARIABLE_TYPE Type;
    }

    // this private struct defines a variable bindings list
    public struct VARIABLE_BINDING_LIST
    {
        public int RequestID;
        public string ParentEntity;
        public ArrayList Item;
    }

}

