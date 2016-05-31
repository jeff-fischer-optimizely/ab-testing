﻿using System;
using EPiServer.Marketing.Testing.Data;

namespace EPiServer.Marketing.Testing.Web.Repositories
{
    public interface IMarketingTestingWebRepository
    {
        IMarketingTest GetTestById(Guid testGuid);
        Guid CreateMarketingTest(TestingStoreModel testData);
        void DeleteMarketingTest(Guid testGuid);
        void StartMarketingTest(Guid testGuid);
        void StopMarketingTest(Guid testGuid);
        void ArchiveMarketingTest(Guid testGuid);
        IMarketingTest GetActiveTestForContent(Guid contentGuid);
        void DeleteTestForContent(Guid contentGuid);
    }
}
