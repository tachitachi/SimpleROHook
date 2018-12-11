﻿using System;

using System.Windows.Forms;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace SimpleROHookCS
{
    enum COPYDATAENTRY
    {
        COPYDATA_NPCLogger = 127
    };

    unsafe class SRHSharedData : IDisposable
    {
        private const int MAX_PATH = 260;

        [StructLayout(LayoutKind.Sequential)]
        unsafe struct StSHAREDMEMORY
        {
            public int g_hROWindow;

            public int executeorder;

            public int write_packetlog;
            public int freemouse;
            public int ground_zbias;
            public int alphalevel;
            public int m2e;
            public int bbe;
            public int deadcell;
            public int chatscope;
            public int castrange;
            public int fix_windowmode_vsyncwait;
            public int show_framerate;
            public int objectinformation;
            public int _44khz_audiomode;
            public int cpucoolerlevel;
            public int chainload;
            public int proxify;
            public fixed char configfilepath[MAX_PATH];
            public fixed char musicfilename[MAX_PATH];
            public fixed char proxyfilepath[MAX_PATH];
        }

        private MemoryMappedFile m_Mmf = null;
        private MemoryMappedViewAccessor m_Accessor = null;
        private StSHAREDMEMORY* m_pSharedMemory;

        public SRHSharedData()
        {
            m_Mmf = MemoryMappedFile.CreateNew(@"SimpleROHook1011",
                Marshal.SizeOf(typeof(StSHAREDMEMORY)),
                MemoryMappedFileAccess.ReadWrite);
            if (m_Mmf == null)
                MessageBox.Show("CreateOrOpen MemoryMappedFile Failed.");
            m_Accessor = m_Mmf.CreateViewAccessor();

            byte* p = null;
            m_Accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref p);
            m_pSharedMemory = (StSHAREDMEMORY*)p;

            write_packetlog = false;
            freemouse = false;
            ground_zbias = 0;
            alphalevel = 0x7f;
            m2e = false;
            bbe = false;
            deadcell = false;
            chatscope = false;
            castrange = false;
            fix_windowmode_vsyncwait = false;
            show_framerate = false;
            objectinformation = false;
            _44khz_audiomode = false;
            cpucoolerlevel = 0;
            chainload = true;
            proxify = true;
            configfilepath = "";
            musicfilename = "";
            executeorder = false;
            g_hROWindow = 0;
        }
        public void Dispose()
        {
            m_Accessor.Dispose();
            m_Mmf.Dispose();
        }

        public bool write_packetlog
        {
            get
            {
                return (m_pSharedMemory->write_packetlog == 0) ? false : true;
            }
            set
            {
                m_pSharedMemory->write_packetlog = (value == false) ? 0 : 1;
            }
        }
        public bool freemouse
        {
            get
            {
                return (m_pSharedMemory->freemouse == 0)? false : true;
            }
            set
            {
                m_pSharedMemory->freemouse = (value == false)? 0 : 1;
            }
        }
        public int ground_zbias
        {
            get
            {
                return m_pSharedMemory->ground_zbias;
            }
            set
            {
                m_pSharedMemory->ground_zbias = value;
            }
        }
        public int alphalevel
        {
            get
            {
                return m_pSharedMemory->alphalevel;
            }
            set
            {
                m_pSharedMemory->alphalevel = value;
            }
        }

        public bool m2e
        {
            get
            {
                return (m_pSharedMemory->m2e == 0) ? false : true;
            }
            set
            {
                m_pSharedMemory->m2e = (value == false) ? 0 : 1;
            }
        }

        public bool bbe
        {
            get
            {
                return (m_pSharedMemory->bbe == 0) ? false : true;
            }
            set
            {
                m_pSharedMemory->bbe = (value == false) ? 0 : 1;
            }
        }

        public bool deadcell
        {
            get
            {
                return (m_pSharedMemory->deadcell == 0) ? false : true;
            }
            set
            {
                m_pSharedMemory->deadcell = (value == false) ? 0 : 1;
            }
        }

        public bool chatscope
        {
            get
            {
                return (m_pSharedMemory->chatscope == 0) ? false : true;
            }
            set
            {
                m_pSharedMemory->chatscope = (value == false) ? 0 : 1;
            }
        }

        public bool castrange
        {
            get
            {
                return (m_pSharedMemory->castrange == 0) ? false : true;
            }
            set
            {
                m_pSharedMemory->castrange = (value == false) ? 0 : 1;
            }
        }

        public bool fix_windowmode_vsyncwait
        {
            get
            {
                return (m_pSharedMemory->fix_windowmode_vsyncwait == 0) ? false : true;
            }
            set
            {
                m_pSharedMemory->fix_windowmode_vsyncwait = (value == false) ? 0 : 1;
            }
        }
        public bool show_framerate 
        {
            get
            {
                return (m_pSharedMemory->show_framerate == 0) ? false : true;
            }
            set
            {
                m_pSharedMemory->show_framerate = (value == false) ? 0 : 1;
            }
        }
        public bool objectinformation
        {
            get
            {
                return (m_pSharedMemory->objectinformation == 0) ? false : true;
            }
            set
            {
                m_pSharedMemory->objectinformation = (value == false) ? 0 : 1;
            }
        }
        public bool _44khz_audiomode
        {
            get
            {
                return (m_pSharedMemory->_44khz_audiomode == 0) ? false : true;
            }
            set
            {
                m_pSharedMemory->_44khz_audiomode = (value == false) ? 0 : 1;
            }
        }
        public int cpucoolerlevel
        {
            get
            {
                return m_pSharedMemory->cpucoolerlevel;
            }
            set
            {
                m_pSharedMemory->cpucoolerlevel = value;
            }
        }
        public bool chainload
        {
            get
            {
                return (m_pSharedMemory->chainload == 0) ? false : true;
            }
            set
            {
                m_pSharedMemory->chainload = (value == false) ? 0 : 1;
            }
        }
        public bool proxify
        {
            get
            {
                return (m_pSharedMemory->proxify == 0) ? false : true;
            }
            set
            {
                m_pSharedMemory->proxify = (value == false) ? 0 : 1;
            }
        }

        public bool executeorder
        {
            get
            {
                return (m_pSharedMemory->executeorder == 0)? false : true;
            }
            set
            {
                m_pSharedMemory->executeorder = (value == false)? 0 : 1;
            }
        }
        public int g_hROWindow
        {
            get
            {
                return m_pSharedMemory->g_hROWindow;
            }
            set
            {
                m_pSharedMemory->g_hROWindow = value;
            }
        }

        public string configfilepath
        {
            get
            {
                string result = new string(m_pSharedMemory->configfilepath);
                return result;
            }
            set
            {
                char[] cstr = value.ToCharArray();
                Marshal.Copy(cstr, 0, (IntPtr)m_pSharedMemory->configfilepath, cstr.Length);
                m_pSharedMemory->configfilepath[cstr.Length] = '\0';
            }
        }
        public string musicfilename
        {
            get
            {
                string result = new string(m_pSharedMemory->musicfilename);
                return result;
            }
            set
            {
                char[] cstr = value.ToCharArray();
                Marshal.Copy(cstr, 0, (IntPtr)m_pSharedMemory->musicfilename, cstr.Length);
                m_pSharedMemory->musicfilename[cstr.Length] = '\0';
            }
        }


        public string proxyfilepath
        {
            get
            {
                string result = new string(m_pSharedMemory->proxyfilepath);
                return result;
            }
            set
            {
                char[] cstr = value.ToCharArray();
                Marshal.Copy(cstr, 0, (IntPtr)m_pSharedMemory->proxyfilepath, cstr.Length);
                m_pSharedMemory->proxyfilepath[cstr.Length] = '\0';
            }
        }
    }
}
