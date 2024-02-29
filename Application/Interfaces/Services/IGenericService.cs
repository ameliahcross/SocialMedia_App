using System;

namespace SocialMedia_App.Core.Application.Interfaces.Services
{
	public interface IGenericService<SaveViewModel, ViewModel, Entity>
                    where SaveViewModel : class
                    where ViewModel : class
                    where Entity : class
    {
        Task<List<ViewModel>> GetAllViewModel();
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
        Task Update(SaveViewModel saveViewModel, int id);
        Task<SaveViewModel> Add(SaveViewModel saveViewModel);
        Task Delete(int id);
    }
}

