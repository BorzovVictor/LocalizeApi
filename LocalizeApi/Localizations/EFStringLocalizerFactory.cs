using System;
using System.Collections.Generic;
using System.Linq;
using LocalizeApi.Data;
using LocalizeApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace LocalizeApi
{
    public class EFStringLocalizerFactory : IStringLocalizerFactory
    {
        readonly string _connectionString;
        public EFStringLocalizerFactory(string connection)
        {
            _connectionString = connection;
        }
 
        public IStringLocalizer Create(Type resourceSource)
        {
            return CreateStringLocalizer();
        }
 
        public IStringLocalizer Create(string baseName, string location)
        {
            return CreateStringLocalizer();
        }
 
        private IStringLocalizer CreateStringLocalizer()
        {
            var db = new LocalizationContext(
                new DbContextOptionsBuilder<LocalizationContext>()
                    .UseSqlServer(_connectionString)
                    .Options);
            
            db.Database.Migrate();
            
            // initial db
            if (!db.Cultures.Any())
            {
                db.AddRange(
                    new Culture
                    {
                        Name = "en",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "Header", Value = "Hello" },
                            new Resource { Key = "Message", Value = "Welcome" }
                        }
                    },
                    new Culture
                    {
                        Name = "ru",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "Header", Value = "Привет" },
                            new Resource { Key = "Message", Value = "Добро пожаловать" }
                        }
                    },
                    new Culture
                    {
                        Name = "de",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "Header", Value = "Hallo" },
                            new Resource { Key = "Message", Value = "Willkommen" }
                        }
                    }
                );
                db.SaveChanges();
            }
            return new EFStringLocalizer(db);
        }
    }
}