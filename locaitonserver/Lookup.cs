using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;

namespace locationserver
{
    class Lookup
    {
        private static Lookup _lookup;
        public static Lookup GetInstance
        {
            get
            {
                if (_lookup == null)
                {
                    _lookup = new Lookup();
                }
                return _lookup;
            }
        }
        public ConcurrentDictionary<string, string> _Location = new ConcurrentDictionary<string, string>();

        public Lookup() {

        }

        public ConcurrentDictionary<string, string> Location { get { return _Location; } }

        public void AddUser(string User) 
        {

        }


    }
}
