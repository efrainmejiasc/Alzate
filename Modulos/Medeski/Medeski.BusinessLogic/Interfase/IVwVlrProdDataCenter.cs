﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IVwVlrProdDataCenter
    {
        IList<VW_VLR_PROD_DATACENTER> GetAll();
        IList<VW_VLR_PROD_DATACENTER> GetAllxServ(int inServ);
    }
}
