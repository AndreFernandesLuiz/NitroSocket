using System;
using System.Runtime.InteropServices;

namespace NitroSocket.Snmp
{

    /// <summary>
    /// Snmp API enumerations
    /// </summary>
    public enum SNMPAPI_STATE : int
    {
        SNMPAPI_OFF = 0,
        SNMPAPI_ON = 1
    }

    /// <summary>
    /// Snmp API enumerations
    /// </summary>
    public enum SNMPAPI_RETRANSMIT : int
    {
        SNMPAPI_OFF = 0,
        SNMPAPI_ON = 1
    }

    public enum SNMPAPI_TRANSLATE : int
    {
        SNMPAPI_TRANSLATED = 0,
        SNMPAPI_UNTRANSLATED_V1 = 1,
        SNMPAPI_UNTRANSLATED_V2 = 2
    }

    public enum SNMPAPI_STATUS : uint
    {
        SNMPAPI_FAILURE = 0,		/* Generic error code */
        SNMPAPI_SUCCESS = 1,		/* Generic success code */

        /* WinSNMP API Error Codes (for SnmpGetLastError) */
        /* (NOT SNMP Response-PDU error_status codes) */
        SNMPAPI_ALLOC_ERROR = 2,		/* Error allocating memory */
        SNMPAPI_CONTEXT_INVALID = 3,		/* Invalid context parameter */
        SNMPAPI_CONTEXT_UNKNOWN = 4,		/* Unknown context parameter */
        SNMPAPI_ENTITY_INVALID = 5,		/* Invalid entity parameter */
        SNMPAPI_ENTITY_UNKNOWN = 6,		/* Unknown entity parameter */
        SNMPAPI_INDEX_INVALID = 7,		/* Invalid VBL index parameter */
        SNMPAPI_NOOP = 8,		/* No operation performed */
        SNMPAPI_OID_INVALID = 9,		/* Invalid OID parameter */
        SNMPAPI_OPERATION_INVALID = 10,	/* Invalid/unsupported operation */
        SNMPAPI_OUTPUT_TRUNCATED = 11,	/* Insufficient output buf len */
        SNMPAPI_PDU_INVALID = 12,	/* Invalid PDU parameter */
        SNMPAPI_SESSION_INVALID = 13,	/* Invalid session parameter */
        SNMPAPI_SYNTAX_INVALID = 14,	/* Invalid syntax in smiVALUE */
        SNMPAPI_VBL_INVALID = 15,	/* Invalid VBL parameter */
        SNMPAPI_MODE_INVALID = 16,	/* Invalid mode parameter */
        SNMPAPI_SIZE_INVALID = 17,	/* Invalid size/length parameter */
        SNMPAPI_NOT_INITIALIZED = 18,	/* SnmpStartup failed/not called */
        SNMPAPI_MESSAGE_INVALID = 19,	/* Invalid SNMP message format */
        SNMPAPI_HWND_INVALID = 20,	/* Invalid Window handle */
        SNMPAPI_OTHER_ERROR = 99,	/* For internal/undefined errors */

        /* Generic Transport Layer (TL) Errors */
        SNMPAPI_TL_NOT_INITIALIZED = 100,	/* TL not initialized */
        SNMPAPI_TL_NOT_SUPPORTED = 101,	/* TL does not support protocol */
        SNMPAPI_TL_NOT_AVAILABLE = 102,	/* Network subsystem has failed */
        SNMPAPI_TL_RESOURCE_ERROR = 103,	/* TL resource error */
        SNMPAPI_TL_UNDELIVERABLE = 104,	/* Destination unreachable */
        SNMPAPI_TL_SRC_INVALID = 105,	/* Source endpoint invalid */
        SNMPAPI_TL_INVALID_PARAM = 106,	/* Input parameter invalid */
        SNMPAPI_TL_IN_USE = 107,	/* Source endpoint in use */
        SNMPAPI_TL_TIMEOUT = 108,	/* No response before timeout */
        SNMPAPI_TL_PDU_TOO_BIG = 109,	/* PDU too big for send/receive */
        SNMPAPI_TL_OTHER = 199	/* Undefined TL error */
    }

    public enum SNMPAPI_PDU : int
    {
        SNMP_PDU_GET = 0xa0,
        SNMP_PDU_GETNEXT = 0xa1,
        SNMP_PDU_RESPONSE = 0xa2,
        SNMP_PDU_SET = 0xa3,
        SNMP_PDU_V1TRAP = 0xa4,
        SNMP_PDU_GETBULK = 0xa5,
        SNMP_PDU_INFORM = 0xa6,
        SNMP_PDU_TRAP = 0xa7
    }

    public enum SNMPAPI_SYNTAX : int
    {
        // SNMP ObjectSyntax Values
        SNMP_SYNTAX_SEQUENCE = 0x30,

        // These values are used in the "syntax" member of the smiVALUE structure which follows
        SNMP_SYNTAX_INT = 0x02,
        SNMP_SYNTAX_INT32 = SNMP_SYNTAX_INT,
        SNMP_SYNTAX_BITS = 0x03,
        SNMP_SYNTAX_OCTETS = 0x04,
        SNMP_SYNTAX_NULL = 0x05,
        SNMP_SYNTAX_OID = 0x06,

        SNMP_SYNTAX_IPADDR = 0x40,
        SNMP_SYNTAX_CNTR32 = 0x41,
        SNMP_SYNTAX_GAUGE32 = 0x42,
        SNMP_SYNTAX_TIMETICKS = 0x43,
        SNMP_SYNTAX_OPAQUE = 0x44,
        SNMP_SYNTAX_NSAPADDR = 0x45,
        SNMP_SYNTAX_CNTR64 = 0x46,
        SNMP_SYNTAX_UINT32 = 0x47,

        // Exception conditions in response PDUs for SNMPv2
        SNMP_SYNTAX_NOSUCHOBJECT = 0x80,
        SNMP_SYNTAX_NOSUCHINSTANCE = 0x81,
        SNMP_SYNTAX_ENDOFMIBVIEW = 0x82
    }

    public enum SNMPAPI_ERROR_STATUS : int
    {
        SNMP_ERROR_NOERROR = 0,
        SNMP_ERROR_TOOBIG = 1,
        SNMP_ERROR_NOSUCHNAME = 2,
        SNMP_ERROR_BADVALUE = 3,
        SNMP_ERROR_READONLY = 4,
        SNMP_ERROR_GENERR = 5,
        SNMP_ERROR_NOACCESS = 6,
        SNMP_ERROR_WRONGTYPE = 7,
        SNMP_ERROR_WRONGLENGTH = 8,
        SNMP_ERROR_WRONGENCODING = 9,
        SNMP_ERROR_WRONGVALUE = 10,
        SNMP_ERROR_NOCREATION = 11,
        SNMP_ERROR_INCONSISTENTVALUE = 12,
        SNMP_ERROR_RESOURCEUNAVAILABLE = 13,
        SNMP_ERROR_COMMITFAILED = 14,
        SNMP_ERROR_UNDOFAILED = 15,
        SNMP_ERROR_AUTHORIZATIONERROR = 16,
        SNMP_ERROR_NOTWRITABLE = 17,
        SNMP_ERROR_INCONSISTENTNAME = 18
    }

    // this public enum contains the currently available types
    public enum VARIABLE_TYPE
    {
        VARIABLE_TYPE_UNKNOWN,
        VARIABLE_TYPE_INT,
        VARIABLE_TYPE_INT32,
        VARIABLE_TYPE_OCTECT
    }



}
