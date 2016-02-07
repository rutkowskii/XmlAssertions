using System;
using System.Xml;
using FluentAssertions;
using FluentAssertions.Specialized;
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

            protected static ExceptionAssertions<XmlAssertionException> AssertExceptionMessage(string exceptionMessage)
            {
                return asserting.ShouldThrow<XmlAssertionException>()
                    .WithMessage(exceptionMessage);
            }
        }

        [Subject(typeof (XmlAssertable))]
        class when_asserting_attribute_we_do_not_have : spec_for_XmlAssertable
        {
            Given that = () => SetupSut(@"<person name = ""Piotr"" />");

            When trigger = () => asserting = () => sut.XmlShould().HaveAttribute("surname");

            Then should_throw_exception_with_proper_message = () => AssertExceptionMessage("Expected attribute [surname] was not found");
        }

        [Subject(typeof (XmlAssertable))]
        class when_asserting_attribute_we_have_but_with_different_value : spec_for_XmlAssertable
        {
            Given that = () => SetupSut(@"<person name = ""Piotr"" />");

            When trigger = () => asserting = () => sut.XmlShould().HaveAttribute("name", "Gosia");

            Then should_throw_exception_with_proper_message = 
                () => AssertExceptionMessage("Expected attribute [name] with value [Gosia], but found value [Piotr]");
        }

        [Subject(typeof (XmlAssertable))]
        class when_asserting_attribute_we_have : spec_for_XmlAssertable
        {
            Given that = () => SetupSut(@"<person name = ""Piotr"" />");

            When trigger = () => asserting = () => sut.XmlShould().HaveAttribute("name");

            Then should_not_throw_exception = () => asserting.ShouldNotThrow();
        }

        [Subject(typeof(XmlAssertable))]
        class when_asserting_attribute_value_we_have : spec_for_XmlAssertable
        {
            Given that = () => SetupSut(@"<person name = ""Piotr"" />");

            When trigger = () => asserting = () => sut.XmlShould().HaveAttribute("name", "Piotr");

            Then should_not_throw_exception = () => asserting.ShouldNotThrow();
        }

        [Subject(typeof(XmlAssertable))]
        class when_asserting_element_name_we_do_not_have : spec_for_XmlAssertable
        {
            Given that = () => SetupSut(@"<person name = ""Piotr"" />");

            When trigger = () => asserting = () => sut.XmlShould().HaveName("cat");

            Then should_throw_exception_with_proper_message = () => AssertExceptionMessage("Expected xml node with name [cat], but found [person]");
        }

        [Subject(typeof(XmlAssertable))]
        class when_asserting_element_name_we_have : spec_for_XmlAssertable
        {
            Given that = () => SetupSut(@"<person name = ""Piotr"" />");

            When trigger = () => asserting = () => sut.XmlShould().HaveName("person");

            Then should_not_throw_exception = () =>  asserting.ShouldNotThrow();
        }
    }
}