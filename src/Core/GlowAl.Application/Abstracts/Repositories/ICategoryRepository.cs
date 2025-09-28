using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlowAl.Domain.Entities;
using GlowAl.Persistence.Repositories;

namespace GlowAl.Application.Abstracts.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
}
