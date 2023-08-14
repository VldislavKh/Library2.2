using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.IntegrationTests
{
    [CollectionDefinition("Test Collection")]
    public class AuthorControllerTestCollection : ICollectionFixture<TestingWebAppFactory> 
    {
    }
}
