using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Repositories;
using IMGCloud.Infrastructure.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _collectionRepository;
        public CollectionService(ICollectionRepository collectionRepository) 
        {
            _collectionRepository = collectionRepository;
        }

        public async Task<List<CollectionContext>> GetAllCollectionsAsync(CancellationToken cancellationToken)
        => await _collectionRepository.GetAllCollectionsAsync(cancellationToken);

        Task<List<CollectionContext>> ICollectionService.GetAllCollectionsAsync(CancellationToken cancellationToken)
        => this.GetAllCollectionsAsync( cancellationToken);

    }
}
