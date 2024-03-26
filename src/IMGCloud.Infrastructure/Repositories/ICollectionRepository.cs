﻿using IMGCloud.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Repositories
{
    public interface ICollectionRepository
    {
        Task<List<CollectionContext>> GetAllCollectionsAsync(CancellationToken cancellationToken);
    }
}
