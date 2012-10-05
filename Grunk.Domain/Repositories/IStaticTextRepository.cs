﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grunk.Domain.Repositories
{
    public interface IStaticTextRepository : IBaseRepository
    {
        StaticText GetByPosition(string position);

        void Create(string position, string header, string text);
        void Update(string position, string header, string text);
    }
}
