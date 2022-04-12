using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.LeilaoOnline.Selenium.Fixtures
{
    [CollectionDefinition("Chrome Driver")]
    public class CollectionFixture : ICollectionFixture<TestFixture>
    {

    }
}
