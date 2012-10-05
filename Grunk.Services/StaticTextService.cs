using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain.Services;
using System.Security.Cryptography;
using Grunk.Domain.Repositories;

namespace Grunk.Services
{
    public class StaticTextService : IStaticTextService
    {
        IStaticTextRepository StaticTextRepository;

        public StaticTextService(IStaticTextRepository staticTextRepository)
        {
            this.StaticTextRepository = staticTextRepository;
        }

        public Domain.StaticText GetByPosition(string name)
        {
            return StaticTextRepository.GetByPosition(name);
        }

        public void Create(string position, string header, string text)
        {
            StaticTextRepository.Create(position, header, text);
        }


        public void Update(string position, string header, string text)
        {
            StaticTextRepository.Update(position, header, text);
        }
    }
}
