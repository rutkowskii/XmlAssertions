using System;
using System.Xml;
using FluentAssertions;
using Machine.Specifications;
using Given = Machine.Specifications.Establish;
using When = Machine.Specifications.Because;
using Then = Machine.Specifications.It;

// ReSharper disable InconsistentNaming

namespace XmlAssertions.Tests
{
    public partial class XmlAssertableTests
    {
        class spec_for_XmlAssertable
        {
            protected static XmlNode sut;
            protected static Action asserting;

            protected static void SetupSut(string xmlString)
            {
                sut = xmlString.ToXmlElement();
            }

            protected static void AssertExceptionMessage(string path, string exceptionMessage)
            {
                var effectiveMessage = string.Format("Node {0}; {1}", path, exceptionMessage);
                asserting.ShouldThrow<XmlAssertionException>()
                    .WithMessage(effectiveMessage);
            }
        }

        [Subject(typeof(IXmlAssertable))]
        class when_asserting_attribute_we_do_not_have : spec_for_XmlAssertable
        {
            Given that = () => SetupSut(@"<person name = ""Piotr"" />");

            When trigger = () => asserting = () => sut.XmlShould().HaveAttribute("surname");

            Then should_throw_exception_with_proper_message = () => AssertExceptionMessage(
                "//person[0]",
                "Expected attribute [surname] was not found");
        }

        [Subject(typeof(IXmlAssertable))]
        class when_asserting_attribute_we_have_but_with_different_value : spec_for_XmlAssertable
        {
            Given that = () => SetupSut(@"<person name = ""Piotr"" />");

            When trigger = () => asserting = () => sut.XmlShould().HaveAttribute("name", "Paweł");

            Then should_throw_exception_with_proper_message = 
                () => AssertExceptionMessage("//person[0]", "Expected attribute [name] with value [Paweł], but found [Piotr]");
        }

        [Subject(typeof(IXmlAssertable))]
        class when_asserting_attribute_we_have : spec_for_XmlAssertable
        {
            Given that = () => SetupSut(@"<person name = ""Piotr"" />");

            When trigger = () => asserting = () => sut.XmlShould().HaveAttribute("name");

            Then should_not_throw_exception = () => asserting.ShouldNotThrow();
        }

        [Subject(typeof(IXmlAssertable))]
        class when_asserting_attribute_value_we_have : spec_for_XmlAssertable
        {
            Given that = () => SetupSut(@"<person name = ""Piotr"" />");

            When trigger = () => asserting = () => sut.XmlShould().HaveAttribute("name", "Piotr");

            Then should_not_throw_exception = () => asserting.ShouldNotThrow();
        }

        [Subject(typeof(IXmlAssertable))]
        class when_asserting_element_name_we_do_not_have : spec_for_XmlAssertable
        {
            Given that = () => SetupSut(@"<person name = ""Piotr"" />");

            When trigger = () => asserting = () => sut.XmlShould().HaveName("cat");

            Then should_throw_exception_with_proper_message = () => AssertExceptionMessage(
                "//person[0]",
                "Expected xml node with name [cat], but found [person]");
        }

        [Subject(typeof(IXmlAssertable))]
        class when_asserting_element_name_we_have : spec_for_XmlAssertable
        {
            Given that = () => SetupSut(@"<person name = ""Piotr"" />");

            When trigger = () => asserting = () => sut.XmlShould().HaveName("person");

            Then should_not_throw_exception = () =>  asserting.ShouldNotThrow();
        }
    }
}