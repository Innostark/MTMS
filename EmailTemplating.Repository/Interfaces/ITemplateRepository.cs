﻿using System.Collections.Generic;
using EmailTemplating.Models;

namespace EmailTemplating.Repository.Interfaces
{
    /// <summary>
    /// Interface for Template Repository
    /// </summary>
    public interface ITemplateRepository : IBaseRepository<Template, int>
    {
        IEnumerable<Template> GetAllTemplates();
        Template FindTemplate(int id);
        bool MergeVarMapExist(int mergeVarMapId);
    }
}
