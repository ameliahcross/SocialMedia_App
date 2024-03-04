using AutoMapper;
using SocialMedia_App.Core.Application.Interfaces.Repositories;
using SocialMedia_App.Core.Application.Interfaces.Services;

namespace SocialMedia_App.Core.Application.Services
{
    public class GenericService<SaveViewModel, ViewModel, Entity> : IGenericService<SaveViewModel, ViewModel, Entity>
         where SaveViewModel : class
         where ViewModel : class
         where Entity : class
    {
        private readonly IGenericRepository<Entity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task Update(SaveViewModel viewModel, int id)
        {
            Entity entity = _mapper.Map<Entity>(viewModel);
            await _repository.UpdateAsync(entity, id);
        }

        public virtual async Task<SaveViewModel> Add(SaveViewModel viewModel)
        {
            Entity entity = _mapper.Map<Entity>(viewModel);
            entity = await _repository.AddAsync(entity);
            SaveViewModel saveViewModel = _mapper.Map<SaveViewModel>(entity);
            return saveViewModel;
        }

        public virtual async Task Delete(int id)
        {
            Entity entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity);
        }

        public virtual async Task<SaveViewModel> GetByIdSaveViewModel(int id)
        {
            Entity entity = await _repository.GetByIdAsync(id);
            SaveViewModel saveViewModel = _mapper.Map<SaveViewModel>(entity);
            return saveViewModel;
        }

        public virtual async Task<List<ViewModel>> GetAllViewModel()
        {
           List<Entity> entityList = await _repository.GetAllAsync();
           return _mapper.Map<List<ViewModel>>(entityList);
        }

    }

}
