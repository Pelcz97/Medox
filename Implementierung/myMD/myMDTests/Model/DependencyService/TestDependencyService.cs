using myMD.Model.DependencyService;
using myMD.Model.FileHelper;
using myMDTests.Model.FileHelper;
using myMDTests.Model.ParserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myMDTests.Model.DependencyService
{
    public class TestDependencyService : IDependencyService
    {
        public T Get<T>() where T : class
        {
            return new TestFileHelper() as T ?? new TestHl7ParserHelper() as T;
        }
    }
}
