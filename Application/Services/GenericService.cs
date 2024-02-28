using SocialMedia_App.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia_App.Core.Application.Services
{
    internal class GenericService<SaveViewModel, ViewModel, Entity> : IGenericService<SaveViewModel, ViewModel, Entity> 
        where SaveViewModel : class
        where ViewModel : class
        where Entity : class
    {

    }
}
