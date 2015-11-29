﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EPiServer.Marketing.Multivariate.Dal
{
    public interface IMultivariateTestDal
    {
        MultivariateTestParameters Get(Guid objectId);
        MultivariateTestParameters[] GetByOriginalItemId(Guid itemId);
        Guid Add(MultivariateTestParameters parameters);
        void Update(MultivariateTestParameters parameters);
        void Delete(Guid Id);
        void UpdateViews(Guid TestId, Guid ItemId);
        void UpdateConversions(Guid TestId, Guid ItemId);
        MultivariateTestParameters[] GetFilteredTestList(MultivariateTestCriteria criteria);
    }
}
