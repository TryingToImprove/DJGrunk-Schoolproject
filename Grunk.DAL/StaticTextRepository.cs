using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grunk.Domain.Repositories;
using System.Xml;
using Grunk.Domain;

namespace Grunk.DAL
{
    public class StaticTextRepository : BaseRepository<StaticText>, IStaticTextRepository
    {
        public StaticTextRepository(Entities entities)
            : base(entities)
        {
        }

        public StaticText GetByPosition(string position)
        {
            return entities.Single(x => x.Position == position);
        }

        public void Create(string position, string header, string text)
        {
            entities.AddObject(new StaticText
            {
                Header = header,
                Text = text,
                Position = position
            });
            SaveChanges();
        }

        public void Update(string position, string header, string text)
        {
            var staticText = GetByPosition(position);
            staticText.Header = header;
            staticText.Text = text;

            SaveChanges();
        }
    }
}
