using FluentAssertions;
using Machine.Specifications;
// ReSharper disable InconsistentNaming

namespace XmlAssertions.Tests
{
    public partial class XmlAssertableTests
    {
        [Subject(typeof(IXmlAssertable))]
        class when_comparing_identical_xml_trees_but_differing_letter_case : spec_for_XmlAssertable
        {
            Establish that = () => SetupSut(Resource.Uppercase);

            Because comparison_ignores_case = () => asserting = () => sut.XmlShould().IgnoreCase().BeEqualTo(Resource.ExpectedXml);

            It should_not_throw_exception = () => asserting.ShouldNotThrow();
        }
    }
}
