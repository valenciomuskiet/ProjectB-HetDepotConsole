using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ProjectBCaseTheDepot
{
    public class Reservering                                                    /// Class voor reservering
    {
        public int code;
        public DateTime tijd;

        public Reservering(int Acode, DateTime Atijd)                            ///Constructor 
        {
            code = Acode;
            tijd = Atijd;
        }
    }
}

