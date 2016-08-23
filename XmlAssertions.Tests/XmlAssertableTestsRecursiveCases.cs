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
        [Subject(typeof(IXmlAssertable))]
        class when_comparing_identical_xml_trees: spec_for_XmlAssertable
        {
            Given that = () => SetupSut(Resource.ExpectedXml);

            When trigger = () => asserting = () => sut.XmlShould().BeEqualTo(Resource.ExpectedXml);

            Then should_not_throw_exception = () => asserting.ShouldNotThrow();
        }

        [Subject(typeof(IXmlAssertable))]
        class when_comparing_with_xml_tree_having_different_attribute_set : spec_for_XmlAssertable
        {
            Given that = () => SetupSut(Resource.AttributesSetDiffering);

            When trigger = () => asserting = () => sut.XmlShould().BeEqualTo(Resource.ExpectedXml);

            Then should_throw_exception_with_proper_message =
                () =>
                    AssertExceptionMessage("//people[0]/person[1]/documents[2]/document[1]",
                        "Attributes collection does not match expected state, redundant attributes found: [valid-from]lacking attributes: [number]");
        }

        [Subject(typeof(IXmlAssertable))]
        class when_comparing_with_xml_tree_having_different_attribute_value : spec_for_XmlAssertable
        {  
            Given that = () => SetupSut(Resource.AttributeValueDiffering);

            When trigger = () => asserting = () => sut.XmlShould().BeEqualTo(Resource.ExpectedXml);

            Then should_throw_exception_with_proper_message =
               () =>
                   AssertExceptionMessage("//people[0]/person[1]/address[1]",
                       "Expected attribute [number] with value [10], but found [55]");
        }

        [Subject(typeof(IXmlAssertable))]
        class when_comparing_with_xml_tree_having_different_child_node_name: spec_for_XmlAssertable
        {
            Given that = () => SetupSut(Resource.ChildNodeNameDiffering);

            When trigger = () => asserting = () => sut.XmlShould().BeEqualTo(Resource.ExpectedXml);

            Then should_throw_exception_with_proper_message =
               () =>
                   AssertExceptionMessage("//people[0]/person[1]/documents[2]/paszport[1]",
                       "Expected xml node with name [document], but found [paszport]");
        }
        [Subject(typeof(IXmlAssertable))]
        class when_comparing_with_xml_tree_having_different_child_count: spec_for_XmlAssertable
        {
            Given that = () => SetupSut(Resource.ChildrenCountDiffering);

            When trigger = () => asserting = () => sut.XmlShould().BeEqualTo(Resource.ExpectedXml);

            Then should_throw_exception_with_proper_message =
               () =>
                   AssertExceptionMessage("//people[0]/person[1]/documents[2]",
                       "Number of children is incorrent, expected [2], but was [3]");
        }

        [Subject(typeof(IXmlAssertable))]
        class when_comparing_with_xml_tree_having_different_tag_content: spec_for_XmlAssertable
        {
            Given that = () => SetupSut(Resource.TagContentDiffering);

            When trigger = () => asserting = () => sut.XmlShould().BeEqualTo(Resource.ExpectedXml);

            Then should_throw_exception_with_proper_message =
               () =>
                   AssertExceptionMessage("//people[0]/person[1]/documents[2]/document[0]/authority[0]/name[0]/#text[0]",
                       "Expected text [Urzad gminy w Terespolu], but found [Urzad gminy w Kalinowie]");
        }
    }
}
