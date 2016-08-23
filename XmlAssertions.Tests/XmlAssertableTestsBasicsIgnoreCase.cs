using FluentAssertions;
using Machine.Specifications;
// ReSharper disable InconsistentNaming

namespace XmlAssertions.Tests
{
    public partial class XmlAssertableTests
    {
        [Subject(typeof(IXmlAssertable))]
        class when_asserting_attribute_we_have_case_insensitive : spec_for_XmlAssertable
        {
            Establish that = () => SetupSut(@"<person NAME = ""PIOTR"" />");

            Because comparison_ignores_case = () => asserting = () => sut.XmlShould().IgnoreCase().HaveAttribute("name");

            It should_not_throw_exception = () => asserting.ShouldNotThrow();
        }

        [Subject(typeof(IXmlAssertable))]
        class when_asserting_attribute_value_we_have_case_insensitive : spec_for_XmlAssertable
        {
            Establish that = () => SetupSut(@"<person NAME = ""PIOTR"" />");

            Because comparison_ignores_case = () => asserting = () => sut.XmlShould().IgnoreCase().HaveAttribute("name", "piotr");

            It should_not_throw_exception = () => asserting.ShouldNotThrow();
        }

        [Subject(typeof(IXmlAssertable))]
        class when_asserting_element_name_we_have_case_insensitice : spec_for_XmlAssertable
        {
            Establish that = () => SetupSut(@"<person name = ""Piotr"" />");

            Because comparison_ignores_case = () => asserting = () => sut.XmlShould().IgnoreCase().HaveName("PERSON");

            It should_not_throw_exception = () => asserting.ShouldNotThrow();
        }

    }
}
