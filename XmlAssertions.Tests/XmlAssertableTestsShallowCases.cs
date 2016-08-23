using FluentAssertions;
using Machine.Specifications;
// ReSharper disable UnusedMember.Local
// ReSharper disable InconsistentNaming

namespace XmlAssertions.Tests
{
    public partial class XmlAssertableTests
    {

        [Subject(typeof(IXmlAssertable))]
        class when_comparing_shallowly_with_element_having_a_different_name : spec_for_XmlAssertable
        {
            Establish that = () => SetupSut(@"<person name = ""Piotr"" person-id=""1234"" />");

            Because trigger = () => asserting = () => sut.XmlShould()
                .BeEqualShallowTo(@"<czlowiek name = ""Piotr"" person-id=""1234"" />".ToXmlElement());

            It should_throw_exception_with_proper_message = () => 
                AssertExceptionMessage(
                    "//person[0]",
                    "Expected xml node with name [czlowiek], but found [person]");
        }

        [Subject(typeof(IXmlAssertable))]
        class when_comparing_shallowly_with_element_lacking_some_attributes: spec_for_XmlAssertable
        {
            Establish that = () => SetupSut(@"<person name = ""Piotr"" person-id=""1234"" />");

            Because trigger = () => asserting = () => sut.XmlShould()
               .BeEqualShallowTo(@"<person name = ""Piotr"" person-id=""1234"" surname=""Rutkowski"" birth-year=""1990"" />".ToXmlElement());

            It should_throw_exception_with_proper_message = () => AssertExceptionMessage(
                "//person[0]",
                "Attributes collection does not match expected state, lacking attributes: [surname, birth-year]");
        }

        [Subject(typeof(IXmlAssertable))]
        class when_comparing_shallowly_with_element_having_redundant_attributes: spec_for_XmlAssertable
        {
            Establish that = () => SetupSut(@"<person name = ""Piotr"" person-id=""1234"" />");

            Because trigger = () => asserting = () => sut.XmlShould()
                          .BeEqualShallowTo(@"<person name = ""Piotr"" />".ToXmlElement());
            It should_throw_exception_with_proper_message = () => AssertExceptionMessage(
                "//person[0]",
                "Attributes collection does not match expected state, redundant attributes found: [person-id]");
        }

        [Subject(typeof(IXmlAssertable))]
        class when_comparing_shallowly_with_element_differing_attribute_value : spec_for_XmlAssertable
        {
            Establish that = () => SetupSut(@"<person name = ""Piotr"" person-id=""1234"" />");

            Because trigger = () => asserting = () => sut.XmlShould()
              .BeEqualShallowTo(@"<person name = ""Piotr"" person-id=""9999""  />".ToXmlElement());

            It should_throw_exception_with_proper_message = () => AssertExceptionMessage(
                "//person[0]",
                "Expected attribute [person-id] with value [9999], but found [1234]");
        }

        [Subject(typeof(IXmlAssertable))]
        class when_comparing_shallowly_with_element_matching_name_and_attributes : spec_for_XmlAssertable
        {
            Establish that = () => SetupSut(@"<person name = ""Piotr"" person-id=""1234"" />");

            Because trigger = () => asserting = () => sut.XmlShould()
              .BeEqualShallowTo(@"<person name = ""Piotr"" person-id=""1234""  />".ToXmlElement());

            It should_not_throw = () => asserting.ShouldNotThrow();
        }

        [Subject(typeof(IXmlAssertable))]
        class when_comparing_shallowly_with_element_having_inner_elements_we_lack: spec_for_XmlAssertable
        {
            Establish that = () => SetupSut(@"<person name = ""Piotr"" person-id=""1234"" />");

            Because trigger = () => asserting = () => sut.XmlShould()
              .BeEqualShallowTo(@"<person name = ""Piotr"" person-id=""1234""><inner-tag /></person>".ToXmlElement());

            It should_not_throw = () => asserting.ShouldNotThrow();
        }
    }
}
