﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Repositories
{
    public interface IPictureRepository : IBaseRepository
    {
        void Delete(int id);
    }
}
