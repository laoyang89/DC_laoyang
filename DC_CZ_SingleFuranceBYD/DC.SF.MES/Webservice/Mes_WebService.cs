using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.MES
{
    public class Mes_WebService : MesBase
    {
        public override bool ConnectTest()
        {
            throw new NotImplementedException();
        }

        public override bool CreateConnect()
        {
            throw new NotImplementedException();
        }

        public override bool LoadSend(string code)
        {
            return true;
        }

        public override bool OutFurnanceSend()
        {
            throw new NotImplementedException();
        }

        public override bool ScanCodeCheck(string code)
        {
            return true;
        }

        public override bool UnLoadSend()
        {
            throw new NotImplementedException();
        }
    }
}
