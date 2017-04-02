using System;
using System.Collections;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace NitroSocket.Snmp
{
	/// <summary>
	/// Snmp API Functions
	/// </summary>
	public class SnmpAPI
	{

        
        // Callback
        public delegate SNMPAPI_STATUS SnmpCallback(IntPtr session, IntPtr hwnd, int msg, ulong wparam, ulong lparam, IntPtr data);
        private string LocalAddr;	// this is the address of the local machine
        private string RemoteAddr;	// this is the address of the remote machine


        private int major;			// this is major version of SNMP
        private int minor;			// this is minor version of SNMP
        private int level;			// this is the highest level of SNMP
        private int translate;		// this is the translation type
        private int retran;			// this is the retransmission flag
        private IntPtr Session;		// this is a pointer to the session (this is the remote connection item)
        
        private string ErrMess;				// this is the private Error Message

        private bool VBLInit = false;		// this is a flag indicating if the Variable Bindings List has been initialized
               

        private VARIABLE_BINDING_LIST Vbl = new VARIABLE_BINDING_LIST();	// this is the private Variable Bindings List object

        public static string oidDelimiter = ":-:";


        // this is a getter for Error Message
        public string ErrorMessage
        {
            get
            {
                return ErrMess;
            }
        }

        /// <summary>
        /// FUNCTION: CreateVariableList
        /// PURPOSE : To create a new vbl
        /// </summary>
        public static bool CreateVariableList(string ParentEntity, int RequestID)
        {
            bool Success = false; // init success to failure
            try
            {
                VARIABLE_BINDING_LIST Vbl = new VARIABLE_BINDING_LIST();
                Vbl.ParentEntity = ParentEntity;
                Vbl.RequestID = RequestID;
                Vbl.Item = new ArrayList();
                Success = true;
            }
            catch (Exception Err)
            {
               String ErrMess = Err.Message;
            }
            return Success; // return success flag
        }


        public static ArrayList VblToArrayList(IntPtr vbl)
        {
            int count = 0;
            ArrayList al = new ArrayList();
            SMIOID name = new SMIOID();
            SMIVALUE val = new SMIVALUE();

            SNMPAPI_STATUS rc;

            //
            //  Get the variable count and set the capacity of the arraylist.  This
            //  speeds our processing as there's no need to expand the list as
            //  we add entries.
            //
            rc = SnmpAPI.SnmpCountVbl(vbl);
            if (rc != SNMPAPI_STATUS.SNMPAPI_FAILURE)
                count = (int)rc;
            al.Capacity = count;

            for (int i = 1; i <= count ; i++)
            {
                rc = SnmpAPI.SnmpGetVb(vbl, i, ref name, ref val);
                if (rc != SNMPAPI_STATUS.SNMPAPI_SUCCESS)
                    break;

                //
                //  Build the variable name
                //
                string soid = OidToString(ref name);
                string v = VbToString(ref val);
                if (soid != null)
                    al.Add(soid + oidDelimiter + v + oidDelimiter);
            }
            return (al);
        }

        public static Hashtable VblToHashTable(IntPtr vbl)
        {
            int count = 0;
            ArrayList al = new ArrayList();
            SMIOID name = new SMIOID();
            SMIVALUE val = new SMIVALUE();
            Hashtable ht = new Hashtable();

            SNMPAPI_STATUS rc;

            //
            //  Get the variable count and set the capacity of the arraylist.  This
            //  speeds our processing as there's no need to expand the list as
            //  we add entries.
            //
            rc = SnmpAPI.SnmpCountVbl(vbl);
            if (rc != SNMPAPI_STATUS.SNMPAPI_FAILURE)
                count = (int)rc;
            al.Capacity = count;

            for (int i = 1; i <= count; i++)
            {
                rc = SnmpAPI.SnmpGetVb(vbl, i, ref name, ref val);
                if (rc != SNMPAPI_STATUS.SNMPAPI_SUCCESS)
                    break;

                //
                //  Build the variable name
                //
                string soid = OidToString(ref name);
                string v = VbToString(ref val);
                if (soid != null)
                {
                    if (ht.ContainsKey(soid))
                    {
                        ht[soid] = ht[soid] + " " + v;
                    }
                    else
                    {
                        ht.Add(soid, v);
                    }
                }
                    
            }
            return (ht);
        }

        public static string OidToString(ref SMIOID oid)
        {
            string soid = null;
            IntPtr OTSBuffer = Marshal.AllocHGlobal(1408);
            

            try
            {
                SNMPAPI_STATUS rc = SnmpAPI.SnmpOidToStr(ref oid, 1408, OTSBuffer);
                if (rc != SNMPAPI_STATUS.SNMPAPI_FAILURE)
                    soid = Marshal.PtrToStringAnsi(OTSBuffer);
                SnmpAPI.SnmpFreeDescriptor((int)SNMPAPI_SYNTAX.SNMP_SYNTAX_OID, ref oid);
            }
            catch (Exception e)
            {
                Console.WriteLine("OidToString Exception:\n" + e.Message + "\n" + e.StackTrace);
            }
            //Free
            Marshal.FreeHGlobal(OTSBuffer);
            return (soid);
        }

        public static string VbToString(ref SMIVALUE sval)
        {
            string val = "";

            //
            //  Convert all types to a string value
            //
            switch (sval.type)
            {
                //
                //  Standard 32 bit integer conversion
                //
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_INT:
                    val = sval.val.sNumber.ToString();
                    break;

                //
                //  Standard 32 bit unsigned integer conversion
                //
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_UINT32:
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_CNTR32:
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_GAUGE32:
                    val = sval.val.uNumber.ToString();
                    break;

                //
                //  Timeticks are in hundredths of a second (.01).  We need to multiply
                //  by ten to send ms (.001) to TimeSpan.
                //
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_TIMETICKS:
                    val = TimeSpan.FromMilliseconds((long)sval.val.uNumber * 10).ToString();
                    val = val.Substring(0, val.Length - 4);
                    break;

                //
                //  Standard 64 bit integer conversion
                //
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_CNTR64:
                    val = sval.val.hNumber.ToString();
                    break;

                //
                //  Byte array conversion.  SNMP doesn't differentiate between
                //  ASCII and binary data, so we perform a check ourselves and
                //  convert binary data to ASCII (using ToBase64String, which is uuencode).
                //
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_BITS:
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_OPAQUE:
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_OCTETS:
                    if (sval.val.str.size > 0)
                    {
                        int i = 0;
                        byte[] bits = new byte[sval.val.str.size];

                        Marshal.Copy(sval.val.str.octets, bits, 0, (int)sval.val.str.size);

                        for (i = 0; i < bits.Length; i++)
                        {
                            if (char.IsControl((char)bits[i]) && (i < bits.Length - 1 || bits[i] != 0))
                            {
                                val = Convert.ToBase64String(bits, 0, bits.Length);
                                break;
                            }
                        }
                        if (i == bits.Length)
                            val = Marshal.PtrToStringAnsi(sval.val.str.octets, (int)sval.val.str.size);

                        SnmpAPI.SnmpFreeDescriptor((int)sval.type, ref sval.val.str);
                    }
                    break;

                //
                //  Ip Address conversion
                //
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_NSAPADDR:
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_IPADDR:
                #if(WIN32)
                    IPAddress addr = new IPAddress((long)(UInt32)Marshal.ReadIntPtr(sval.val.str.octets));
                #else
                    IPAddress addr = null;
                    try
                    {

                        byte[] bytes = new byte[sval.val.str.size];

                        Marshal.Copy(sval.val.str.octets, bytes, 0, (int)sval.val.str.size);

                        addr = new IPAddress(bytes);
                        val = addr.ToString();

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Problem To Convert IPADDRESS From SNMPApi " + ex.ToString());
                    }
                #endif
                    break;   
                //
                //  SNMP OID conversion
                //
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_OID:
                    val = OidToString(ref sval.val.oid);
                    break;

                //
                //  Catch the rest
                //
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_NULL:
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_NOSUCHOBJECT:
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_NOSUCHINSTANCE:
                case SNMPAPI_SYNTAX.SNMP_SYNTAX_ENDOFMIBVIEW:
                    val = "(null)";
                    break;
            }
            return (val);
        }


        /// <summary>
        /// FUNCTION: AddVariableListItem
        /// PURPOSE : To add to a vbl
        /// </summary>
        public bool AddVariableListItem(VARIABLE_ITEM VariableItem)
        {
            bool Success = false; // init success to failure
            try
            {
                Vbl.Item.Add(VariableItem);
                Success = true;
            }
            catch (Exception Err)
            {
                ErrMess = Err.Message;
            }
            return Success; // return success flag
        }

        /// <summary>
        /// FUNCTION: Close
        /// PURPOSE : To close a session
        /// </summary>
        public bool Close()
        {
            bool Success = false; // init success to failure
            try
            {
                SNMPAPI_STATUS Result = new SNMPAPI_STATUS(); // set up a result item

                // close the session
                Result = SnmpAPI.SnmpClose(Session);
                if (Result != SNMPAPI_STATUS.SNMPAPI_SUCCESS)
                    throw new Exception("SNMP Close Error");

                // call clean up for garbage collection
                Result = SnmpAPI.SnmpCleanup();
                if (Result != SNMPAPI_STATUS.SNMPAPI_SUCCESS)
                    throw new Exception("SNMP Ternination Error");

                Success = true;
            }
            catch (Exception Err)
            {
                ErrMess = Err.Message;
            }
            return Success; // return success flag
        }


        public bool Listen(string RemoteMachine)
        {
            bool Success = false; // init success to failure
            try
            {
                SNMPAPI_STATUS Result = new SNMPAPI_STATUS(); // set up a result item

                // this is our call-back function
                SnmpAPI.SnmpCallback SnmpCB1 = new SnmpAPI.SnmpCallback(OnSnmpMessage);

                // get the local and remote ip addresses
                this.LocalAddr = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
                this.RemoteAddr = Dns.GetHostEntry(RemoteMachine).AddressList[0].ToString();

                // initialize the snmp api system
                Result = SnmpAPI.SnmpStartup(out major, out minor, out level, out translate, out retran);
                if (Result != SNMPAPI_STATUS.SNMPAPI_SUCCESS)
                    throw new Exception("SNMP Initialization Error");

                // create the session
                Session = SnmpAPI.SnmpCreateSession(IntPtr.Zero, 0, SnmpCB1, IntPtr.Zero);
                if (Result == SNMPAPI_STATUS.SNMPAPI_FAILURE)
                    throw new Exception("SNMP Create Session Error");

                Result = SnmpRegister(Session, System.IntPtr.Zero, System.IntPtr.Zero, System.IntPtr.Zero, System.IntPtr.Zero, (int)SNMPAPI_STATE.SNMPAPI_ON);
                if (Result != SNMPAPI_STATUS.SNMPAPI_SUCCESS)
                    throw new Exception("SNMP Initialization Error");


                IntPtr VbL1 = new IntPtr();									// pointer to variable bindings list
                SMIOID[] OID = new SMIOID[1];			// an array of oids
                SMIVALUE[] Val = new SMIVALUE[1];		// an array of values
						
				// create entities for the source and destination addresses
    			IntPtr Source = SnmpAPI.SnmpStrToEntity(Session,LocalAddr);
	    		IntPtr Destin = SnmpAPI.SnmpStrToEntity(Session,RemoteAddr);

						
                VbL1 = SnmpAPI.SnmpCreateVbl(Session,ref OID[0],ref Val[0]);

                VARIABLE_ITEM VariableListItem = new VARIABLE_ITEM();
                VariableListItem.Entity = ".1";
                VariableListItem.Type = VARIABLE_TYPE.VARIABLE_TYPE_OCTECT;
                VariableListItem.Value = "GetAlarmDescription(AlarmCode)";
                AddVariableListItem(VariableListItem);
                
                IntPtr Pdu = SnmpAPI.SnmpCreatePdu(Session, (int)SNMPAPI_PDU.SNMP_PDU_GETBULK, Int16.MinValue, Int16.MinValue, Int16.MinValue, VbL1);

                GCHandle[] PinnedArray = new GCHandle[1];		// an array of pinned memory

                SMIOCTETS DestOct = new SMIOCTETS();				// the destination octect

                // set the destination octet (we only need to set the first one, the rest will flow accordingly)
                DestOct.size = (uint)(VariableListItem.Value.Length);
                //DestOct.octets = PinnedArray[0].AddrOfPinnedObject();
                IntPtr Context = SnmpAPI.SnmpStrToContext(Session, ref DestOct);
                
                // set the port to 162
                Result = SnmpAPI.SnmpSetPort(Destin, 162);

                while (!Success)
                {
                    Result = SnmpAPI.SnmpRecvMsg(Session, out Source, out Destin, out Context, out Pdu);
                    if (Result == SNMPAPI_STATUS.SNMPAPI_SUCCESS)

                    //throw new Exception("SNMP Initialization Error");
                    {
                        int teste1;
                        int teste0;
                        int teste3; int teste4;
                        IntPtr vlb;
                        Result = SnmpAPI.SnmpGetPduData(Pdu, out teste0, out teste1, out teste3, out teste4, out vlb);
                        //VARIABLE_BINDING_LIST teste11 = teste;

                        
                        Success = true;
                        break;
                    }
                    Thread.Sleep(100);
                }
                

            }
            catch (Exception Err)
            {
                ErrMess = Err.Message;
            }
            return Success;
            
        }
       
        public bool Open(string RemoteMachine)
        {
            bool Success = false; // init success to failure
            try
            {
                SNMPAPI_STATUS Result = new SNMPAPI_STATUS(); // set up a result item

                // this is our call-back function
                SnmpAPI.SnmpCallback SnmpCB = new SnmpAPI.SnmpCallback(OnSnmpMessage);

                // get the local and remote ip addresses
                this.LocalAddr = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
                this.RemoteAddr = Dns.GetHostEntry(RemoteMachine).AddressList[0].ToString();

                // initialize the snmp api system
                Result = SnmpAPI.SnmpStartup(out major, out minor, out level, out translate, out retran);
                if (Result != SNMPAPI_STATUS.SNMPAPI_SUCCESS)
                    throw new Exception("SNMP Initialization Error");

                // create the session
                Session = SnmpAPI.SnmpCreateSession(IntPtr.Zero, 0, SnmpCB, IntPtr.Zero);
                if (Result == SNMPAPI_STATUS.SNMPAPI_FAILURE)
                    throw new Exception("SNMP Create Session Error");

                Success = true;

            }
            catch (Exception Err)
            {
                ErrMess = Err.Message;
            }
            return Success; // return success flag
        }

        /// <summary>
        /// FUNCTION: Send
        /// PURPOSE : To send a pdu in order to set the MIB values
        /// </summary>
        public bool Send()
        {
            bool Success = false; // init success to failure
            try
            {
                if (VBLInit)
                {

                    if (Vbl.Item.Count > 0)
                    {
                        SNMPAPI_STATUS Result = new SNMPAPI_STATUS(); // set up a result item

                        // create entities for the source and destination addresses
                        IntPtr Source = SnmpAPI.SnmpStrToEntity(Session, LocalAddr);
                        IntPtr Destin = SnmpAPI.SnmpStrToEntity(Session, RemoteAddr);

                        // create arrays to hold Variable Info
                        SMIOID[] OID = new SMIOID[Vbl.Item.Count];			// an array of oids
                        SMIVALUE[] Val = new SMIVALUE[Vbl.Item.Count];		// an array of values
                        SMIOCTETS[] Oct = new SMIOCTETS[Vbl.Item.Count];	// an array of octets
                        SMIOCTETS DestOct = new SMIOCTETS();				// the destination octect
                        GCHandle[] PinnedArray = new GCHandle[Vbl.Item.Count];		// an array of pinned memory
                        byte[][] ba = new byte[Vbl.Item.Count][];					// a multi-dimensional byte array
                        IntPtr VbL1 = new IntPtr();									// pointer to variable bindings list
                        IntPtr Context = new IntPtr();								// pointer to a context object

                        // start looping through all items in the variable bindings list
                        for (int i = 0; i < Vbl.Item.Count; i++)
                        {

                            ba[i] = new byte[((VARIABLE_ITEM)Vbl.Item[i]).Value.Length]; // new byte array

                            // check to see if the user has passed the separator
                            if (Vbl.ParentEntity.Substring(Vbl.ParentEntity.Length - 1, 1) != "."
                                && ((VARIABLE_ITEM)Vbl.Item[i]).Entity.Substring(0, 1) != ".")
                                Result = SnmpAPI.SnmpStrToOid(Vbl.ParentEntity + "." + ((VARIABLE_ITEM)Vbl.Item[i]).Entity, ref OID[i]);
                            else
                                Result = SnmpAPI.SnmpStrToOid(Vbl.ParentEntity + ((VARIABLE_ITEM)Vbl.Item[i]).Entity, ref OID[i]);

                            // check result
                            if (Result == SNMPAPI_STATUS.SNMPAPI_FAILURE)
                                throw new Exception("SNMP OID Creation Failure");

                            // create the octet and value for this variable
                            Oct[i].size = (uint)((VARIABLE_ITEM)Vbl.Item[i]).Value.Length;			// set the octet size
                            ba[i] = Encoding.ASCII.GetBytes(((VARIABLE_ITEM)Vbl.Item[i]).Value);	// encode string to byte array
                            PinnedArray[i] = GCHandle.Alloc(ba[i], GCHandleType.Pinned);				// this creates a static memory location
                            Oct[i].octets = PinnedArray[i].AddrOfPinnedObject();					// set the octet pointer

                            // now, check what type of variable this is and set the value accordingly
                            switch (((VARIABLE_ITEM)Vbl.Item[i]).Type)
                            {
                                case VARIABLE_TYPE.VARIABLE_TYPE_INT:
                                    Val[i].type = SNMPAPI_SYNTAX.SNMP_SYNTAX_INT;
                                    Val[i].val.sNumber = int.Parse(((VARIABLE_ITEM)Vbl.Item[i]).Value);
                                    break;
                                case VARIABLE_TYPE.VARIABLE_TYPE_INT32:
                                    Val[i].type = SNMPAPI_SYNTAX.SNMP_SYNTAX_INT32;
                                    Val[i].val.hNumber = long.Parse(((VARIABLE_ITEM)Vbl.Item[i]).Value);
                                    break;
                                case VARIABLE_TYPE.VARIABLE_TYPE_OCTECT:
                                    Val[i].type = SNMPAPI_SYNTAX.SNMP_SYNTAX_OCTETS;
                                    Val[i].val.str = Oct[i];
                                    break;
                            }

                            // check to see if this is the first item, or an item to append
                            if (i == 0)
                                VbL1 = SnmpAPI.SnmpCreateVbl(Session, ref OID[i], ref Val[i]);
                            else
                                Result = SnmpAPI.SnmpSetVb(VbL1, 0, ref OID[i], ref Val[i]);


                        }
                        // now, set up the protocol description unit
                        IntPtr Pdu = SnmpAPI.SnmpCreatePdu(Session, (int)SNMPAPI_PDU.SNMP_PDU_SET, Vbl.RequestID, 0, 0, VbL1);

                        // set the destination octet (we only need to set the first one, the rest will flow accordingly)
                        DestOct.size = (uint)((VARIABLE_ITEM)Vbl.Item[0]).Value.Length;
                        DestOct.octets = PinnedArray[0].AddrOfPinnedObject();
                        Context = SnmpAPI.SnmpStrToContext(Session, ref DestOct);

                        // set the port to 162
                        Result = SnmpAPI.SnmpSetPort(Destin, 162);

                        // now send the messages
                        Result = SnmpAPI.SnmpSendMsg(Session, Source, Destin, Context, Pdu);


                        // SNMPAPI use requires us to handle some garbage collecting ourselves, else suffer memory leakage.

                        // free the context
                        Result = SnmpAPI.SnmpFreeContext(Context);

                        // free the pdu
                        Result = SnmpAPI.SnmpFreePdu(Pdu);

                        // free the variable lists
                        SnmpAPI.SnmpFreeVbl(VbL1);

                        for (int t = 0; t < Vbl.Item.Count; t++)
                        {
                            // free the pinned arrays
                            PinnedArray[t].Free();

                            // free the oids
                            SnmpAPI.SnmpFreeDescriptor((int)SNMPAPI_SYNTAX.SNMP_SYNTAX_OID, ref OID[t]);
                        }

                        // finally, free the entities
                        SnmpAPI.SnmpFreeEntity(Source);
                        SnmpAPI.SnmpFreeEntity(Destin);

                        Success = true;
                    }
                    else
                    {
                        throw new Exception("Variable Binding List Empty.");
                    }
                }
                else
                {
                    throw new Exception("Variable Binding Does Not Exist.");
                }
            }
            catch (Exception Err)
            {
                ErrMess = Err.Message;
            }
            return Success; // return success flag
        }

        /// <summary>
        /// FUNCTION: OnSnmpMessage
        /// PURPOSE : Our callback function
        /// </summary>
        private SNMPAPI_STATUS OnSnmpMessage(IntPtr session, IntPtr hwnd, int msg, UInt64 wparam, UInt64 lparam, IntPtr data)
        {
            return new SNMPAPI_STATUS();

        }


		// Communication functions
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpStartup(out int major, out int minor, out int level, out int translate, out int retransmit);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpCleanup();
		[DllImport("wsnmp32.dll")]
		public static extern IntPtr SnmpCreateSession(IntPtr hwnd, int msg, SnmpCallback callback, IntPtr data);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpClose(IntPtr session);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpRegister(IntPtr session, IntPtr src, IntPtr dest, IntPtr context, IntPtr notification, int state);

        //Aml
        [DllImport("wsnmp32.dll")]
        //public static extern SNMPAPI_STATUS SnmpListen(IntPtr entity, IntPtr status);
        public static extern SNMPAPI_STATUS SnmpListen(IntPtr entity, Int32 status);
        //Aml
        
        
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpSendMsg(IntPtr session, IntPtr src, IntPtr dest, IntPtr context, IntPtr pdu);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpRecvMsg(IntPtr session, out IntPtr src, out IntPtr dest, out IntPtr context, out IntPtr pdu);

        //Aml
        [DllImport("wsnmp32.dll")]
        //public static extern SNMPAPI_STATUS SnmpCancelMsg(IntPtr session, IntPtr reqid);
        public static extern SNMPAPI_STATUS SnmpCancelMsg(IntPtr session, Int32 reqid);
        //Aml

		// Entity and Context functions
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpContextToStr(IntPtr context, ref SMIOCTETS octets);
		[DllImport("wsnmp32.dll")]
		public static extern IntPtr SnmpStrToContext(IntPtr session, ref SMIOCTETS octets);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpFreeEntity(IntPtr entity);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpEntityToStr(IntPtr entity, int size, IntPtr str);
		[DllImport("wsnmp32.dll")]
		public static extern IntPtr SnmpStrToEntity(IntPtr session, [MarshalAs(UnmanagedType.LPStr)] string str);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpFreeContext(IntPtr context);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpSetPort(IntPtr entity, int port);

		// Database functions
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpGetRetransmitMode(out uint mode);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpGetRetry(IntPtr entity, out uint policy, out uint actual);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpGetTimeout(IntPtr entity, out int policy, out int actual);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpGetTranslateMode(out uint mode);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpGetVendorInfo(ref VENDORINFO Info);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpSetRetransmitMode(SNMPAPI_RETRANSMIT mode);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpSetRetry(IntPtr entity, int policy);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpSetTimeout(IntPtr entity, int policy);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpSetTranslateMode(SNMPAPI_TRANSLATE mode);

		// PDU functions
		[DllImport("wsnmp32.dll")]
        public static extern IntPtr SnmpCreatePdu(IntPtr session, int type, int reqid, int status, int index, IntPtr vblist);
		[DllImport("wsnmp32.dll")]
		public static extern IntPtr SnmpDuplicatePdu(IntPtr session, IntPtr pdu);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpFreePdu(IntPtr pdu);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpGetPduData(IntPtr pdu, out int type, out int reqid, out int status, out int index, out IntPtr vblist);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpSetPduData(IntPtr pdu, ref int type, ref int reqid, ref int nonrepeaters, ref int maxreps, ref IntPtr vblist);

		// Utility functions
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpDecodeMsg(IntPtr session, out IntPtr src, out IntPtr dest, out IntPtr context, out IntPtr pdu, ref SMIOCTETS octets);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpEncodeMsg(IntPtr session, IntPtr src, IntPtr dest, IntPtr context, IntPtr pdu, ref SMIOCTETS octets);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpFreeDescriptor(int syntax, ref SMIOID desc);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpFreeDescriptor(int syntax, ref SMIOCTETS desc);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpGetLastError(IntPtr session);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpOidCompare(IntPtr oid1, IntPtr oid2, int max, ref int result);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpOidCopy(IntPtr src, IntPtr dest);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpOidToStr(ref SMIOID oid, int size, IntPtr str);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpStrToOid(string str, ref SMIOID oid);

		// Variable Binding functions
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpCountVbl(IntPtr vbl);
		[DllImport("wsnmp32.dll")]
		public static extern IntPtr SnmpCreateVbl(IntPtr session, ref SMIOID name, ref SMIVALUE value);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpDeleteVb(IntPtr vbl, int index);

        //Aml
        [DllImport("wsnmp32.dll")]
        //public static extern SNMPAPI_STATUS SnmpDuplicateVbl(IntPtr session, IntPtr vbl);
        public static extern IntPtr SnmpDuplicateVbl(IntPtr session, IntPtr vbl);
        //Aml
        
        [DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpFreeVbl(IntPtr vbl);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpGetVb(IntPtr vbl, int index, ref SMIOID name, ref SMIVALUE value);
		[DllImport("wsnmp32.dll")]
		public static extern SNMPAPI_STATUS SnmpSetVb(IntPtr vbl, int index, ref SMIOID name, ref SMIVALUE value);
	}
}

