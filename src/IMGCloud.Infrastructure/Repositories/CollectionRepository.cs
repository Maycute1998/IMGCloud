using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Repositories
{
    public class CollectionRepository : RepositoryBase<Collection, int>, ICollectionRepository
    {
        public CollectionRepository(ImgCloudContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {

        }
        private async Task<List<CollectionContext>> GetAllCollectionsAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Collections
                .Where(c => c.Status == Status.Active)
                .Select(c => new CollectionContext { Id = c.Id}.MapFor(c))
                .Take(10)
                .ToListAsync(cancellationToken);
        }

        Task<List<CollectionContext>> ICollectionRepository.GetAllCollectionsAsync(CancellationToken cancellationToken)
        => this.GetAllCollectionsAsync(cancellationToken);
    }
}
